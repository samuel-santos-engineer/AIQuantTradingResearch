using AIQuantTradingResearch.Application.Integration;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class CapabilityInvocationContractsTests
{
    [Fact]
    public void SuccessKeepsTheTechnologyNeutralPayload()
    {
        var payload = new Dictionary<string, string> { ["status"] = "available" };

        var result = CapabilityInvocationResult.Success(payload);

        Assert.True(result.IsSuccess);
        Assert.Equal(CapabilityInvocationFailure.None, result.Failure);
        Assert.Equal("available", result.Payload!["status"]);
        Assert.Null(result.Message);
    }

    [Fact]
    public void FailureRequiresANonSuccessFailureCategory()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => CapabilityInvocationResult.Failed(CapabilityInvocationFailure.None, "invalid"));

        var result = CapabilityInvocationResult.Failed(
            CapabilityInvocationFailure.Timeout,
            "bounded timeout");

        Assert.False(result.IsSuccess);
        Assert.Equal(CapabilityInvocationFailure.Timeout, result.Failure);
        Assert.Null(result.Payload);
    }
}
