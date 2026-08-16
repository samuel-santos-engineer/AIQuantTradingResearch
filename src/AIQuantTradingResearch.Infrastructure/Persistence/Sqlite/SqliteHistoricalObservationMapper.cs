using System.Globalization;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteHistoricalObservationMapper
{
    public static SqliteHistoricalObservationRecord ToRecord(
        string target,
        PriceObservation observation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(target);
        ArgumentNullException.ThrowIfNull(observation);

        var offsetMinutes = checked((short)observation.Instant.Offset.TotalMinutes);

        return new SqliteHistoricalObservationRecord(
            target,
            observation.Instant.UtcTicks,
            offsetMinutes,
            observation.Price.ToString("G29", CultureInfo.InvariantCulture));
    }

    public static PriceObservation ToObservation(SqliteHistoricalObservationRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        var offset = TimeSpan.FromMinutes(record.OffsetMinutes);
        var instant = new DateTimeOffset(record.InstantUtcTicks, TimeSpan.Zero).ToOffset(offset);
        var price = decimal.Parse(
            record.PriceText,
            NumberStyles.Float,
            CultureInfo.InvariantCulture);

        return new PriceObservation(instant, price);
    }
}
