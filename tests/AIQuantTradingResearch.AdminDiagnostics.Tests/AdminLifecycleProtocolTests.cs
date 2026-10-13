using AIQuantTradingResearch.AdminDiagnostics;
using Xunit;

namespace AIQuantTradingResearch.AdminDiagnostics.Tests;

public sealed class AdminLifecycleProtocolTests
{
    private const string Meta = "4149514c0101010000112233445566778899aabbccddeeff00000001000000010000002a00000006fc23ac000000000000000000000000000000000000000000";
    private const string Agent = "4149514c0104300000112233445566778899aabbccddeeff000000010000002b0000002c00000006fc23ac0000000007ea8ed400000000000000000000000000";
    private static readonly Guid Launch = Guid.Parse("00112233-4455-6677-8899-aabbccddeeff");
    private static LifecycleRecord Record() => new(LifecycleRole.Root, LifecycleMessage.MetaRouter, 0, Launch, 1, 1, 42, 30_000_000_000);

    [Fact] public void ContextBindingsRejectInvalidBounds()
    {
        foreach (var pid in new uint[] { 0, 0x80000000, uint.MaxValue })
        {
            Assert.Throws<LifecycleValidationException>(() => new LifecycleDirection("sr", LifecycleRole.Supervisor, Launch, pid, 30_000_000_000));
            var d = new LifecycleDirection("sr", LifecycleRole.Supervisor, Launch, 1, 30_000_000_000);
            var r = Record() with { Role = LifecycleRole.Supervisor, Message = LifecycleMessage.RPrepare };
            Assert.Throws<LifecycleValidationException>(() => d.Accept(r, 0, r.Message, pid));
            Assert.True(d.Failed);
            Assert.Throws<LifecycleValidationException>(() => d.Accept(r, 0, r.Message, 42));
            foreach (var pair in new[] { (pid, 42u), (1u, pid) })
                Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.Metadata(Record().Encode(), LifecycleRole.Router, Launch,
                    new Dictionary<LifecycleMessage, (uint, uint)> { [LifecycleMessage.MetaRouter] = pair }, 30_000_000_000));
        }
        foreach (var time in new ulong[] { 0, 0x8000000000000000, ulong.MaxValue })
        {
            Assert.Throws<LifecycleValidationException>(() => new LifecycleDirection("sr", LifecycleRole.Supervisor, Launch, 1, time));
            Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.Metadata(Record().Encode(), LifecycleRole.Router, Launch,
                new Dictionary<LifecycleMessage, (uint, uint)> { [LifecycleMessage.MetaRouter] = (1, 42) }, time));
        }
    }

    [Fact] public void CanonicalMetadata()
    {
        Assert.Equal(Meta, Convert.ToHexString(Record().Encode()).ToLowerInvariant());
        Assert.Equal(64, Record().Encode().Length);
        Assert.Equal(Record(), LifecycleRecord.Decode(Convert.FromHexString(Meta)));
    }
    [Theory]
    [InlineData(0x30, 0)] [InlineData(0x30, 7)] [InlineData(0xb0, 15)] [InlineData(0xb1, 1)]
    public void CanonicalAgentResults(byte type, byte outcome)
    {
        var r = Record() with { Role = LifecycleRole.Guardian, Message = (LifecycleMessage)type,
            Outcome = outcome, SenderPid = 43, RelatedPid = 44, ShutdownDeadline = 34_000_000_000 };
        var expected = Convert.FromHexString(Agent); expected[6] = type; expected[7] = outcome;
        Assert.Equal(expected, r.Encode()); Assert.Equal(r, LifecycleRecord.Decode(expected));
        var d = new LifecycleDirection("sg", r.Role, Launch, 43, r.StartupDeadline);
        d.Accept(r with { Message = LifecycleMessage.GBound, Outcome = 0, ShutdownDeadline = 0 }, 0, LifecycleMessage.GBound, 44);
        d.BindShutdown(30_000_000_000); d.Accept(r with { Sequence = 2 }, 30_000_000_001, r.Message, 44);
    }
    [Theory]
    [InlineData(0, 0)] [InlineData(4, 2)] [InlineData(5, 9)] [InlineData(6, 255)] [InlineData(7, 7)] [InlineData(63, 1)]
    public void InvalidHeaders(int offset, byte value)
    {
        var bytes = Convert.FromHexString(Meta); bytes[offset] = value;
        Assert.Throws<LifecycleValidationException>(() => LifecycleRecord.Decode(bytes));
    }
    [Fact] public void InvalidLengthsAndNumericBounds()
    {
        Assert.Throws<LifecycleValidationException>(() => LifecycleRecord.Decode(new byte[63]));
        Assert.Throws<LifecycleValidationException>(() => LifecycleRecord.Decode(new byte[65]));
        foreach (var r in new[] { Record() with { Launch = Guid.Empty }, Record() with { Sequence = 0 },
            Record() with { Sequence = 33 }, Record() with { SenderPid = 0 }, Record() with { RelatedPid = 0 },
            Record() with { SenderPid = 0x80000000 }, Record() with { StartupDeadline = 0 },
            Record() with { StartupDeadline = 0x8000000000000000 }, Record() with { ShutdownDeadline = 0x8000000000000000 } })
            Assert.Throws<LifecycleValidationException>(() => r.Encode());
    }
    [Theory]
    [InlineData(0xb0, 0)] [InlineData(0xb0, 65)] [InlineData(0xb1, 0)] [InlineData(0xb1, 5)] [InlineData(0x7f, 0)] [InlineData(0x7f, 9)]
    public void InvalidOutcomes(byte type, byte outcome) => Assert.Throws<LifecycleValidationException>(() =>
        (Record() with { Message = (LifecycleMessage)type, Outcome = outcome }).Encode());
    [Fact] public void DeadlineBoundaries()
    {
        Assert.Equal((5_000_000_000ul, 30_000_000_000ul), AdminLifecycleProtocol.StartupDeadlines(0));
        Assert.Equal((ulong)long.MaxValue, AdminLifecycleProtocol.Deadline((ulong)long.MaxValue - 4_000_000_000, 4_000_000_000));
        Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.Deadline(long.MaxValue, 1));
        AdminLifecycleProtocol.CheckPrivilegedDeadline(30_000_000_000, 4_999_999_999);
        Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.CheckPrivilegedDeadline(30_000_000_000, 5_000_000_000));
    }
    [Fact] public void C1GateReceiptIsNotExecReadiness()
    {
        var g = Record() with { Role = LifecycleRole.Guardian, Message = LifecycleMessage.AExec, SenderPid = 43, RelatedPid = 44 };
        var a = Record() with { Role = LifecycleRole.AgentBootstrap, Message = LifecycleMessage.AExecAck, SenderPid = 44, RelatedPid = 43 };
        var gate = new LifecycleLaunchGate();
        foreach (var r in new[] { g, a })
        {
            new LifecycleDirection("gate", r.Role, Launch, r.SenderPid, r.StartupDeadline).Accept(r, 0, r.Message, r.RelatedPid);
            gate.Accept(r);
        }
        Assert.True(gate.MustCloseBeforeExec);
        Assert.Throws<LifecycleValidationException>(() => gate.Accept(a));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleLaunchGate().Accept(a));
        var startup = new LifecycleStartup();
        foreach (var m in new[] { LifecycleMessage.GBound, LifecycleMessage.RPrepare, LifecycleMessage.RBound,
            LifecycleMessage.GRelease, LifecycleMessage.AExec, LifecycleMessage.AExecAck }) startup.Accept(m);
        Assert.False(startup.Running);
        Assert.Throws<LifecycleValidationException>(() => startup.Accept(LifecycleMessage.StartUi));
    }
    [Theory] [InlineData("sr")] [InlineData("sg")] [InlineData("ra")]
    public void BootstrapAndInternalEventsRejectedOnRuntime(string channel)
    {
        foreach (var r in new[] { Record() with { Role = LifecycleRole.AgentBootstrap, Message = LifecycleMessage.AExecAck },
            Record(), Record() with { Role = LifecycleRole.Supervisor, Message = LifecycleMessage.GuardianObserved },
            Record() with { Role = LifecycleRole.Supervisor, Message = LifecycleMessage.GuardianSignal, Outcome = 1 } })
            Assert.Throws<LifecycleValidationException>(() =>
                new LifecycleDirection(channel, r.Role, Launch, r.SenderPid, r.StartupDeadline).Accept(r, 0, r.Message, r.RelatedPid));
    }
    [Fact] public void BindingReplayStateExpiryAndStickiness()
    {
        var r = Record() with { Role = LifecycleRole.Guardian, Message = LifecycleMessage.GBound, SenderPid = 43, RelatedPid = 44 };
        foreach (var wrong in new[] { r with { SenderPid = 45 }, r with { RelatedPid = 45 },
            r with { Launch = Guid.NewGuid() }, r with { Sequence = 2 } })
        {
            var d = new LifecycleDirection("sg", r.Role, Launch, 43, r.StartupDeadline);
            Assert.Throws<LifecycleValidationException>(() => d.Accept(wrong, 0, r.Message, 44));
            Assert.Throws<LifecycleValidationException>(() => d.Accept(r, 0, r.Message, 44));
        }
        var replay = new LifecycleDirection("sg", r.Role, Launch, 43, r.StartupDeadline);
        replay.Accept(r, 0, r.Message, 44);
        Assert.Throws<LifecycleValidationException>(() => replay.Accept(r, 0, r.Message, 44));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleDirection("sg", r.Role, Launch, 43, r.StartupDeadline).Accept(r, r.StartupDeadline, r.Message, 44));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleDirection("sg", r.Role, Launch, 43, r.StartupDeadline).Accept(r, 0, LifecycleMessage.AgentResult, 44));
    }
    [Fact] public void CountsAndShutdownCannotReset()
    {
        var r = Record() with { Role = LifecycleRole.Guardian, Message = LifecycleMessage.GBound, SenderPid = 43 };
        var d = new LifecycleDirection("sg", r.Role, Launch, 43, r.StartupDeadline);
        d.Accept(r, 0, r.Message, 42);
        Assert.Throws<LifecycleValidationException>(() => d.Accept(r with { Sequence = 2 }, 0, r.Message, 42));
        d = new LifecycleDirection("sg", r.Role, Launch, 43, r.StartupDeadline);
        d.BindShutdown(30_000_000_000);
        Assert.Throws<LifecycleValidationException>(() => d.BindShutdown(30_000_000_001));
    }
    [Fact] public void MetadataOrderCountAndBinding()
    {
        var a = Record(); var b = a with { Message = LifecycleMessage.MetaGuardian, Sequence = 2, RelatedPid = 43 };
        var bindings = new Dictionary<LifecycleMessage, (uint, uint)> { [a.Message] = (1, 42), [b.Message] = (1, 43) };
        var data = a.Encode().Concat(b.Encode()).ToArray();
        Assert.Equal(new[] { a, b }, AdminLifecycleProtocol.Metadata(data, LifecycleRole.Supervisor, Launch, bindings, a.StartupDeadline));
        foreach (var wrong in new[] { data[..^1], data.Concat(new byte[] { 0 }).ToArray(), b.Encode().Concat(a.Encode()).ToArray() })
            Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.Metadata(wrong, LifecycleRole.Supervisor, Launch, bindings, a.StartupDeadline));
        Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.Metadata(data, LifecycleRole.Supervisor, Launch, bindings, a.StartupDeadline, a.StartupDeadline));
    }
    [Fact] public void AllMetadataRolesAndDescriptorLifetime()
    {
        var a = Record(); var b = a with { Role = LifecycleRole.GuardianBootstrap, Message = LifecycleMessage.MetaAgent, Sequence = 2, SenderPid = 43, RelatedPid = 44 };
        var bindings = new Dictionary<LifecycleMessage, (uint, uint)> { [a.Message] = (1, 42), [b.Message] = (43, 44) };
        foreach (var role in new[] { LifecycleRole.Guardian, LifecycleRole.Agent })
            Assert.Equal(new[] { a, b }, AdminLifecycleProtocol.Metadata(a.Encode().Concat(b.Encode()).ToArray(), role, Launch, bindings, a.StartupDeadline));
        Assert.Equal(new[] { a }, AdminLifecycleProtocol.Metadata(a.Encode(), LifecycleRole.Router, Launch, bindings, a.StartupDeadline));
        AdminLifecycleProtocol.ValidateDescriptors(LifecycleRole.Supervisor, new Dictionary<int, string> { [3] = "sr", [4] = "sg", [5] = "metadata" });
        AdminLifecycleProtocol.ValidateDescriptors(LifecycleRole.Guardian, new Dictionary<int, string> { [3] = "sg", [4] = "gate", [5] = "metadata" });
        AdminLifecycleProtocol.ValidateDescriptors(LifecycleRole.Router, new Dictionary<int, string> { [3] = "credential", [4] = "ra", [5] = "sr", [6] = "metadata" });
        AdminLifecycleProtocol.ValidateDescriptors(LifecycleRole.Agent, new Dictionary<int, string> { [3] = "ra", [4] = "metadata" });
        AdminLifecycleProtocol.ValidateDescriptors(LifecycleRole.Agent, new Dictionary<int, string> { [3] = "ra" }, metadataConsumed: true);
        Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.ValidateDescriptors(LifecycleRole.Agent, new Dictionary<int, string> { [3] = "ra", [4] = "gate" }));
        Assert.Throws<LifecycleValidationException>(() => AdminLifecycleProtocol.ValidateDescriptors(LifecycleRole.Agent, new Dictionary<int, string> { [3] = "ra", [9] = "credential" }, metadataConsumed: true));
    }
    [Fact] public void CompleteCatalogStructuralRoundtrip()
    {
        foreach (var m in Enum.GetValues<LifecycleMessage>())
        {
            byte outcome = m is LifecycleMessage.AgentSignal or LifecycleMessage.GuardianSignal or LifecycleMessage.RouterSignal
                or LifecycleMessage.AgentWaitFailure or LifecycleMessage.GuardianWaitFailure or LifecycleMessage.RouterWaitFailure or LifecycleMessage.Fail ? (byte)1 : (byte)0;
            var r = Record() with { Message = m, Outcome = outcome };
            Assert.Equal(r, LifecycleRecord.Decode(r.Encode()));
        }
    }
    [Fact] public void PartialFramesAndEof()
    {
        var f = new LifecycleFrames(); Assert.Empty(f.Feed(Record().Encode().AsSpan(0, 31)));
        Assert.Equal(new[] { Record() }, f.Feed(Record().Encode().AsSpan(31))); f.Eof();
        Assert.Throws<LifecycleValidationException>(() => f.Feed(Record().Encode()));
        f = new LifecycleFrames(); f.Feed(Record().Encode().AsSpan(0, 63));
        Assert.Throws<LifecycleValidationException>(() => f.Eof());
        Assert.Throws<LifecycleValidationException>(() => new LifecycleFrames().Feed(new byte[65]));
    }
    [Fact] public void FullStartupOrder()
    {
        var model = new LifecycleStartup();
        foreach (var m in Enum.GetValues<LifecycleMessage>().Where(m => m >= LifecycleMessage.GBound && m <= LifecycleMessage.RExposed)) model.Accept(m);
        Assert.True(model.Running);
    }
    [Fact] public void ShutdownReportsNeverReplaceIndependentWaits()
    {
        var s = Shutdown(); const ulong now = 30_000_000_001;
        s.Event(LifecycleMessage.StopRouter, now); s.Event(LifecycleMessage.StopGuardian, now);
        s.Event(LifecycleMessage.StopUi, now); s.Event(LifecycleMessage.UiStopped, now); s.LifecycleEof(now);
        s.Event(LifecycleMessage.RProtocolDone, now); s.Event(LifecycleMessage.HostDisposed, now); s.AgentReport(Report(), now, "sg");
        Assert.False(s.CompleteModel);
        s.WaitObservation("agent", 44, true, 0, now);
        s.WaitObservation("guardian", 43, true, 0, now); s.WaitObservation("router", 42, true, 0, now);
        s.Absence(true, now); Assert.True(s.CompleteModel);
        Assert.Throws<LifecycleValidationException>(() => Shutdown().Absence(true, now));
        foreach (var observation in new[] { (44u, true, 0), (43u, false, 0), (43u, true, 7) })
        {
            s = Shutdown(); s.Event(LifecycleMessage.StopGuardian, now); s.AgentReport(Report(), now, "sg");
            s.WaitObservation("agent", 44, true, 0, now);
            Assert.Throws<LifecycleValidationException>(() => s.WaitObservation("guardian", observation.Item1, observation.Item2, observation.Item3, now));
        }
    }
    private static LifecycleShutdown Shutdown() => new(30_000_000_000, 44, 43, 42, Launch, 30_000_000_000);
    private static LifecycleRecord Report() => Record() with { Role = LifecycleRole.Guardian, Message = LifecycleMessage.AgentResult,
        Sequence = 2, SenderPid = 43, RelatedPid = 44, ShutdownDeadline = 34_000_000_000 };

    [Fact] public void GenericResultIsTerminalFailure()
    {
        const ulong now = 30_000_000_001; var s = Shutdown(); s.Event(LifecycleMessage.StopGuardian, now);
        Assert.Throws<LifecycleValidationException>(() => s.Event(LifecycleMessage.AgentResult, now));
        Assert.True(s.Failed); Assert.False(s.CompleteModel);
        Assert.Throws<LifecycleValidationException>(() => s.AgentReport(Report(), now, "sg"));
    }
    [Theory]
    [InlineData(0)] [InlineData(-1)] [InlineData(2147483648L)]
    public void ConstructorRejectsInvalidPids(long value)
    {
        Assert.Throws<LifecycleValidationException>(() => new LifecycleShutdown(0, value, 43, 42, Launch, 30_000_000_000));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleShutdown(0, 44, value, 42, Launch, 30_000_000_000));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleShutdown(0, 44, 43, value, Launch, 30_000_000_000));
    }
    [Fact] public void ConstructorRejectsDuplicateBindingsAndOverflow()
    {
        foreach (var ids in new[] { (43L, 43L, 42L), (44L, 42L, 42L), (44L, 43L, 44L) })
            Assert.Throws<LifecycleValidationException>(() => new LifecycleShutdown(0, ids.Item1, ids.Item2, ids.Item3, Launch, 30_000_000_000));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleShutdown(0, 44, 43, 42, Guid.Empty, 30_000_000_000));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleShutdown((ulong)long.MaxValue, 44, 43, 42, Launch, 30_000_000_000));
        Assert.Throws<LifecycleValidationException>(() => new LifecycleShutdown(0, 44, 43, 42, Launch, 0));
    }
    [Fact] public void CompleteReportBindingAndMutationStickiness()
    {
        const ulong now = 30_000_000_001; var r = Report();
        foreach (var wrong in new LifecycleRecord?[] { r with { SenderPid = 42 }, r with { RelatedPid = 43 },
            r with { Launch = Guid.Parse("11111111-1111-1111-1111-111111111111") }, r with { StartupDeadline = 30_000_000_001 },
            r with { ShutdownDeadline = 34_000_000_001 }, r with { Sequence = 1 }, r with { Sequence = 0 },
            r with { Outcome = 7 }, r with { Role = LifecycleRole.Agent }, r with { Message = LifecycleMessage.AgentSignal, Outcome = 15 }, null })
        {
            var s = Shutdown(); s.Event(LifecycleMessage.StopGuardian, now);
            Assert.Throws<LifecycleValidationException>(() => s.AgentReport(wrong, now, "sg"));
            Assert.True(s.Failed);
            Assert.Throws<LifecycleValidationException>(() => s.AgentReport(r, now, "sg"));
        }
        foreach (var channel in new[] { "sr", "ra", "gate" })
        {
            var s = Shutdown(); s.Event(LifecycleMessage.StopGuardian, now);
            Assert.Throws<LifecycleValidationException>(() => s.AgentReport(r, now, channel));
            Assert.True(s.Failed);
        }
        var premature = Shutdown();
        Assert.Throws<LifecycleValidationException>(() => premature.AgentReport(r, now, "sg")); Assert.True(premature.Failed);
        var duplicate = Shutdown(); duplicate.Event(LifecycleMessage.StopGuardian, now); duplicate.AgentReport(r, now, "sg");
        Assert.Throws<LifecycleValidationException>(() => duplicate.AgentReport(r, now, "sg")); Assert.True(duplicate.Failed);
    }
    [Fact] public void ReportAloneCannotAdvancePositiveAgentWait()
    {
        const ulong now = 30_000_000_001; var s = Shutdown();
        s.Event(LifecycleMessage.StopGuardian, now); s.AgentReport(Report(), now, "sg");
        Assert.Throws<LifecycleValidationException>(() => s.WaitObservation("guardian", 43, true, 0, now));
        Assert.True(s.Failed);
    }
    [Theory]
    [InlineData(0)] [InlineData(-1)] [InlineData(2147483648L)] [InlineData(43)]
    public void InvalidObservationIsSticky(long pid)
    {
        const ulong now = 30_000_000_001; var s = Shutdown();
        s.Event(LifecycleMessage.StopGuardian, now); s.AgentReport(Report(), now, "sg");
        Assert.Throws<LifecycleValidationException>(() => s.WaitObservation("agent", pid, true, 0, now));
        Assert.True(s.Failed);
        Assert.Throws<LifecycleValidationException>(() => s.WaitObservation("agent", 44, true, 0, now));
    }
    [Theory] [InlineData(0, false)] [InlineData(-1, true)] [InlineData(256, true)] [InlineData(7, true)]
    public void InvalidExitObservationFails(int code, bool normal)
    {
        const ulong now = 30_000_000_001; var s = Shutdown();
        s.Event(LifecycleMessage.StopGuardian, now); s.AgentReport(Report(), now, "sg");
        Assert.Throws<LifecycleValidationException>(() => s.WaitObservation("agent", 44, normal, code, now));
        Assert.True(s.Failed);
    }
    [Fact] public void TimestampAndDeadlineFailuresAreSticky()
    {
        foreach (var now in new[] { 29_999_999_999ul, 34_000_000_000ul, ulong.MaxValue })
        {
            var s = Shutdown();
            Assert.Throws<LifecycleValidationException>(() => s.Event(LifecycleMessage.StopRouter, now));
            Assert.True(s.Failed);
            Assert.Throws<LifecycleValidationException>(() => s.Event(LifecycleMessage.StopRouter, 30_000_000_001));
        }
        var d = new LifecycleDirection("sg", LifecycleRole.Guardian, Launch, 43, 30_000_000_000);
        Assert.Throws<LifecycleValidationException>(() => d.BindShutdown((ulong)long.MaxValue));
        Assert.True(d.Failed);
        Assert.Throws<LifecycleValidationException>(() => d.BindShutdown(30_000_000_000));
    }
    private static LifecycleDirection TerminalDirection()
    {
        var d = new LifecycleDirection("ra", LifecycleRole.Agent, Launch, 44, 30_000_000_000);
        d.Accept(Record() with { Role = LifecycleRole.Agent, Message = LifecycleMessage.AReady, SenderPid = 44 }, 0, LifecycleMessage.AReady, 42);
        d.Accept(Record() with { Role = LifecycleRole.Agent, Message = LifecycleMessage.UiReady, Sequence = 2, SenderPid = 44 }, 0, LifecycleMessage.UiReady, 42);
        d.BindShutdown(30_000_000_000);
        d.Accept(Record() with { Role = LifecycleRole.Agent, Message = LifecycleMessage.UiStopped, Sequence = 3, SenderPid = 44,
            ShutdownDeadline = 34_000_000_000 }, 30_000_000_001, LifecycleMessage.UiStopped, 42);
        return d;
    }
    [Fact] public void TerminalLifecycleEofOnlyOnce()
    {
        var d = TerminalDirection(); d.Eof(true, 30_000_000_001);
        Assert.Throws<LifecycleValidationException>(() => d.Eof(true, 30_000_000_001)); Assert.True(d.Failed);
        foreach (var channel in new[] { "sr", "sg", "gate", "ra" })
        {
            d = new LifecycleDirection(channel, LifecycleRole.Agent, Launch, 44, 30_000_000_000);
            Assert.Throws<LifecycleValidationException>(() => d.Eof(true, 30_000_000_001)); Assert.True(d.Failed);
        }
        foreach (var now in new ulong?[] { null, 0, 34_000_000_000, ulong.MaxValue })
        {
            d = TerminalDirection(); Assert.Throws<LifecycleValidationException>(() => d.Eof(true, now)); Assert.True(d.Failed);
        }
    }
    [Fact] public void MalformedGateAndDirectionMutationsAreSticky()
    {
        var g = Record() with { Role = LifecycleRole.Guardian, Message = LifecycleMessage.AExec, SenderPid = 43, RelatedPid = 44 };
        var gate = new LifecycleLaunchGate();
        Assert.Throws<LifecycleValidationException>(() => gate.Accept(g with { Outcome = 7 })); Assert.True(gate.Failed);
        Assert.Throws<LifecycleValidationException>(() => gate.Accept(g));
        var d = new LifecycleDirection("gate", LifecycleRole.Guardian, Launch, 43, 30_000_000_000);
        Assert.Throws<LifecycleValidationException>(() => d.Accept(null, 0, LifecycleMessage.AExec, 44)); Assert.True(d.Failed);
        Assert.Throws<LifecycleValidationException>(() => d.Accept(g, 0, LifecycleMessage.AExec, 44));
    }
}
