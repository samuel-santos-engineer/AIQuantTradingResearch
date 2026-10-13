using System.Buffers.Binary;

namespace AIQuantTradingResearch.AdminDiagnostics;

// Pure models. A decoded report never establishes a kernel wait or role integrity.
public enum LifecycleRole : byte { Root = 1, Supervisor, GuardianBootstrap, Guardian, RouterBootstrap, Router, AgentBootstrap, Agent }
public enum LifecycleMessage : byte
{
    MetaRouter = 1, MetaGuardian = 2, MetaAgent = 3,
    GBound = 0x10, RPrepare, RBound, GRelease, AExec, AExecAck, ABind, AReady,
    StartUi, UiReady, RUiReady, RExpose, RExposed,
    StopRouter = 0x20, StopGuardian, StopUi, UiStopped, RProtocolDone, HostDisposed,
    AgentResult = 0x30, GuardianObserved, RouterObserved, ChildrenAbsent, Fail = 0x7f,
    AgentSignal = 0xb0, AgentWaitFailure, GuardianSignal, GuardianWaitFailure, RouterSignal, RouterWaitFailure
}
public sealed class LifecycleValidationException : Exception
{
    public LifecycleValidationException() : base("Lifecycle validation failed.") { }
}

public sealed record LifecycleRecord(LifecycleRole Role, LifecycleMessage Message, byte Outcome,
    Guid Launch, uint Sequence, uint SenderPid, uint RelatedPid, ulong StartupDeadline, ulong ShutdownDeadline = 0)
{
    public void Validate()
    {
        if (!Enum.IsDefined(Role) || !Enum.IsDefined(Message) || Launch == Guid.Empty || Sequence is < 1 or > 32
            || SenderPid is < 1 or > int.MaxValue || RelatedPid > int.MaxValue
            || (RelatedPid == 0 && Message != LifecycleMessage.ChildrenAbsent)
            || StartupDeadline is 0 or > long.MaxValue || ShutdownDeadline > long.MaxValue)
            throw new LifecycleValidationException();
        var valid = Message switch
        {
            LifecycleMessage.AgentResult or LifecycleMessage.GuardianObserved or LifecycleMessage.RouterObserved => true,
            LifecycleMessage.AgentSignal or LifecycleMessage.GuardianSignal or LifecycleMessage.RouterSignal => Outcome is >= 1 and <= 64,
            LifecycleMessage.AgentWaitFailure or LifecycleMessage.GuardianWaitFailure or LifecycleMessage.RouterWaitFailure => Outcome is >= 1 and <= 4,
            LifecycleMessage.Fail => Outcome is >= 1 and <= 8,
            _ => Outcome == 0
        };
        if (!valid) throw new LifecycleValidationException();
    }
    public byte[] Encode()
    {
        Validate();
        var bytes = new byte[64];
        "AIQL"u8.CopyTo(bytes); bytes[4] = 1; bytes[5] = (byte)Role;
        bytes[6] = (byte)Message; bytes[7] = Outcome;
        Launch.TryWriteBytes(bytes.AsSpan(8, 16), bigEndian: true, out _);
        BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan(24), Sequence);
        BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan(28), SenderPid);
        BinaryPrimitives.WriteUInt32BigEndian(bytes.AsSpan(32), RelatedPid);
        BinaryPrimitives.WriteUInt64BigEndian(bytes.AsSpan(36), StartupDeadline);
        BinaryPrimitives.WriteUInt64BigEndian(bytes.AsSpan(44), ShutdownDeadline);
        return bytes;
    }
    public static LifecycleRecord Decode(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 64 || !bytes[..4].SequenceEqual("AIQL"u8) || bytes[4] != 1
            || bytes[52..].IndexOfAnyExcept((byte)0) >= 0) throw new LifecycleValidationException();
        var record = new LifecycleRecord((LifecycleRole)bytes[5], (LifecycleMessage)bytes[6], bytes[7],
            new Guid(bytes.Slice(8, 16), bigEndian: true),
            BinaryPrimitives.ReadUInt32BigEndian(bytes[24..]), BinaryPrimitives.ReadUInt32BigEndian(bytes[28..]),
            BinaryPrimitives.ReadUInt32BigEndian(bytes[32..]), BinaryPrimitives.ReadUInt64BigEndian(bytes[36..]),
            BinaryPrimitives.ReadUInt64BigEndian(bytes[44..]));
        record.Validate(); return record;
    }
}

