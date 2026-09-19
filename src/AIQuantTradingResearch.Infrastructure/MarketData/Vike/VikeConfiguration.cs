namespace AIQuantTradingResearch.Infrastructure.MarketData.Vike;

public sealed class VikeConfiguration
{
    public const string SectionName = "Vike";
    public const string ApiKeyName = "ApiKey";

    public VikeConfiguration(string apiKey, Uri? baseAddress = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        var effectiveBaseAddress = baseAddress ?? new Uri("https://vike.io/", UriKind.Absolute);
        if (!effectiveBaseAddress.IsAbsoluteUri ||
            !string.Equals(effectiveBaseAddress.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Vike base address must be an absolute HTTPS URI.", nameof(baseAddress));
        }

        ApiKey = apiKey;
        BaseAddress = effectiveBaseAddress;
    }

    internal string ApiKey { get; }

    internal Uri BaseAddress { get; }
}
