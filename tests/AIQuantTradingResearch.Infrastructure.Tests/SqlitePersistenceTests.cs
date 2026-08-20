using System.Globalization;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class SqlitePersistenceTests
{
    private static readonly DateTimeOffset FirstInstant =
        new(2024, 3, 10, 0, 0, 0, TimeSpan.FromHours(-4));

    [Fact]
    public void OpenConnectionBootstrapsExactVersionTwoStrictWithoutRowIdSchema()
    {
        using var database = new TestDatabase();
        using var connection = database.Factory.OpenConnection();

        Assert.Equal(2L, Scalar<long>(connection, "PRAGMA user_version;"));
        Assert.Equal(4L, Scalar<long>(connection, "SELECT COUNT(*) FROM pragma_table_info('historical_observations');"));
        using (var columns = connection.CreateCommand())
        {
            columns.CommandText = "PRAGMA table_info(historical_observations);";
            using var reader = columns.ExecuteReader();
            var actual = new List<(string Name, string Type, long NotNull)>();
            while (reader.Read())
            {
                actual.Add((reader.GetString(1), reader.GetString(2), reader.GetInt64(3)));
            }

            Assert.Equal(
                [("target", "TEXT", 1L), ("instant_utc_ticks", "INTEGER", 1L), ("offset_minutes", "INTEGER", 1L), ("price_text", "TEXT", 1L)],
                actual);
        }
        var definition = Scalar<string>(connection, "SELECT sql FROM sqlite_schema WHERE name = 'historical_observations';");
        Assert.Contains("STRICT, WITHOUT ROWID", definition, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("target TEXT COLLATE BINARY NOT NULL", definition, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("PRIMARY KEY (target, instant_utc_ticks)", definition, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void OpenConnectionIsIdempotentReturnsSeparateOpenConnectionsAndCallerDisposalClosesThem()
    {
        using var database = new TestDatabase();
        var first = database.Factory.OpenConnection();
        var second = database.Factory.OpenConnection();

        Assert.NotSame(first, second);
        Assert.Equal(System.Data.ConnectionState.Open, first.State);
        first.Dispose();
        Assert.Equal(System.Data.ConnectionState.Closed, first.State);
        second.Dispose();
    }

    [Fact]
    public void OpenConnectionWhenUnsupportedVersionExistsRejectsWithoutReplacingState()
    {
        using var database = new TestDatabase();
        using (var connection = new SqliteConnection($"Data Source={database.Path}"))
        {
            connection.Open();
            Execute(connection, "PRAGMA user_version = 3;");
        }

        Assert.Throws<InvalidOperationException>(() => database.Factory.OpenConnection());
        using var verification = new SqliteConnection($"Data Source={database.Path}");
        verification.Open();
        Assert.Equal(3L, Scalar<long>(verification, "PRAGMA user_version;"));
    }

    [Fact]
    public void BootstrapIsIdempotentPreservesAcceptedDataAndRejectsIncompatibleVersionZeroSchema()
    {
        using var database = new TestDatabase();
        var observation = Observation(0, 100m);
        database.Store.Persist("TARGET", [observation]);
        using (database.Factory.OpenConnection())
        {
        }

        Assert.Equal([observation], database.Store.Retrieve("TARGET").Observations);

        using var incompatible = new TestDatabase();
        using (var connection = new SqliteConnection($"Data Source={incompatible.Path}"))
        {
            connection.Open();
            Execute(connection, "CREATE TABLE historical_observations (target TEXT);");
        }

        Assert.Throws<InvalidOperationException>(() => incompatible.Factory.OpenConnection());
        using var verification = new SqliteConnection($"Data Source={incompatible.Path}");
        verification.Open();
        Assert.Equal(1L, Scalar<long>(verification, "SELECT COUNT(*) FROM sqlite_schema WHERE name = 'historical_observations';"));
    }

    [Fact]
    public void MapperRoundTripPreservesTargetOffsetAndExtremeDecimalsAcrossCulture()
    {
        var originalCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
            var observation = new PriceObservation(FirstInstant, decimal.MaxValue);

            var record = SqliteHistoricalObservationMapper.ToRecord(" AAPL/Exact ", observation);
            var reconstructed = SqliteHistoricalObservationMapper.ToObservation(record);

            Assert.Equal(" AAPL/Exact ", record.Target);
            Assert.Equal(observation, reconstructed);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Fact]
    public void MapperWhenRecordIsMalformedRejectsInvalidDecimalAndOffset()
    {
        Assert.Throws<FormatException>(() => SqliteHistoricalObservationMapper.ToObservation(
            new SqliteHistoricalObservationRecord("TARGET", FirstInstant.UtcTicks, 0, "bad")));
        Assert.Throws<ArgumentOutOfRangeException>(() => SqliteHistoricalObservationMapper.ToObservation(
            new SqliteHistoricalObservationRecord("TARGET", FirstInstant.UtcTicks, 900, "1")));
    }

    [Fact]
    public void PersistNewMultiRowBatchAndRetrievePreservesExactAscendingHistory()
    {
        using var database = new TestDatabase();
        var observations = new[] { Observation(2, 12.34567890123456789012345678m), Observation(0, 0.0000000000000000000000000001m) };

        var result = database.Store.Persist(" AAPL ", observations);
        var history = database.Store.Retrieve(" AAPL ");

        Assert.Equal(ObservationPersistenceOutcome.NewlyAccepted, result.Outcome);
        Assert.True(history.IsSuccess);
        Assert.Equal(observations.OrderBy(static observation => observation.Instant), history.Observations);
    }

    [Fact]
    public void PersistEquivalentAndMixedBatchesAreIdempotentOrNewlyAcceptedWithoutMutation()
    {
        using var database = new TestDatabase();
        var first = Observation(0, 100m);
        var second = Observation(1, 101m);

        Assert.Equal(ObservationPersistenceOutcome.NewlyAccepted, database.Store.Persist("TARGET", [first]).Outcome);
        Assert.Equal(ObservationPersistenceOutcome.Idempotent, database.Store.Persist("TARGET", [first]).Outcome);
        Assert.Equal(ObservationPersistenceOutcome.NewlyAccepted, database.Store.Persist("TARGET", [first, second]).Outcome);
        Assert.Equal([first, second], database.Store.Retrieve("TARGET").Observations);
    }

    [Fact]
    public void PersistConflictsForDecimalOrOffsetAndLeavesOriginalHistoryUntouched()
    {
        using var database = new TestDatabase();
        var original = Observation(0, 100m);
        Assert.Equal(ObservationPersistenceOutcome.NewlyAccepted, database.Store.Persist("TARGET", [original]).Outcome);

        var differentDecimal = new PriceObservation(original.Instant, 101m);
        var sameInstantDifferentOffset = new PriceObservation(original.Instant.ToOffset(TimeSpan.FromHours(-3)), 100m);

        Assert.Equal(ObservationPersistenceOutcome.Conflict, database.Store.Persist("TARGET", [differentDecimal]).Outcome);
        Assert.Equal(ObservationPersistenceOutcome.Conflict, database.Store.Persist("TARGET", [sameInstantDifferentOffset]).Outcome);
        Assert.Equal([original], database.Store.Retrieve("TARGET").Observations);
    }

    [Fact]
    public void PersistBatchWithLaterConflictRollsBackEarlierNewObservation()
    {
        using var database = new TestDatabase();
        var original = Observation(1, 100m);
        Assert.Equal(ObservationPersistenceOutcome.NewlyAccepted, database.Store.Persist("TARGET", [original]).Outcome);

        var result = database.Store.Persist("TARGET", [Observation(0, 99m), new PriceObservation(original.Instant, 101m)]);

        Assert.Equal(ObservationPersistenceOutcome.Conflict, result.Outcome);
        Assert.Equal([original], database.Store.Retrieve("TARGET").Observations);
    }

    [Fact]
    public void RetrieveReturnsSuccessfulEmptyHistoryAndExactBinaryTargetIsolation()
    {
        using var database = new TestDatabase();
        var observation = Observation(0, 100m);
        database.Store.Persist("Target", [observation]);
        database.Store.Persist(" target", [Observation(1, 101m)]);

        var empty = database.Store.Retrieve("TARGET");

        Assert.True(empty.IsSuccess);
        Assert.Empty(empty.Observations!);
        Assert.Equal([observation], database.Store.Retrieve("Target").Observations);
        Assert.Single(database.Store.Retrieve(" target").Observations!);
    }

    [Fact]
    public void StoreMapsUnavailableOpenFailureWithoutLeakingSqliteDetails()
    {
        var store = new SqliteHistoricalObservationStore(new ThrowingConnectionFactory());

        Assert.Equal(PersistenceFailure.Unavailable, store.Persist("TARGET", [Observation(0, 1m)]).Failure);
        Assert.Equal(PersistenceFailure.Unavailable, store.Retrieve("TARGET").Failure);
    }

    [Fact]
    public void RetrieveMalformedPersistedRowMapsToInvalidDataWithoutRepairingIt()
    {
        using var database = new TestDatabase();
        using (var connection = database.Factory.OpenConnection())
        {
            Execute(connection, "PRAGMA ignore_check_constraints = ON;");
            Execute(connection, $"INSERT INTO historical_observations VALUES ('TARGET', {FirstInstant.UtcTicks}, 0, 'malformed');");
        }

        var result = database.Store.Retrieve("TARGET");

        Assert.False(result.IsSuccess);
        Assert.Equal(PersistenceFailure.InvalidData, result.Failure);
        using var verification = database.Factory.OpenConnection();
        Assert.Equal(1L, Scalar<long>(verification, "SELECT COUNT(*) FROM historical_observations;"));
    }

    [Fact]
    public void AddInfrastructureWithStorageRegistersExpectedGraphWithoutResolutionTimeDatabaseCreation()
    {
        using var database = new TestDatabase(createDirectory: false);
        var services = new ServiceCollection();
        services.AddInfrastructure(new TwelveDataConfiguration("offline-placeholder"), database.Configuration);

        _ = Assert.Single(
            services,
            static descriptor => descriptor.ServiceType == typeof(IHistoricalObservationStore));
        using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        _ = provider.GetRequiredService<IHistoricalObservationStore>();
        _ = provider.GetRequiredService<ISqliteConnectionFactory>();
        _ = provider.GetRequiredService<IObservationSource>();
        Assert.False(File.Exists(database.Path));
    }

    private static PriceObservation Observation(int dayOffset, decimal price) =>
        new(FirstInstant.AddDays(dayOffset), price);

    private static void Execute(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    private static T Scalar<T>(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return (T)command.ExecuteScalar()!;
    }

    private sealed class ThrowingConnectionFactory : ISqliteConnectionFactory
    {
        public SqliteConnection OpenConnection() => throw new InvalidOperationException("offline failure");
    }

    private sealed class TestDatabase : IDisposable
    {
        private readonly string directory;

        public TestDatabase(bool createDirectory = true)
        {
            directory = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(),
                $"aiq-wp14-{Guid.NewGuid():N}");
            if (createDirectory)
            {
                Directory.CreateDirectory(directory);
            }

            Path = System.IO.Path.Combine(directory, "history.db");
            Configuration = new SqliteStorageConfiguration(Path);
            Factory = new SqliteConnectionFactory(Configuration);
            Store = new SqliteHistoricalObservationStore(Factory);
        }

        public string Path { get; }

        public SqliteStorageConfiguration Configuration { get; }

        public SqliteConnectionFactory Factory { get; }

        public SqliteHistoricalObservationStore Store { get; }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive: true);
            }
        }
    }
}
