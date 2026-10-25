using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Xunit;

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
}
