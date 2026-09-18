using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class TwelveDataConfigurationTests
{
    [Fact]
    public void DefaultTimeoutIsFiniteAndWithinTheConfiguredMaximum()
    {
        var configuration = new TwelveDataConfiguration("test-placeholder-key");

        Assert.Equal(TimeSpan.FromSeconds(TwelveDataConfiguration.DefaultRequestTimeoutSeconds), configuration.RequestTimeout);
        Assert.InRange(
            TwelveDataConfiguration.DefaultRequestTimeoutSeconds,
            1,
            TwelveDataConfiguration.MaximumRequestTimeoutSeconds);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(31)]
    public void OutOfRangeTimeoutIsRejected(int timeoutSeconds) =>
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new TwelveDataConfiguration("test-placeholder-key", timeoutSeconds));
}
