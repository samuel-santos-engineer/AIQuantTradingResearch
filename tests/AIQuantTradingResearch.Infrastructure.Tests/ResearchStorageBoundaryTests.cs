using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Xunit;
using System.Runtime.Versioning;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ResearchStorageBoundaryTests
{
    [Theory]
    [InlineData("WEBSITE_SITE_NAME", "staging")]
    [InlineData("WEBSITE_INSTANCE_ID", "instance-b")]
    [InlineData("WEBSITE_HOSTNAME", "site.azurewebsites.net")]
    [InlineData("WEBSITE_ROLE_INSTANCE_ID", "0")]
    [InlineData("WEBSITE_SITE_NAME", "")]
    [InlineData("AIQ_RESEARCH_TRANSPORT", "azure-app-service-http")]
    [InlineData("AIQ_RESEARCH_STORAGE_SCOPE", "durable")]
    public void AppServiceOrUnknownStorageCannotOptIntoWritableLocalStore(string name, string value) =>
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateHost(k => k == name ? value : null));
    [Fact] public void LocalScopeIsQualificationNotAzureOverride()
    {
        ResearchStorageBoundary.ValidateHost(_ => null);
        ResearchStorageBoundary.ValidateHost(k => k == "AIQ_RESEARCH_STORAGE_SCOPE" ? "local-qualification" : null);
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateHost(k => k switch
        { "AIQ_RESEARCH_STORAGE_SCOPE" => "local-qualification", "WEBSITE_INSTANCE_ID" => "instance-a", _ => null }));
    }
    private const string Uid = "Name: probe\nUid:\t1654\t1654\t1654\t1654\n";
    private static string[] Mounts(string type = "ext4") => ["1 0 0:1 / / rw - overlay overlay rw", $"2 1 8:1 /research /private rw - {type} /dev/test rw"];
    [Theory] [InlineData("cifs")] [InlineData("nfs")] [InlineData("nfs4")] [InlineData("tmpfs")] [InlineData("fuse.blobfuse2")] [InlineData("unknown")]
    public void NetworkEphemeralOrUnknownMountedStoreIsRejected(string type) =>
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux("/private/data", Mounts(type), Uid, "/private"));
    [Fact] public void DeepestMountAndExpectedIdentityAreChecked()
    {
        var mount = ResearchStorageBoundary.ValidateLinux("/private/data", Mounts(), Uid, "/private");
        Assert.Equal("8:1", mount.Device); Assert.Equal("/research", mount.Root); Assert.Equal("ext4", mount.FileSystem);
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux("/private/data", Mounts(), Uid, "/other"));
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux("/private/data", [..Mounts(), "3 2 0:2 / /private/data rw - tmpfs tmpfs rw"], Uid, "/private"));
    }
    [Theory] [InlineData("0")] [InlineData("12002")]
    public void WrongUidCannotOpenLinuxWriter(string uid) => Assert.Throws<ResearchUnavailableException>(() =>
        ResearchStorageBoundary.ValidateLinux("/private/data", Mounts(), Uid.Replace("1654", uid, StringComparison.Ordinal), null));
    [Fact] public void ReadOnlyStateMountFailsBeforeOpeningWritableState() => Assert.Throws<ResearchUnavailableException>(() =>
        ResearchStorageBoundary.ValidateLinux("/private/data", Mounts().Select(x => x.Replace("/private rw", "/private ro", StringComparison.Ordinal)), Uid, "/private"));
    [Fact] public void MissingOrMalformedKernelIdentityFailsClosed()
    {
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux("/private/data", [], Uid, null));
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux("/private/data", ["bad"], Uid, null));
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux("/private/data", Mounts(), "", null));
    }

    private static Dictionary<string, string?> Staging() => new()
    {
        ["AIQ_RESEARCH_STORAGE_SCOPE"] = ResearchStorageBoundary.StagingScope,
        ["AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING"] = "1",
        ["WEBSITE_SITE_NAME"] = "aiqr115researchstagingwus27f5eabb5",
        ["WEBSITE_HOSTNAME"] = "aiqr115researchstagingwus27f5eabb5.azurewebsites.net",
        ["WEBSITE_INSTANCE_ID"] = "offline-simulated-instance",
        ["WEBSITE_ROLE_INSTANCE_ID"] = "0",
        ["WEBSITES_ENABLE_APP_SERVICE_STORAGE"] = "false",
        ["AIQ_RESEARCH_EXPECTED_MOUNT_PATH"] = "/"
    };
    [Fact] public void OnlyCompleteExplicitStagingIdentitySelectsEphemeralMode()
    {
        var settings = Staging();
        Assert.True(ResearchStorageBoundary.ValidateHost(k => settings.GetValueOrDefault(k)));
        foreach (var name in settings.Keys)
        {
            var incomplete = Staging(); incomplete.Remove(name);
            Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateHost(k => incomplete.GetValueOrDefault(k)));
        }
    }
    [Theory]
    [InlineData("WEBSITE_SITE_NAME", "production")]
    [InlineData("WEBSITE_SITE_NAME", "aiqr112wp05wus27f5eabb5")]
    [InlineData("WEBSITE_HOSTNAME", "production.azurewebsites.net")]
    [InlineData("WEBSITE_INSTANCE_ID", " ")]
    [InlineData("WEBSITE_ROLE_INSTANCE_ID", "1")]
    [InlineData("AIQ_RESEARCH_STORAGE_SCOPE", "local-qualification")]
    [InlineData("AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING", "true")]
    [InlineData("WEBSITES_ENABLE_APP_SERVICE_STORAGE", "true")]
    [InlineData("AIQ_RESEARCH_EXPECTED_MOUNT_PATH", "/private")]
    public void PartialWrongOrProductionLikeOptInFails(string name, string value)
    {
        var settings = Staging(); settings[name] = value;
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateHost(k => settings.GetValueOrDefault(k)));
    }
    [Fact] public void ContainerRootOverlayIsTheOnlyStagingMountCandidate()
    {
        var mount = ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory,
            ["1 0 0:1 / / rw - overlay overlay rw"], Uid, "/", true);
        Assert.Equal("/", mount.Path); Assert.Equal("overlay", mount.FileSystem);
    }
    [Theory]
    [InlineData("cifs")] [InlineData("nfs")] [InlineData("nfs4")] [InlineData("ext4")]
    [InlineData("xfs")] [InlineData("tmpfs")] [InlineData("fuse.blobfuse2")]
    public void StagingRejectsNonOverlayRootAndNestedMounts(string type)
    {
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory,
            [$"1 0 0:1 / / rw - {type} source rw"], Uid, "/", true));
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory,
            ["1 0 0:1 / / rw - overlay overlay rw", $"2 1 0:2 / /tmp rw - {type} source rw"], Uid, "/", true));
    }
    [Theory] [InlineData("/home/research")] [InlineData("/private/data")]
    [InlineData("/tmp/aiq-research/../aiq-research")] [InlineData("/tmp/aiq-research/subdir")]
    public void StagingRejectsOtherPaths(string path) => Assert.Throws<ResearchUnavailableException>(() =>
        ResearchStorageBoundary.ValidateLinux(path, ["1 0 0:1 / / rw - overlay overlay rw"], Uid, "/", true));
    [Theory]
    [InlineData("1 0 0:1 / / ro - overlay overlay rw")]
    [InlineData("1 0 0:1 /bound / rw - overlay overlay rw")]
    [InlineData("1 0 8:1 / / rw - overlay overlay rw")]
    [InlineData("1 0 0:1 / / rw shared:7 - overlay overlay rw")]
    [InlineData("1 0 0:1 / / rw master:7 - overlay overlay rw")]
    [InlineData("bad")]
    public void StagingRejectsUnverifiableRootEvidence(string line) => Assert.Throws<ResearchUnavailableException>(() =>
        ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory, [line], Uid, "/", true));
    [Fact] public void StagingRejectsMissingEvidenceWrongUidAndMountedDatabaseFile()
    {
        var root = new[] { "1 0 0:1 / / rw - overlay overlay rw" };
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory, [], Uid, "/", true));
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory, root, "", "/", true));
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory, root, Uid.Replace("1654", "0"), "/", true));
        Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.ValidateLinux(ResearchStorageBoundary.StagingDirectory,
            [..root, "2 1 8:1 / /tmp/aiq-research/research.sqlite rw - ext4 source rw"], Uid, "/", true));
    }
    [Fact]
    [SupportedOSPlatform("linux")]
    public void LinuxPrivateDirectoryRejectsSymlinksAndBroadPermissions()
    {
        if (!OperatingSystem.IsLinux()) return; // Actual Linux branch is exercised by container smoke, not a Windows pass claim.
        var parent = Path.Combine(Path.GetTempPath(), "aiq-wp09-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(parent);
        try
        {
            var data = Path.Combine(parent, "data");
            ResearchStorageBoundary.VerifyPrivateWritableDirectory(data);
            Assert.Empty(Directory.EnumerateFileSystemEntries(data));
            Directory.CreateSymbolicLink(Path.Combine(parent, "link"), data);
            Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.VerifyPrivateWritableDirectory(Path.Combine(parent, "link")));
            File.CreateSymbolicLink(Path.Combine(data, "research.sqlite"), Path.Combine(parent, "missing"));
            Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.VerifyPrivateWritableDirectory(data));
            File.Delete(Path.Combine(data, "research.sqlite"));
            File.SetUnixFileMode(data, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute | UnixFileMode.OtherRead);
            Assert.Throws<ResearchUnavailableException>(() => ResearchStorageBoundary.VerifyPrivateWritableDirectory(data));
        }
        finally { Directory.Delete(parent, true); }
    }
    [Fact] public void DiagnosticHasOnlyFixedBooleanFieldsAndDoesNotAdmitLocalHost()
    {
        var result = ResearchStorageBoundary.ProbeStaging();
        Assert.Equal("accepted,hostOptIn,kernel,linux,privateWritable,probeCleanup", string.Join(",", result.Keys.Order(StringComparer.Ordinal)));
        Assert.False(result["accepted"]); Assert.False(result["hostOptIn"]); Assert.True(result["probeCleanup"]);
    }
}
