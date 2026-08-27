using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace AIQuantTradingResearch.Infrastructure.Tests;

internal sealed class WindowsIsolatedProcessGroup : IDisposable
{
    private const uint CreateNewProcessGroup = 0x00000200;
    private const uint CreateUnicodeEnvironment = 0x00000400;
    private const uint CtrlBreakEvent = 1;
    private const uint WaitObject0 = 0;
    private const uint WaitTimeout = 258;
    private const uint StartfUseStdHandles = 0x100;
    private const uint HandleFlagInherit = 1;
    private const int DiagnosticCaptureLimitBytes = 64 * 1024;
    private readonly SafeKernelHandle processHandle;
    private readonly SafeKernelHandle threadHandle;
    private readonly SafeFileHandle stdoutRead;
    private readonly SafeFileHandle stderrRead;
    private readonly Task<BoundedCapturedStream> stdoutDrain;
    private readonly Task<BoundedCapturedStream> stderrDrain;
    private SignalDiagnostic? signal;

    private WindowsIsolatedProcessGroup(SafeKernelHandle processHandle, SafeKernelHandle threadHandle, int processId, SafeFileHandle stdoutRead, SafeFileHandle stderrRead)
    {
        this.processHandle = processHandle;
        this.threadHandle = threadHandle;
        ProcessId = processId;
        ProcessGroupId = processId;
        this.stdoutRead = stdoutRead; this.stderrRead = stderrRead;
        stdoutDrain = Task.Run(() => DrainBoundedSynchronously(new FileStream(stdoutRead, FileAccess.Read, 4096, false)));
        stderrDrain = Task.Run(() => DrainBoundedSynchronously(new FileStream(stderrRead, FileAccess.Read, 4096, false)));
    }

    public int ProcessId { get; }
    public int ProcessGroupId { get; }
    public WindowsProcessDiagnosticSnapshot DiagnosticSnapshot => new(ProcessId, (uint)ProcessGroupId, CreateNewProcessGroup | CreateUnicodeEnvironment, true, true, true, CtrlBreakEvent, signal?.Target ?? 0, signal?.Result, signal?.Error, TryExitCode(), stdoutDrain.GetAwaiter().GetResult(), stderrDrain.GetAwaiter().GetResult());

    public static WindowsIsolatedProcessGroup Launch(
        string executablePath,
        IReadOnlyList<string> arguments,
        string workingDirectory,
        IReadOnlyDictionary<string, string> environment)
    {
        if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException("WP08 Windows process-group acceptance is Windows-only.");
        ArgumentException.ThrowIfNullOrWhiteSpace(executablePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(workingDirectory);
        if (!Path.IsPathFullyQualified(executablePath) || !Path.IsPathFullyQualified(workingDirectory)) throw new ArgumentException("WP08 process launch paths must be absolute.");
        if (!File.Exists(executablePath)) throw new FileNotFoundException("The Worker executable was not found.", executablePath);

        string commandLine = string.Join(' ', new[] { Quote(executablePath) }.Concat(arguments.Select(Quote)));
        string environmentBlock = string.Join('\0', Environment.GetEnvironmentVariables()
            .Cast<System.Collections.DictionaryEntry>()
            .Select(entry => new KeyValuePair<string, string>((string)entry.Key, (string?)entry.Value ?? string.Empty))
            .Concat(environment)
            .GroupBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.Last())
            .OrderBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
            .Select(pair => $"{pair.Key}={pair.Value}")) + "\0\0";

        IntPtr environmentMemory = Marshal.StringToHGlobalUni(environmentBlock);
        var stdout = CreatePipePair(); var stderr = CreatePipePair();
        try
        {
            var startup = new StartupInfo { cb = Marshal.SizeOf<StartupInfo>(), dwFlags = StartfUseStdHandles, hStdOutput = stdout.Child.DangerousGetHandle(), hStdError = stderr.Child.DangerousGetHandle(), hStdInput = GetStdHandle(unchecked((uint)-10)) };
            if (!CreateProcessW(executablePath, new StringBuilder(commandLine), IntPtr.Zero, IntPtr.Zero, true,
                CreateNewProcessGroup | CreateUnicodeEnvironment, environmentMemory, workingDirectory, ref startup, out ProcessInformation information))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "CreateProcessW failed for the owned WP08 Worker process.");
            }

