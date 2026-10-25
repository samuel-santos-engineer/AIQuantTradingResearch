namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

/// <summary>Local process-lifetime ownership only; not a cross-App-Service lease.</summary>
internal sealed class ResearchWriterOwnership : IDisposable
{
    private readonly FileStream handle;
    internal string DirectoryPath { get; }
    internal ResearchWriterOwnership(string directory)
    {
        ResearchStorageBoundary.Validate(directory);
        DirectoryPath = Path.GetFullPath(directory);
        // Require an exclusive, non-symlink research directory, not a shared historical DB path.
        Directory.CreateDirectory(DirectoryPath);
        for (var current = new DirectoryInfo(DirectoryPath); current is not null; current = current.Parent)
            if ((current.Attributes & FileAttributes.ReparsePoint) != 0) throw new IOException("Research unavailable.");
        foreach (var item in Directory.EnumerateFileSystemEntries(DirectoryPath))
            if (!Allowed(Path.GetFileName(item)) || Directory.Exists(item) || (File.GetAttributes(item) & FileAttributes.ReparsePoint) != 0)
                throw new IOException("Research unavailable.");
        Private(DirectoryPath, true);
        var path = Path.Combine(DirectoryPath, "writer.lock");
        if (File.Exists(path) && (File.GetAttributes(path) & FileAttributes.ReparsePoint) != 0) throw new IOException("Research unavailable.");
        handle = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        try { Private(path, false); }
        catch { handle.Dispose(); throw; }
    }
    internal void ProtectFiles()
    {
        foreach (var path in Directory.EnumerateFiles(DirectoryPath))
        {
            if (!Allowed(Path.GetFileName(path)) || (File.GetAttributes(path) & FileAttributes.ReparsePoint) != 0) throw new IOException("Research unavailable.");
            Private(path, false);
        }
    }
    private static bool Allowed(string name) => name is "research.sqlite" or "research.sqlite-wal" or "research.sqlite-shm" or
        "writer.lock" or "watermark.json" or "watermark.json.pending";
    private static void Private(string path, bool directory)
    {
        if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(path, directory ?
            UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute : UnixFileMode.UserRead | UnixFileMode.UserWrite);
    }
    public void Dispose() => handle.Dispose(); // Keep lock file inode; never unlink a held ownership path.
}