public static class AdminLifecycleProtocol
{
    public const ulong StartupNanoseconds = 30_000_000_000, PrivilegedNanoseconds = 5_000_000_000, ShutdownNanoseconds = 4_000_000_000;
    public static ulong Deadline(ulong now, ulong duration)
    {
        if (duration == 0 || duration > long.MaxValue || now > (ulong)long.MaxValue - duration)
            throw new LifecycleValidationException();
        return now + duration;
    }
    public static (ulong Privileged, ulong Startup) StartupDeadlines(ulong now)
    {
        var total = Deadline(now, StartupNanoseconds);
        return (total - (StartupNanoseconds - PrivilegedNanoseconds), total);
    }
    public static void CheckPrivilegedDeadline(ulong startupDeadline, ulong now)
    {
        if (startupDeadline < StartupNanoseconds || startupDeadline > long.MaxValue || now > long.MaxValue
            || now >= startupDeadline - (StartupNanoseconds - PrivilegedNanoseconds)) throw new LifecycleValidationException();
    }
    public static LifecycleMessage[][] DirectionOrder(string channel, LifecycleRole role) => (channel, role) switch
    {
        ("sr", LifecycleRole.Supervisor) => [[LifecycleMessage.RPrepare], [LifecycleMessage.RExpose], [LifecycleMessage.StopRouter]],
        ("sr", LifecycleRole.Router) => [[LifecycleMessage.RBound], [LifecycleMessage.RUiReady], [LifecycleMessage.RExposed], [LifecycleMessage.RProtocolDone], [LifecycleMessage.HostDisposed]],
        ("sg", LifecycleRole.Supervisor) => [[LifecycleMessage.GRelease], [LifecycleMessage.StopGuardian]],
        ("sg", LifecycleRole.Guardian) => [[LifecycleMessage.GBound], [LifecycleMessage.AgentResult, LifecycleMessage.AgentSignal, LifecycleMessage.AgentWaitFailure]],
        ("ra", LifecycleRole.Router) => [[LifecycleMessage.ABind], [LifecycleMessage.StartUi], [LifecycleMessage.StopUi]],
        ("ra", LifecycleRole.Agent) => [[LifecycleMessage.AReady], [LifecycleMessage.UiReady], [LifecycleMessage.UiStopped]],
        ("gate", LifecycleRole.Guardian) => [[LifecycleMessage.AExec]],
        ("gate", LifecycleRole.AgentBootstrap) => [[LifecycleMessage.AExecAck]],
        _ => []
    };
    public static bool IsInternal(LifecycleMessage m) => m is LifecycleMessage.GuardianObserved or LifecycleMessage.RouterObserved
        or LifecycleMessage.ChildrenAbsent or LifecycleMessage.GuardianSignal or LifecycleMessage.GuardianWaitFailure
        or LifecycleMessage.RouterSignal or LifecycleMessage.RouterWaitFailure;
    public static bool IsStartup(LifecycleMessage m) => m >= LifecycleMessage.GBound && m <= LifecycleMessage.RExposed;
    public static LifecycleRole? Sender(string channel, LifecycleMessage m) => (channel, m) switch
    {
        ("sr", LifecycleMessage.RPrepare or LifecycleMessage.RExpose or LifecycleMessage.StopRouter) => LifecycleRole.Supervisor,
        ("sr", LifecycleMessage.RBound or LifecycleMessage.RUiReady or LifecycleMessage.RExposed or LifecycleMessage.RProtocolDone or LifecycleMessage.HostDisposed) => LifecycleRole.Router,
        ("sg", LifecycleMessage.GRelease or LifecycleMessage.StopGuardian) => LifecycleRole.Supervisor,
        ("sg", LifecycleMessage.GBound or LifecycleMessage.AgentResult or LifecycleMessage.AgentSignal or LifecycleMessage.AgentWaitFailure) => LifecycleRole.Guardian,
        ("ra", LifecycleMessage.ABind or LifecycleMessage.StartUi or LifecycleMessage.StopUi) => LifecycleRole.Router,
        ("ra", LifecycleMessage.AReady or LifecycleMessage.UiReady or LifecycleMessage.UiStopped) => LifecycleRole.Agent,
        ("gate", LifecycleMessage.AExec) => LifecycleRole.Guardian,
        ("gate", LifecycleMessage.AExecAck) => LifecycleRole.AgentBootstrap,
        _ => null
    };
    public static bool IsPeer(string channel, LifecycleRole role) => (channel, role) is
        ("sr", LifecycleRole.Supervisor or LifecycleRole.Router) or
        ("sg", LifecycleRole.Supervisor or LifecycleRole.Guardian) or
        ("ra", LifecycleRole.Router or LifecycleRole.Agent);

