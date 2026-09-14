using System.Globalization;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.Json;
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
    public void RetrieveEmitsBoundedProviderActivityAndMetricsWithoutTargetData()
    {
        using var database = new TestDatabase();
        database.Store.Persist("SECRET-TARGET", [Observation(0, 1m)]);
        var activities = new List<Activity>();
        using var parent = new Activity("HistoricalObservationRetrieval").Start();
        using var listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == "AIQuantTradingResearch.Infrastructure",
            Sample = static (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
            ActivityStopped = activity => activities.Add(activity),
        };
        ActivitySource.AddActivityListener(listener);
        var instruments = new List<(string Name, string? Unit)>();
        using var meter = new MeterListener();
        meter.InstrumentPublished = (instrument, observer) =>
        {
            if (instrument.Meter.Name == "AIQuantTradingResearch.Infrastructure") { instruments.Add((instrument.Name, instrument.Unit)); observer.EnableMeasurementEvents(instrument); }
        };
        meter.Start();

        Assert.True(database.Store.Retrieve("SECRET-TARGET").IsSuccess);

        var activity = Assert.Single(activities, item => item.OperationName == "provider.operation");
        Assert.Equal(parent.SpanId, activity.ParentSpanId);
        Assert.Equal(parent.TraceId, activity.TraceId);
        Assert.Equal("historical-observation.retrieve", activity.GetTagItem("aiq.operation"));
        Assert.Equal("success", activity.GetTagItem("aiq.outcome"));
        Assert.DoesNotContain(activity.Tags, tag => tag.Value?.Contains("SECRET-TARGET", StringComparison.Ordinal) == true);
        Assert.Contains(("provider.operations", "{operation}"), instruments);
        Assert.Contains(("provider.duration", "ms"), instruments);
        Assert.Contains(("provider.failures", "{operation}"), instruments);
    }

    [Fact]
    public void OpenConnectionBootstrapsExactVersionTwoStrictWithoutRowIdSchema()
    {
        using var database = new TestDatabase();
        using var connection = database.Factory.OpenConnection();

        Assert.Equal(4L, Scalar<long>(connection, "PRAGMA user_version;"));
        Assert.Equal(1L, Scalar<long>(connection, "SELECT COUNT(*) FROM sqlite_schema WHERE type = 'table' AND name = 'experiment_results';"));
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
    public void OpenConnectionCreatesMissingParentEnforcesDeleteJournalAndPreservesAcceptedHistoryAcrossReopen()
    {
        using var database = new TestDatabase(
            createDirectory: false,
            createParentDirectoryForInitialization: true);
        string parent = Path.GetDirectoryName(database.Path)!;
        var observation = Observation(0, 123.4567890123456789012345678m);

        Assert.False(Directory.Exists(parent));
        using (var connection = database.Factory.OpenConnection())
        {
            Assert.True(Directory.Exists(parent));
            Assert.Equal("delete", Scalar<string>(connection, "PRAGMA journal_mode;"), ignoreCase: true);
            Assert.Equal(4L, Scalar<long>(connection, "PRAGMA user_version;"));
        }

        Assert.Equal(ObservationPersistenceOutcome.NewlyAccepted, database.Store.Persist("WP04", [observation]).Outcome);
        SqliteConnection.ClearAllPools();

        using (var reopened = database.Factory.OpenConnection())
        {
            Assert.Equal("delete", Scalar<string>(reopened, "PRAGMA journal_mode;"), ignoreCase: true);
            Assert.Equal(4L, Scalar<long>(reopened, "PRAGMA user_version;"));
        }

        Assert.Equal([observation], database.Store.Retrieve("WP04").Observations);
    }

    [Fact]
    public void QualificationEvidenceArtifactMatchesStdoutAndAtomicallyReplacesPriorArtifact()
    {
        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp04-evidence-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        string database = Path.Combine(root, "data", "qualification.sqlite");
        string evidence = Path.Combine(root, "evidence", "record.json");
        try
        {
            var first = RunQualification(database, evidence, "initialize", "run-initialize");
            Assert.Equal(0, first.ExitCode);
            Assert.Equal(first.StandardOutput.Trim(), File.ReadAllText(evidence));
            AssertDiagnosticSequence(
                first.StandardError,
                "QUALIFICATION_ENTERED",
                "SQLITE_QUALIFICATION_STARTED",
                "ARTIFACT_WRITE_SUCCEEDED",
                "WORKER_EXITING");
            Assert.DoesNotContain("X-WP04-Evidence-Token", first.StandardError, StringComparison.Ordinal);
            Assert.DoesNotContain("runId=", first.StandardError, StringComparison.OrdinalIgnoreCase);
            using var firstRecord = JsonDocument.Parse(first.StandardOutput);
            Assert.Equal("run-initialize", firstRecord.RootElement.GetProperty("RunId").GetString());
            Assert.Equal(4, firstRecord.RootElement.GetProperty("SchemaVersion").GetInt64());
            Assert.Equal("delete", firstRecord.RootElement.GetProperty("JournalMode").GetString());
            Assert.Equal("ok", firstRecord.RootElement.GetProperty("IntegrityCheck").GetString());
            Assert.Equal("ok", firstRecord.RootElement.GetProperty("QuickCheck").GetString());

            var second = RunQualification(database, evidence, "reopen", "run-reopen");
            Assert.Equal(0, second.ExitCode);
            Assert.Equal(second.StandardOutput.Trim(), File.ReadAllText(evidence));
            Assert.DoesNotContain("run-initialize", File.ReadAllText(evidence), StringComparison.Ordinal);
            Assert.DoesNotContain(
                Directory.EnumerateFiles(Path.GetDirectoryName(evidence)!),
                static path => Path.GetFileName(path).EndsWith(".tmp", StringComparison.Ordinal));
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(root))
            {
                Directory.Delete(root, recursive: true);
            }
        }
    }

    [Fact]
    public void QualificationWithoutEvidenceOutputPreservesStdoutOnlyBehaviorAndInvalidArtifactParentFails()
    {
        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp04-evidence-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        try
        {
            var stdoutOnly = RunQualification(Path.Combine(root, "stdout.sqlite"), null, "initialize", null);
            Assert.Equal(0, stdoutOnly.ExitCode);
            using var record = JsonDocument.Parse(stdoutOnly.StandardOutput);
            Assert.Equal("stdout-only", record.RootElement.GetProperty("RunId").GetString());

            string parentFile = Path.Combine(root, "not-a-directory");
            File.WriteAllText(parentFile, "x");
            var invalid = RunQualification(Path.Combine(root, "invalid.sqlite"), Path.Combine(parentFile, "record.json"), "initialize", "run-invalid");
            Assert.Equal(1, invalid.ExitCode);
            Assert.Contains("evidence artifact could not be written", invalid.StandardError, StringComparison.Ordinal);
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(root))
            {
                Directory.Delete(root, recursive: true);
            }
        }
    }

    [Fact]
    public async Task QualificationHttpEvidencePreservesEndpointStatusesAndEmitsSanitizedLifecycleDiagnostics()
    {
        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp04-http-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        string runId = $"run-http-{Guid.NewGuid():N}";
        string token = $"test-capability-token-{Guid.NewGuid():N}";
        Process? process = null;
        try
        {
            string database = Path.Combine(root, "data", "qualification.sqlite");
            string evidence = Path.Combine(root, "evidence", "record.json");
            var start = new ProcessStartInfo("dotnet")
            {
                WorkingDirectory = FindRepositoryRoot(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            start.ArgumentList.Add("run");
            start.ArgumentList.Add("--project");
            start.ArgumentList.Add(Path.Combine(FindRepositoryRoot(), "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj"));
            start.ArgumentList.Add("--no-build");
            start.ArgumentList.Add("--configuration");
            start.ArgumentList.Add("Release");
            start.Environment["Worker__Mode"] = "PersistentSqliteQualification";
            start.Environment["PersistentSqliteQualification__Phase"] = "initialize";
            start.Environment["PersistentSqliteQualification__EvidenceOutputPath"] = evidence;
            start.Environment["PersistentSqliteQualification__RunId"] = runId;
            start.Environment["PersistentSqliteQualification__HttpEvidenceEnabled"] = "true";
            start.Environment["PersistentSqliteQualification__HttpEvidenceToken"] = token;
            start.Environment["Persistence__DatabasePath"] = database;
            start.Environment["Persistence__CreateParentDirectoryForInitialization"] = "true";
            start.Environment["Visualization__HandoffPath"] = Path.Combine(root, "read-model.json");

            process = Process.Start(start) ?? throw new InvalidOperationException("Qualification Worker did not start.");
            Task<string> stdoutTask = process.StandardOutput.ReadToEndAsync();
            Task<string> stderrTask = process.StandardError.ReadToEndAsync();
            using var client = new HttpClient(new SocketsHttpHandler { UseProxy = false }) { Timeout = TimeSpan.FromMilliseconds(500) };
            var missingTokenObserved = false;
            var listenerReady = false;
            for (var attempt = 0; attempt < 50; attempt++)
            {
                try
                {
                    using var response = await client.GetAsync("http://127.0.0.1:8501/internal/wp04/persistence-qualification");
                    if ((int)response.StatusCode == 401)
                    {
                        missingTokenObserved = true;
                    }

                    using var readinessRequest = new HttpRequestMessage(HttpMethod.Get, $"http://127.0.0.1:8501/internal/wp04/persistence-qualification?runId=wrong-{runId}");
                    readinessRequest.Headers.Add("X-WP04-Evidence-Token", token);
                    using var readinessResponse = await client.SendAsync(readinessRequest);
                    if ((int)readinessResponse.StatusCode == 409)
                    {
                        listenerReady = true;
                        break;
                    }
                }
                catch (HttpRequestException)
                {
                }
                catch (TaskCanceledException)
                {
                }

                await Task.Delay(100);
            }

            Assert.True(listenerReady, "Qualification listener did not become reachable.");
            Assert.True(missingTokenObserved, "Missing-token request did not receive a 401 response.");
            using var wrongRunRequest = new HttpRequestMessage(HttpMethod.Get, $"http://127.0.0.1:8501/internal/wp04/persistence-qualification?runId=wrong-{runId}");
            wrongRunRequest.Headers.Add("X-WP04-Evidence-Token", token);
            using var wrongRunResponse = await client.SendAsync(wrongRunRequest);
            Assert.Equal(409, (int)wrongRunResponse.StatusCode);

            using var acceptedRequest = new HttpRequestMessage(HttpMethod.Get, $"http://127.0.0.1:8501/internal/wp04/persistence-qualification?runId={runId}");
            acceptedRequest.Headers.Add("X-WP04-Evidence-Token", token);
            using var acceptedResponse = await client.SendAsync(acceptedRequest);
            Assert.Equal(200, (int)acceptedResponse.StatusCode);
            Assert.Equal(File.ReadAllText(evidence), await acceptedResponse.Content.ReadAsStringAsync());

            await process.WaitForExitAsync().WaitAsync(TimeSpan.FromSeconds(10));
            Assert.True(process.HasExited, "Qualification listener did not stop after retrieval.");
            Assert.Equal(0, process.ExitCode);
            string stdout = await stdoutTask;
            string stderr = await stderrTask;
            string recordJson = stdout.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Last(static line => line.StartsWith('{'));
            using var record = JsonDocument.Parse(recordJson);
            Assert.Equal(runId, record.RootElement.GetProperty("RunId").GetString());
            AssertDiagnosticSequence(
                stderr,
                "QUALIFICATION_ENTERED",
                "SQLITE_QUALIFICATION_STARTED",
                "ARTIFACT_WRITE_SUCCEEDED",
                "LISTENER_STARTING",
                "LISTENER_STARTED",
                "REQUEST_ARRIVED",
                "HANDLER_ENTERED",
                "EVIDENCE_RETRIEVAL_SUCCEEDED",
                "LISTENER_STOPPING",
                "LISTENER_STOPPED",
                "WORKER_EXITING");
            Assert.DoesNotContain(token, stderr, StringComparison.Ordinal);
            Assert.DoesNotContain("wrong-run", stderr, StringComparison.Ordinal);
            Assert.DoesNotContain("?runId=", stderr, StringComparison.OrdinalIgnoreCase);
        }
        finally
        {
            if (process is { HasExited: false })
            {
                process.Kill(entireProcessTree: true);
                await process.WaitForExitAsync();
            }

            process?.Dispose();
            SqliteConnection.ClearAllPools();
            await DeleteTemporaryDirectoryAsync(root);
        }
    }

    [Fact]
    public void OpenConnectionWhenUnsupportedVersionExistsRejectsWithoutReplacingState()
    {
        using var database = new TestDatabase();
        using (var connection = new SqliteConnection($"Data Source={database.Path}"))
        {
            connection.Open();
            Execute(connection, "PRAGMA user_version = 5;");
        }

        Assert.Throws<InvalidOperationException>(() => database.Factory.OpenConnection());
        using var verification = new SqliteConnection($"Data Source={database.Path}");
        verification.Open();
        Assert.Equal(5L, Scalar<long>(verification, "PRAGMA user_version;"));
    }

    [Fact]
    public void VersionThreeDatabaseMigratesToVersionFourPreservingHistoricalSnapshotAndExperimentEvidence()
    {
        using var database = new TestDatabase();
        const long fromTicks = 638396640000000000L;
        const long toTicks = 638396641000000000L;
        string snapshot = new('a', 64);
        string definition = new('b', 64);
        string research = new('c', 64);
        string sourceState = new('d', 64);
        string experiment = new('e', 64);
        string experimentDefinition = new('f', 64);
        string featureSet = new('1', 64);
        string featureDefinition = new('2', 64);

        using (var connection = new SqliteConnection($"Data Source={database.Path}"))
        {
            connection.Open();
            Execute(connection, SqliteHistoricalObservationSchema.CreateTableStatement);
            Execute(connection, SqliteDatasetSchema.CreateSnapshotTableStatement.Replace("IN (0, 1)", "= 0", StringComparison.Ordinal));
            Execute(connection, SqliteDatasetSchema.CreateObservationTableStatement);
            Execute(connection, SqliteExperimentResultSchema.CreateTableStatement.Replace("IN (0, 1)", "= 0", StringComparison.Ordinal));
            Execute(connection, $"""
                INSERT INTO dataset_snapshots VALUES
                ('{snapshot}', '{definition}', '{research}', '{sourceState}', 'aiq-dataset-identity-v1', 'TARGET',
                {fromTicks}, 0, {toTicks}, 0, 0, 0, NULL, NULL, NULL, NULL, 0);
                """);
            Execute(connection, $"""
                INSERT INTO experiment_results VALUES
                ('{experiment}', 'aiq-experiment-identity-v1', 'simple-return-descriptive-summary-v1', '{experimentDefinition}',
                'aiq-feature-identity-v1', '{featureSet}', '{featureDefinition}', 'aiq-dataset-identity-v1', '{snapshot}',
                '{definition}', '{research}', '{sourceState}', 0, 0, 0, 0, NULL, NULL, NULL);
                """);
            Execute(connection, "PRAGMA user_version = 3;");
        }

        using var upgraded = database.Factory.OpenConnection();
        Assert.Equal(4L, Scalar<long>(upgraded, "PRAGMA user_version;"));
        Assert.Equal(1L, Scalar<long>(upgraded, "SELECT COUNT(*) FROM dataset_snapshots WHERE source_authority = 0;"));
        Assert.Equal(1L, Scalar<long>(upgraded, "SELECT COUNT(*) FROM experiment_results WHERE source_authority = 0;"));
        Assert.Equal(0L, Scalar<long>(upgraded, "SELECT COUNT(*) FROM pragma_foreign_key_check;"));
        Assert.Contains("source_authority IN (0, 1)", Scalar<string>(upgraded, "SELECT sql FROM sqlite_schema WHERE name = 'dataset_snapshots';"), StringComparison.OrdinalIgnoreCase);
        Assert.Contains("source_authority IN (0, 1)", Scalar<string>(upgraded, "SELECT sql FROM sqlite_schema WHERE name = 'experiment_results';"), StringComparison.OrdinalIgnoreCase);
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

    private static (int ExitCode, string StandardOutput, string StandardError) RunQualification(
        string databasePath,
        string? evidenceOutputPath,
        string phase,
        string? runId)
    {
        var start = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = FindRepositoryRoot(),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        start.ArgumentList.Add("run");
        start.ArgumentList.Add("--project");
        start.ArgumentList.Add(Path.Combine(FindRepositoryRoot(), "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj"));
        start.ArgumentList.Add("--no-build");
        start.ArgumentList.Add("--configuration");
        start.ArgumentList.Add("Release");
        start.Environment["Worker__Mode"] = "PersistentSqliteQualification";
        start.Environment["PersistentSqliteQualification__Phase"] = phase;
        start.Environment["Persistence__DatabasePath"] = databasePath;
        start.Environment["Persistence__CreateParentDirectoryForInitialization"] = "true";
        start.Environment["Visualization__HandoffPath"] = Path.Combine(Path.GetDirectoryName(databasePath)!, "read-model.json");
        if (evidenceOutputPath is not null)
        {
            start.Environment["PersistentSqliteQualification__EvidenceOutputPath"] = evidenceOutputPath;
            start.Environment["PersistentSqliteQualification__RunId"] = runId!;
        }

        using var process = Process.Start(start) ?? throw new InvalidOperationException("Qualification Worker did not start.");
        string standardOutput = process.StandardOutput.ReadToEnd();
        string standardError = process.StandardError.ReadToEnd();
        Assert.True(process.WaitForExit(30_000), "Qualification Worker did not terminate.");
        return (process.ExitCode, standardOutput, standardError);
    }

    private static void AssertDiagnosticSequence(string standardError, params string[] events)
    {
        var positions = events.Select(eventName => standardError.IndexOf(
            $"WP04_DIAG_EVENT={eventName}",
            StringComparison.Ordinal)).ToArray();

        Assert.DoesNotContain(positions, static position => position < 0);
        Assert.True(positions.SequenceEqual(positions.Order()), "Diagnostic events were not emitted in order.");
        Assert.All(
            standardError.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(static line => line.StartsWith("WP04_DIAG_", StringComparison.Ordinal)),
            static line => Assert.Matches(
                "^WP04_DIAG_EVENT=[A-Z_]+ WP04_DIAG_RUN_ID=[A-Za-z0-9-]+ WP04_DIAG_PHASE=(initialize|reopen) WP04_DIAG_ELAPSED_MS=[0-9]+ WP04_DIAG_OUTCOME=[A-Za-z]+(?: WP04_DIAG_LISTENER_HOST=0\\.0\\.0\\.0 WP04_DIAG_LISTENER_PORT=8501 WP04_DIAG_ROUTE=/internal/wp04/persistence-qualification WP04_DIAG_HTTP_METHOD=GET)?$",
                line));
    }

    private static async Task DeleteTemporaryDirectoryAsync(string root)
    {
        IOException? lastFailure = null;
        for (var attempt = 0; attempt < 20 && Directory.Exists(root); attempt++)
        {
            try
            {
                Directory.Delete(root, recursive: true);
                return;
            }
            catch (IOException failure)
            {
                lastFailure = failure;
                await Task.Delay(100);
            }
        }

        if (Directory.Exists(root))
        {
            throw new IOException($"Temporary qualification directory could not be deleted: {root}", lastFailure);
        }
    }

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx")))
            {
                return directory.FullName;
            }
        }

        throw new InvalidOperationException("Repository root was not found.");
    }

    private sealed class ThrowingConnectionFactory : ISqliteConnectionFactory
    {
        public SqliteConnection OpenConnection() => throw new InvalidOperationException("offline failure");
    }

    private sealed class TestDatabase : IDisposable
    {
        private readonly string directory;

        public TestDatabase(bool createDirectory = true, bool createParentDirectoryForInitialization = false)
        {
            directory = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(),
                $"aiq-wp14-{Guid.NewGuid():N}");
            if (createDirectory)
            {
                Directory.CreateDirectory(directory);
            }

            Path = System.IO.Path.Combine(directory, "history.db");
            Configuration = new SqliteStorageConfiguration(Path, createParentDirectoryForInitialization);
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