            stdout.Child.Dispose(); stderr.Child.Dispose();
            return new WindowsIsolatedProcessGroup(new SafeKernelHandle(information.hProcess), new SafeKernelHandle(information.hThread), unchecked((int)information.dwProcessId), stdout.Parent, stderr.Parent);
        }
        catch { stdout.Parent.Dispose(); stdout.Child.Dispose(); stderr.Parent.Dispose(); stderr.Child.Dispose(); throw; }
        finally
        {
            Marshal.FreeHGlobal(environmentMemory);
        }
    }

    public void RequestCtrlBreak()
    {
        if (ProcessGroupId <= 0) throw new InvalidOperationException("The owned Worker process group must be non-zero.");
        signal = new SignalDiagnostic((uint)ProcessGroupId, null, null);
        bool result = GenerateConsoleCtrlEvent(CtrlBreakEvent, unchecked((uint)ProcessGroupId));
        signal = new SignalDiagnostic((uint)ProcessGroupId, result, result ? null : Marshal.GetLastWin32Error());
        if (!result)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error(), "GenerateConsoleCtrlEvent(CTRL_BREAK_EVENT) failed for the owned Worker process group.");
        }
    }

    public bool WaitForExit(TimeSpan timeout)
    {
        uint milliseconds = timeout.TotalMilliseconds >= uint.MaxValue ? uint.MaxValue : checked((uint)Math.Ceiling(timeout.TotalMilliseconds));
        uint result = WaitForSingleObject(processHandle.DangerousGetHandle(), milliseconds);
        return result switch { WaitObject0 => true, WaitTimeout => false, _ => throw new Win32Exception(Marshal.GetLastWin32Error(), "WaitForSingleObject failed.") };
    }

    public int ExitCode
    {
        get
        {
            if (!GetExitCodeProcess(processHandle.DangerousGetHandle(), out uint code)) throw new Win32Exception(Marshal.GetLastWin32Error(), "GetExitCodeProcess failed.");
            return unchecked((int)code);
        }
    }

    public void TerminateOwnedProcessForCleanup()
    {
        if (!TerminateProcess(processHandle.DangerousGetHandle(), unchecked((uint)-1))) throw new Win32Exception(Marshal.GetLastWin32Error(), "TerminateProcess failed for the owned Worker process.");
    }

    public void Dispose()
    {
        try { Task.WhenAll(stdoutDrain, stderrDrain).Wait(TimeSpan.FromSeconds(1)); } catch { }
        stdoutRead.Dispose(); stderrRead.Dispose();
        threadHandle.Dispose();
        processHandle.Dispose();
    }

    private int? TryExitCode() { try { return ExitCode; } catch { return null; } }
    private static BoundedCapturedStream DrainBoundedSynchronously(Stream stream)
    {
        byte[] buffer = new byte[4096]; using var retained = new MemoryStream(); bool truncated = false;
        try { int read; while ((read = stream.Read(buffer, 0, buffer.Length)) != 0) { int keep = Math.Min(read, DiagnosticCaptureLimitBytes - (int)retained.Length); if (keep > 0) retained.Write(buffer, 0, keep); if (keep < read) truncated = true; } return new BoundedCapturedStream(Encoding.UTF8.GetString(retained.ToArray()), truncated, false); }
        finally { stream.Dispose(); }
    }
    private static (SafeFileHandle Parent, SafeFileHandle Child) CreatePipePair()
    {
        if (!CreatePipe(out SafeFileHandle read, out SafeFileHandle write, IntPtr.Zero, 0)) throw new Win32Exception(Marshal.GetLastWin32Error(), "CreatePipe failed.");
        if (!SetHandleInformation(read, HandleFlagInherit, 0)) { read.Dispose(); write.Dispose(); throw new Win32Exception(Marshal.GetLastWin32Error(), "SetHandleInformation failed."); }
        return (read, write);
    }

    internal static string Quote(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value.Length != 0 && value.IndexOfAny([' ', '\t', '"']) < 0) return value;
        var result = new StringBuilder("\"");
        int backslashes = 0;
        foreach (char character in value)
        {
            if (character == '\\') { backslashes++; continue; }
            if (character == '"') result.Append('\\', backslashes * 2 + 1).Append(character);
            else result.Append('\\', backslashes).Append(character);
            backslashes = 0;
        }
        result.Append('\\', backslashes * 2).Append('"');
        return result.ToString();
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct StartupInfo
    {
        public int cb;
        public string? lpReserved;
        public string? lpDesktop;
        public string? lpTitle;
        public int dwX;
        public int dwY;
        public int dwXSize;
        public int dwYSize;
        public int dwXCountChars;
        public int dwYCountChars;
        public int dwFillAttribute;
        public uint dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct ProcessInformation
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public uint dwProcessId;
        public uint dwThreadId;
    }

    private sealed class SafeKernelHandle : SafeHandle
    {
        public SafeKernelHandle(IntPtr value) : base(IntPtr.Zero, true) => SetHandle(value);
        public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);
        protected override bool ReleaseHandle() => CloseHandle(handle);
    }

    private sealed record SignalDiagnostic(uint Target, bool? Result, int? Error);
    internal sealed record BoundedCapturedStream(string Text, bool Truncated, bool DrainTimedOut);
    internal sealed record WindowsProcessDiagnosticSnapshot(int ProcessId, uint ProcessGroupId, uint CreationFlags, bool InheritHandles, bool StdoutRedirected, bool StderrRedirected, uint SignalEvent, uint SignalTargetGroupId, bool? SignalResult, int? SignalWin32Error, int? ExitCode, BoundedCapturedStream Stdout, BoundedCapturedStream Stderr);
    [StructLayout(LayoutKind.Sequential)] private struct SECURITY_ATTRIBUTES { public int nLength; public IntPtr lpSecurityDescriptor; public bool bInheritHandle; }
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool CreatePipe(out SafeFileHandle hReadPipe, out SafeFileHandle hWritePipe, IntPtr lpPipeAttributes, uint nSize);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool SetHandleInformation(SafeHandle hObject, uint dwMask, uint dwFlags);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr GetStdHandle(uint nStdHandle);
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1838", Justification = "CreateProcessW requires a writable command-line buffer.")]
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern bool CreateProcessW(string lpApplicationName, StringBuilder lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref StartupInfo lpStartupInfo, out ProcessInformation lpProcessInformation);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
    [DllImport("kernel32.dll", SetLastError = true)] private static extern bool CloseHandle(IntPtr hObject);
}