    public static LifecycleRecord[] Metadata(byte[] data, LifecycleRole receiver, Guid launch,
        IReadOnlyDictionary<LifecycleMessage, (uint Sender, uint Subject)> bindings, ulong startupDeadline, ulong now = 0)
    {
        if (startupDeadline == 0 || startupDeadline > long.MaxValue || now >= startupDeadline || now > long.MaxValue) throw new LifecycleValidationException();
        LifecycleMessage[] order = receiver switch
        {
            LifecycleRole.Supervisor => [LifecycleMessage.MetaRouter, LifecycleMessage.MetaGuardian],
            LifecycleRole.Router => [LifecycleMessage.MetaRouter],
            LifecycleRole.Guardian or LifecycleRole.Agent => [LifecycleMessage.MetaRouter, LifecycleMessage.MetaAgent],
            _ => throw new LifecycleValidationException()
        };
        if (data.Length != order.Length * 64) throw new LifecycleValidationException();
        var result = new LifecycleRecord[order.Length];
        for (var i = 0; i < order.Length; i++)
        {
            var r = LifecycleRecord.Decode(data.AsSpan(i * 64, 64));
            if (!bindings.TryGetValue(order[i], out var binding)) throw new LifecycleValidationException();
            if (binding.Sender == 0 || binding.Sender > int.MaxValue || binding.Subject == 0 || binding.Subject > int.MaxValue)
                throw new LifecycleValidationException();
            var role = order[i] == LifecycleMessage.MetaAgent ? LifecycleRole.GuardianBootstrap : LifecycleRole.Root;
            if (r.Message != order[i] || r.Role != role || r.Sequence != i + 1 || r.SenderPid != binding.Sender
                || r.RelatedPid != binding.Subject || r.Launch != launch || r.StartupDeadline != startupDeadline
                || r.ShutdownDeadline != 0) throw new LifecycleValidationException();
            result[i] = r;
        }
        return result;
    }
    public static void ValidateDescriptors(LifecycleRole role, IReadOnlyDictionary<int, string> handles, bool metadataConsumed = false)
    {
        Dictionary<int, string> expected = role switch
        {
            LifecycleRole.Supervisor => new() { [3] = "sr", [4] = "sg", [5] = "metadata" },
            LifecycleRole.Guardian => new() { [3] = "sg", [4] = "gate", [5] = "metadata" },
            LifecycleRole.Router => new() { [3] = "credential", [4] = "ra", [5] = "sr", [6] = "metadata" },
            LifecycleRole.Agent => new() { [3] = "ra", [4] = "metadata" },
            _ => throw new LifecycleValidationException()
        };
        if (metadataConsumed)
            foreach (var fd in expected.Where(pair => pair.Value == "metadata").Select(pair => pair.Key).ToArray()) expected.Remove(fd);
        if (handles.Count != expected.Count || expected.Any(pair => !handles.TryGetValue(pair.Key, out var kind) || kind != pair.Value))
            throw new LifecycleValidationException();
    }
}

public sealed class LifecycleDirection
{
    private readonly string channel;
    private readonly LifecycleRole role;
    private readonly Guid launch;
    private readonly uint senderPid;
    private readonly ulong startupDeadline;
    public LifecycleDirection(string channel, LifecycleRole role, Guid launch, uint senderPid, ulong startupDeadline)
    {
        if (senderPid == 0 || senderPid > int.MaxValue || startupDeadline == 0 || startupDeadline > long.MaxValue)
            throw new LifecycleValidationException();
        this.channel = channel; this.role = role; this.launch = launch;
        this.senderPid = senderPid; this.startupDeadline = startupDeadline;
    }
    private uint next = 1;
    private readonly int[] counts = new int[2];
    private ulong? stop;
    private bool finished;
    private int position;
    public bool Failed { get; private set; }
    public void BindShutdown(ulong startedAt)
    {
        try
        {
            if (Failed || finished || stop.HasValue) throw new LifecycleValidationException();
            stop = AdminLifecycleProtocol.Deadline(startedAt, AdminLifecycleProtocol.ShutdownNanoseconds);
        }
        catch (LifecycleValidationException) { Failed = true; throw; }
    }
    public void Accept(LifecycleRecord? r, ulong now, LifecycleMessage expected, uint relatedPid)
    {
        try
        {
            if (r is null) throw new LifecycleValidationException();
            if (relatedPid == 0 || relatedPid > int.MaxValue) throw new LifecycleValidationException();
            r.Validate();
            var allowed = AdminLifecycleProtocol.Sender(channel, r.Message);
            if (r.Message == LifecycleMessage.Fail && AdminLifecycleProtocol.IsPeer(channel, role)) allowed = role;
            if (Failed || finished || allowed != role || r.Role != role || r.Message != expected || r.Launch != launch
                || r.SenderPid != senderPid || r.RelatedPid != relatedPid || r.Sequence != next
                || r.StartupDeadline != startupDeadline || now > long.MaxValue) throw new LifecycleValidationException();
            var order = AdminLifecycleProtocol.DirectionOrder(channel, role);
            if (r.Message != LifecycleMessage.Fail && (position >= order.Length || !order[position].Contains(r.Message)))
                throw new LifecycleValidationException();
            var stopping = r.ShutdownDeadline != 0;
            if (AdminLifecycleProtocol.IsStartup(r.Message) && stopping) throw new LifecycleValidationException();
            if (!AdminLifecycleProtocol.IsStartup(r.Message) && r.Message != LifecycleMessage.Fail && !stopping)
                throw new LifecycleValidationException();
            if (stopping)
            {
                if (!stop.HasValue || stop != r.ShutdownDeadline) throw new LifecycleValidationException();
                if (now < stop.Value - AdminLifecycleProtocol.ShutdownNanoseconds) throw new LifecycleValidationException();
            }
            else if (stop.HasValue) throw new LifecycleValidationException();
            if (now >= (stopping ? r.ShutdownDeadline : r.StartupDeadline)) throw new LifecycleValidationException();
            var index = stopping ? 1 : 0;
            if (counts[index] >= 16) throw new LifecycleValidationException();
            counts[index]++; next++; position++;
            if (r.Message == LifecycleMessage.Fail) throw new LifecycleValidationException();
        }
        catch (LifecycleValidationException) { Failed = true; throw; }
    }
    public void Eof(bool permitted = false, ulong? now = null)
    {
        if (Failed || finished || !permitted || channel != "ra" || role != LifecycleRole.Agent
            || position != AdminLifecycleProtocol.DirectionOrder("ra", LifecycleRole.Agent).Length
            || !stop.HasValue || !now.HasValue || now > long.MaxValue || now >= stop
            || now < stop.Value - AdminLifecycleProtocol.ShutdownNanoseconds)
        { Failed = true; throw new LifecycleValidationException(); }
        finished = true;
    }
}

public sealed class LifecycleLaunchGate
{
    private int state;
    public bool Failed { get; private set; }
    public bool MustCloseBeforeExec => state == 2 && !Failed;
    public void Accept(LifecycleRecord? r)
    {
        try
        {
            if (r is null) throw new LifecycleValidationException();
            r.Validate();
            var expected = state switch
            {
                0 => (LifecycleRole.Guardian, LifecycleMessage.AExec),
                1 => (LifecycleRole.AgentBootstrap, LifecycleMessage.AExecAck),
                _ => ((LifecycleRole)0, (LifecycleMessage)0)
            };
            if (Failed || state >= 2 || (r.Role, r.Message) != expected) throw new LifecycleValidationException();
            state++;
        }
        catch (LifecycleValidationException) { Failed = true; throw; }
    }
}

public sealed class LifecycleStartup
{
    private static readonly LifecycleMessage[] Order = [LifecycleMessage.GBound, LifecycleMessage.RPrepare,
        LifecycleMessage.RBound, LifecycleMessage.GRelease, LifecycleMessage.AExec, LifecycleMessage.AExecAck,
        LifecycleMessage.ABind, LifecycleMessage.AReady, LifecycleMessage.StartUi, LifecycleMessage.UiReady,
        LifecycleMessage.RUiReady, LifecycleMessage.RExpose, LifecycleMessage.RExposed];
    private int position;
    public bool Failed { get; private set; }
    public bool Running => !Failed && position == Order.Length;
    public void Accept(LifecycleMessage m)
    {
        if (Failed || position == Order.Length || m != Order[position])
        { Failed = true; throw new LifecycleValidationException(); }
        position++;
    }
}

public sealed class LifecycleFrames
{
    private byte[] pending = [];
    private bool failed;
    private bool finished;
    public LifecycleRecord[] Feed(ReadOnlySpan<byte> chunk)
    {
        if (failed || finished || chunk.Length > 64) { failed = true; throw new LifecycleValidationException(); }
        var buffer = new byte[pending.Length + chunk.Length];
        pending.CopyTo(buffer, 0); chunk.CopyTo(buffer.AsSpan(pending.Length));
        try
        {
            var result = buffer.Length < 64 ? [] : new[] { LifecycleRecord.Decode(buffer.AsSpan(0, 64)) };
            pending = buffer.Length < 64 ? buffer : buffer[64..]; return result;
        }
        catch (LifecycleValidationException) { failed = true; throw; }
    }
    public void Eof()
    {
        if (failed || finished || pending.Length != 0) { failed = true; throw new LifecycleValidationException(); }
        finished = true;
    }
}

// Offline order model only. Injected observations do not qualify actual kernel waits.
public sealed class LifecycleShutdown
{
    private readonly ulong startedAt, startupDeadline;
    private readonly uint agentPid, guardianPid, routerPid;
    private readonly Guid launch;
    private LifecycleRecord? report;
    public ulong Deadline { get; }
    public LifecycleShutdown(ulong startedAt, long agentPid, long guardianPid, long routerPid, Guid launch, ulong startupDeadline)
    {
        if (agentPid is < 1 or > int.MaxValue || guardianPid is < 1 or > int.MaxValue || routerPid is < 1 or > int.MaxValue
            || agentPid == guardianPid || agentPid == routerPid || guardianPid == routerPid || launch == Guid.Empty
            || startupDeadline is 0 or > long.MaxValue) throw new LifecycleValidationException();
        Deadline = AdminLifecycleProtocol.Deadline(startedAt, AdminLifecycleProtocol.ShutdownNanoseconds);
        this.startedAt = startedAt; this.agentPid = (uint)agentPid; this.guardianPid = (uint)guardianPid;
        this.routerPid = (uint)routerPid; this.launch = launch; this.startupDeadline = startupDeadline;
    }
    private readonly HashSet<LifecycleMessage> events = [];
    private readonly HashSet<string> waits = [];
    private bool eof, absence;
    public bool Failed { get; private set; }
    public bool CompleteModel => !Failed && absence;
    private void Check(ulong now)
    {
        if (Failed || now < startedAt || now >= Deadline || now > long.MaxValue) { Failed = true; throw new LifecycleValidationException(); }
    }
    public void Event(LifecycleMessage message, ulong now)
    {
        Check(now);
        var valid = message switch
        {
            LifecycleMessage.StopRouter or LifecycleMessage.StopGuardian => true,
            LifecycleMessage.StopUi => events.Contains(LifecycleMessage.StopRouter),
            LifecycleMessage.UiStopped => events.Contains(LifecycleMessage.StopUi),
            LifecycleMessage.RProtocolDone => eof,
            LifecycleMessage.HostDisposed => events.Contains(LifecycleMessage.RProtocolDone),
            _ => false
        };
        if (!valid || !events.Add(message)) { Failed = true; throw new LifecycleValidationException(); }
    }
    public void LifecycleEof(ulong now)
    {
        Check(now);
        if (eof || !events.Contains(LifecycleMessage.UiStopped)) { Failed = true; throw new LifecycleValidationException(); }
        eof = true;
    }
    public void AgentReport(LifecycleRecord? r, ulong now, string channel)
    {
        try
        {
            Check(now);
            if (r is null) throw new LifecycleValidationException();
            r.Validate();
            if (report is not null || !events.Contains(LifecycleMessage.StopGuardian) || channel != "sg"
                || r.Role != LifecycleRole.Guardian || r.Message != LifecycleMessage.AgentResult || r.Outcome != 0
                || r.Launch != launch || r.Sequence != 2 || r.SenderPid != guardianPid || r.RelatedPid != agentPid
                || r.StartupDeadline != startupDeadline || r.ShutdownDeadline != Deadline) throw new LifecycleValidationException();
            report = r;
        }
        catch (LifecycleValidationException) { Failed = true; throw; }
    }
    public void WaitObservation(string subject, long returnedPid, bool normalExit, int exitCode, ulong now)
    {
        Check(now);
        var expected = subject switch { "agent" => agentPid, "guardian" => guardianPid, "router" => routerPid, _ => 0u };
        var ready = subject == "agent" ? report is not null : subject == "guardian" ? events.Contains(LifecycleMessage.AgentResult)
            : events.Contains(LifecycleMessage.HostDisposed);
        if (returnedPid is < 1 or > int.MaxValue || expected == 0 || returnedPid != expected || !normalExit || exitCode != 0
            || !ready || !waits.Add(subject)) { Failed = true; throw new LifecycleValidationException(); }
        if (subject == "agent") events.Add(LifecycleMessage.AgentResult);
    }
    public void Absence(bool noChildren, ulong now)
    {
        Check(now);
        if (absence || !eof || !noChildren || !waits.SetEquals(["agent", "guardian", "router"]))
        { Failed = true; throw new LifecycleValidationException(); }
        absence = true;
    }
}
