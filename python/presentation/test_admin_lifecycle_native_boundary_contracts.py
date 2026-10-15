"""Synthetic-only boundary checks; no live process, FD, clock or privilege work."""
import importlib.util
from dataclasses import FrozenInstanceError, replace
from pathlib import Path
import sys
import unittest

spec = importlib.util.spec_from_file_location("admin_lifecycle_native_boundary_contracts",
    Path(__file__).resolve().parents[2] / "container/admin_lifecycle_native_boundary_contracts.py")
p = importlib.util.module_from_spec(spec)
sys.modules[spec.name] = p
spec.loader.exec_module(p)


def handle(endpoint, position, channel, owner="A", cloexec=False, **changes):
    return p.Handle(endpoint, "ROOT", owner, position, channel, cloexec, **changes)


def deadline(kind="shutdown"):
    duration = dict(shutdown=4_000_000_000, startup=30_000_000_000, privileged=5_000_000_000)[kind]
    return p.Deadline(10, 10 + duration, kind)


def binding(child="A"):
    parent = dict(p.PARENTS)[child]
    creator = "ROOT" if child in ("G", "R") else parent
    return p.ForkBinding(creator, child, dict(A=44, G=43, R=42, UI=45)[child], (parent,),
                         parent_pid=dict(A=43, G=1, R=1, UI=44)[child])


def wait(child="A"):
    return p.WaitState(binding(child), deadline(), 2)


def exited(state):
    return p.WaitOutcome("exit", state.binding.status_owners[0], state.binding.child_pid,
                         state.binding.child_pid, 0, True, state.binding.child == "R")


class NativeBoundaryTests(unittest.TestCase):
    def reason(self, reason, operation):
        with self.assertRaises(p.BoundaryError) as error: operation()
        self.assertEqual(reason, str(error.exception))

    def test_immutable_and_not_runtime_proof(self):
        with self.assertRaises(FrozenInstanceError): deadline().end = 0
        self.assertFalse(p.PUBLIC_RUNTIME_VERIFIED)

    def test_parent_graph_positive_pid_and_exclusive_owner(self):
        for child in ("A", "G", "R", "UI"): binding(child)
        self.reason("WRONG_PARENT", lambda: p.ForkBinding("R", "A", 44, ("G",)))
        for pid in (0, 1, -1, True, 44.0, "44", p.MAX_PID + 1):
            self.reason("INVALID_INTEGER", lambda: p.ForkBinding("G", "A", pid, ("G",)))
        self.reason("COMPETING_STATUS_OWNER", lambda: p.ForkBinding("G", "A", 44, ("G", "S")))

    def test_prefork_secret_and_privileged_managed_rejected(self):
        for changes in ({"secret_free": False}, {"privileged_managed": True}):
            self.reason("UNSAFE_PREFORK", lambda: replace(binding(), **changes))
        self.reason("UNBOUNDED_ROOT_PHASE", lambda: replace(binding(), finite_root_phase=False))

    def test_full_inheritance_and_missing_parent_endpoint(self):
        original = (handle("gate", "scratch:gate", "gate", owner="G", cloexec=True),
                    handle("guardian_meta", "fd5", "metadata", owner="G", cloexec=True))
        copied = p.inherit(original, binding(), frozenset({"gate"}))
        self.assertEqual(2, len(copied)); self.assertFalse(copied[1].required)
        self.reason("PARENT_ENDPOINT_UNAVAILABLE", lambda: p.inherit(original, binding(), frozenset({"missing"})))
        self.reason("PARENT_ENDPOINT_UNAVAILABLE", lambda: p.inherit(tuple(replace(h, owner="R") for h in original), binding(), frozenset()))

    def test_fd5_occupied_then_explicit_safe_close(self):
        handles = (handle("gate", "scratch:gate", "gate", cloexec=True),
                   handle("inherited_meta", "fd5", "metadata", required=False))
        self.reason("LIVE_DESTINATION", lambda: p.remap(handles, (p.Move("scratch:gate", "fd5_candidate"),)))
        moved = p.remap(handles, (p.Move("fd5", kind="close_unused"), p.Move("scratch:gate", "fd5_candidate")))
        self.assertEqual("fd5_candidate", moved[0].position)

    def test_live_destination_cycle_and_named_scratch(self):
        handles = (handle("first", "fd3", "SR", owner="ROOT"), handle("second", "fd4", "SG", owner="ROOT"))
        self.reason("LIVE_DESTINATION", lambda: p.remap(handles, (p.Move("fd3", "fd4"),)))
        swapped = p.remap(handles, (p.Move("fd3", "scratch:evacuated"), p.Move("fd4", "fd3"), p.Move("scratch:evacuated", "fd4")))
        self.assertEqual(("fd4", "fd3"), tuple(h.position for h in swapped))
        self.reason("INVALID_SYMBOL", lambda: p.remap(handles, (p.Move("fd3", "scratch:7"),)))

    def test_alias_inventory_and_required_endpoint_close(self):
        base = handle("gate", "fd5_candidate", "gate", cloexec=True)
        alias = handle("gate_alias", "scratch:alias", "gate", cloexec=True, alias_of="gate")
        p.validate_inventory((base, alias))
        self.reason("UNBOUND_ALIAS", lambda: p.validate_inventory((alias,)))
        self.reason("LIVE_ENDPOINT_CLOSE", lambda: p.remap((base,), (p.Move("fd5", kind="close_unused"),)))
        self.reason("SLOT_COLLISION", lambda: p.validate_inventory((base, replace(alias, position="fd5"))))

    def agent_exec(self, **changes):
        ra = handle("ra", "fd3", "RA"); metadata = handle("metadata", "fd4", "metadata")
        gate = handle("gate", "fd5_candidate", "gate", cloexec=True)
        alias = handle("gate_alias", "scratch:alias", "gate", cloexec=True, alias_of="gate")
        values = dict(owner="A", inherited=(ra, metadata, gate, alias), survivors=(ra, metadata),
                      explicitly_closed=frozenset({"gate", "gate_alias"}), gate_ack=True,
                      gate_history=p.GateHistory((gate, alias), frozenset({"gate", "gate_alias"})))
        values.update(changes)
        return p.ExecBoundary(**values)

    def test_agent_exec_preserves_ra_metadata_and_closes_gate_aliases(self):
        self.agent_exec().validate()
        self.reason("UNCLOSED_INHERITED_COPY", lambda: self.agent_exec(explicitly_closed=frozenset({"gate"})).validate())
        self.reason("GATE_EXPLICIT_CLOSE_REQUIRED", lambda: self.agent_exec(gate_ack=False).validate())
        original = self.agent_exec()
        handles = tuple(replace(h, cloexec=False) if h.channel == "gate" else h for h in original.inherited)
        self.reason("GATE_CLOEXEC_MISSING", lambda: replace(original, inherited=handles).validate())

    def test_cloexec_only_and_lost_metadata_fail(self):
        original = self.agent_exec()
        self.reason("UNCLOSED_INHERITED_COPY", lambda: replace(original, explicitly_closed=frozenset()).validate())
        self.reason("AGENT_EXEC_MAP", lambda: replace(original, survivors=original.survivors[:1],
            explicitly_closed=original.explicitly_closed | {"metadata"}).validate())
        changed = replace(original.survivors[0], cloexec=True)
        self.reason("REQUIRED_EXEC_SURVIVOR_CLOEXEC", lambda: replace(original,
            inherited=(changed,) + original.inherited[1:], survivors=(changed, original.survivors[1])).validate())

    def test_ui_exec_has_zero_survivors_and_sanitized_environment(self):
        h = handle("ra", "fd3", "RA", owner="UI")
        clean = p.ExecBoundary("UI", (h,), (), frozenset({"ra"}))
        clean.validate()
        self.reason("UI_EXEC_ENDPOINT_LEAK", lambda: replace(clean, survivors=(h,), explicitly_closed=frozenset()).validate())
        self.reason("UNSANITIZED_EXEC", lambda: replace(clean, sanitized_environment=False).validate())

    def test_hardening_complete_and_sessions_separate(self):
        observations = tuple(p.Hardening(owner, (uid,) * 3, (uid,) * 3, "session_" + owner) for owner, uid in p.ROLE_IDS)
        p.validate_sessions(observations)
        self.reason("PARTIAL_IDENTITY_DROP", lambda: replace(observations[0], uids=(1200, 1200, 0)))
        self.reason("SUPPLEMENTARY_GROUPS", lambda: replace(observations[0], groups=(0,)))
        self.reason("CAPABILITIES_RETAINED", lambda: replace(observations[0], capabilities=frozenset({"kill"})))
        self.reason("INCOMPLETE_HARDENING", lambda: replace(observations[0], permanent=False))
        self.reason("CROSS_UID_SIGNAL_DEPENDENCY", lambda: replace(observations[0], cross_uid_signals=True))
        mixed = (replace(observations[0], session=observations[3].session),) + observations[1:]
        self.reason("SHARED_SESSION", lambda: p.validate_sessions(mixed))

    def test_deadline_types_overflow_and_no_reset(self):
        for kind in ("startup", "privileged", "shutdown"): deadline(kind)
        self.reason("DEADLINE_OVERFLOW", lambda: p.Deadline(p.MAX_TIME, p.MAX_TIME, "shutdown"))
        self.reason("DEADLINE_RESET", lambda: replace(deadline(), end=deadline().end + 1))
        for value in (True, 0.0, "0", -1):
            self.reason("INVALID_INTEGER", lambda: p.Deadline(value, 4_000_000_000, "shutdown"))

    def test_positive_child_specific_wait(self):
        state = wait(); state = state.observe(exited(state), 11, state.deadline.end)
        self.assertTrue(state.complete); self.assertIsNone(state.failure)
        self.assertEqual("DUPLICATE_WAIT", state.observe(exited(state), 12, state.deadline.end).failure)
        self.reason("POSITIVE_BOUND_WAIT_MISSING", lambda: replace(wait(), complete=True))

    def test_wait_pending_and_bounded_eintr_without_reset(self):
        state = wait()
        state = state.observe(p.WaitOutcome("running", "G", 44, 0), 11, state.deadline.end)
        self.assertFalse(state.complete); self.assertIsNone(state.failure)
        for now in (12, 13): state = state.observe(p.WaitOutcome("eintr", "G", 44, -1), now, state.deadline.end)
        failed = state.observe(p.WaitOutcome("eintr", "G", 44, -1), 14, state.deadline.end)
        self.assertEqual("EINTR_LIMIT", failed.failure)
        reset = wait().observe(p.WaitOutcome("eintr", "G", 44, -1), 11, deadline().end + 1)
        self.assertEqual("DEADLINE_RESET", reset.failure)

    def test_terminal_failures_are_sticky(self):
        for kind, reason in (("echild", "ECHILD_REQUIRED_CHILD"), ("signal", "SIGNALED_CHILD"),
                             ("interop", "WAIT_INTEROP_ERROR"), ("eof", "NOT_POSITIVE_WAIT_PROOF"),
                             ("absence", "NOT_POSITIVE_WAIT_PROOF"), ("pidfd_ready", "NOT_POSITIVE_WAIT_PROOF"),
                             ("notifier", "NOT_POSITIVE_WAIT_PROOF"), ("gone", "NOT_POSITIVE_WAIT_PROOF")):
            state = wait(); failed = state.observe(p.WaitOutcome(kind, "G", 44, -1), 11, state.deadline.end)
            self.assertEqual(reason, failed.failure)
            self.assertIs(failed, failed.observe(exited(state), 12, state.deadline.end))

    def test_wrong_wait_owner_pid_nonzero_and_signal_normality(self):
        state = wait(); good = exited(state)
        for changes, reason in (({"owner": "S"}, "COMPETING_STATUS_OWNER"), ({"target_pid": 43}, "WRONG_WAIT_TARGET"),
                                ({"returned_pid": 43}, "WRONG_RETURNED_PID"), ({"exit_code": 7}, "NONZERO_CHILD_EXIT"),
                                ({"normal": False}, "NOT_NORMAL_EXIT"), ({"returned_pid": True}, "INVALID_INTEGER")):
            self.assertEqual(reason, state.observe(replace(good, **changes), 11, state.deadline.end).failure)
        self.assertEqual("DEADLINE_EXPIRED_OR_REGRESSED", state.observe(good, state.deadline.end, state.deadline.end).failure)

    def ledger(self, completed=True):
        states = tuple(wait(child) for child in ("A", "G", "R"))
        if completed: states = tuple(s.observe(exited(s), now, s.deadline.end) for s, now in zip(states, (15, 16, 17)))
        return p.Ledger(deadline(), states)

    def test_shutdown_disposal_waits_then_drain_absence(self):
        ledger = self.ledger()
        for now, event in enumerate(("UI_STOPPED", "EOF", "R_PROTOCOL_DONE", "HOST_DISPOSED", "agent_wait", "guardian_wait", "router_wait", "drain", "absence"), 11):
            ledger = ledger.advance(event, now, ledger.deadline.end)
        self.assertTrue(ledger.complete_model)

    def test_missing_wait_and_drain_before_waits_fail_closed(self):
        ledger = self.ledger(False)
        for now, event in enumerate(("UI_STOPPED", "EOF", "R_PROTOCOL_DONE", "HOST_DISPOSED"), 11):
            ledger = ledger.advance(event, now, ledger.deadline.end)
        failed = ledger.advance("agent_wait", 15, ledger.deadline.end)
        self.assertEqual("POSITIVE_BOUND_WAIT_MISSING", failed.failure)
        self.assertIs(failed, failed.advance("absence", 16, ledger.deadline.end))
        self.assertEqual("SHUTDOWN_ORDER", self.ledger().advance("drain", 12, deadline().end).failure)
        self.assertEqual("SHUTDOWN_ORDER", self.ledger().advance("absence", 12, deadline().end).failure)

    def test_no_listener_before_readiness_and_exposure(self):
        ledger = p.Ledger(deadline("startup"), ())
        self.assertEqual("PREMATURE_EXPOSURE", ledger.advance("listener", 11, ledger.deadline.end).failure)
        self.assertEqual("PREMATURE_EXPOSURE", ledger.advance("R_EXPOSE", 11, ledger.deadline.end).failure)
        for event in ("A_READY", "UI_READY", "R_UI_READY", "R_EXPOSE", "listener"):
            ledger = ledger.advance(event, 11, ledger.deadline.end)
        self.assertIsNone(ledger.failure)

    def test_metadata_exec_validate_once_close_then_ui(self):
        state = p.MetadataState()
        for event in ("exec", "validate", "close", "ui_spawn"): state = state.advance(event)
        self.assertTrue(state.closed); self.assertIsNone(state.failure)
        self.assertEqual("METADATA_VALIDATION_ORDER", p.MetadataState().advance("validate").failure)
        self.assertEqual("METADATA_CLOSE_ORDER", p.MetadataState().advance("close").failure)
        self.assertEqual("METADATA_UI_LEAK", p.MetadataState().advance("exec").advance("ui_spawn").failure)
        repeated = p.MetadataState().advance("exec").advance("validate").advance("validate")
        self.assertEqual("METADATA_VALIDATION_ORDER", repeated.failure)
        self.assertIs(repeated, repeated.advance("close"))

    def test_router_exit_requires_disposal_and_wait_clock_regression_fails(self):
        state = wait("R")
        self.assertEqual("ROUTER_HOST_NOT_DISPOSED", state.observe(replace(exited(state), host_disposed=False), 11, state.deadline.end).failure)
        state = wait().observe(p.WaitOutcome("running", "G", 44, 0), 12, deadline().end)
        self.assertEqual("CLOCK_REGRESSION", state.observe(exited(state), 11, state.deadline.end).failure)

    def test_absence_only_or_forged_completion_cannot_complete_model(self):
        self.assertFalse(p.Ledger(deadline(), (), shutdown_events=("absence",)).complete_model)
        events = ("UI_STOPPED", "EOF", "R_PROTOCOL_DONE", "HOST_DISPOSED", "agent_wait", "guardian_wait", "router_wait", "drain", "absence")
        self.assertFalse(p.Ledger(deadline(), (), shutdown_events=events).complete_model)
        self.reason("INVALID_INTEGER", lambda: replace(wait(), complete=True,
            proof=replace(exited(wait()), returned_pid=44.0)))

    def test_malformed_injected_states_have_stable_rejections(self):
        self.reason("INVALID_BOOLEAN", lambda: p.MetadataState(executed=1))
        self.reason("METADATA_CLOSE_ORDER", lambda: p.MetadataState(closed=True))
        self.reason("INVALID_CLOCK_OR_BUDGET", lambda: p.Deadline(0, 4_000_000_000, []))
        state = wait()
        self.assertEqual("INVALID_OBSERVATION", state.observe(p.WaitOutcome([], "G", 44, -1), 11, state.deadline.end).failure)

    def test_gate_history_cannot_disappear_with_omitted_inventory(self):
        original = self.agent_exec()
        omitted = replace(original, inherited=original.survivors, explicitly_closed=frozenset(), gate_history=None)
        for ack in (False, True):
            self.reason("GATE_HISTORY_MISSING", lambda: replace(omitted, gate_ack=ack).validate())
        replace(original, inherited=original.survivors).validate()
        for changes in ({"messages": ("A_EXEC", "close_all_gate_endpoints")},
                        {"guardian_role": "07"}, {"ack_role": "04"}):
            self.reason("GATE_PROTOCOL_HISTORY_INVALID", lambda: replace(original,
                gate_history=replace(original.gate_history, **changes)).validate())
        self.reason("GATE_EXPLICIT_CLOSE_REQUIRED", lambda: replace(original,
            gate_history=replace(original.gate_history, explicitly_closed=frozenset({"gate"}))).validate())

    def test_completed_wait_constructor_rejects_out_of_budget_proof(self):
        state = wait().observe(exited(wait()), 11, deadline().end)
        for now in (0, 9, state.deadline.end, state.deadline.end + 1):
            self.reason("DEADLINE_EXPIRED_OR_REGRESSED", lambda: replace(state, last_now=now))
        self.reason("INVALID_INTEGER", lambda: replace(state, last_now=p.MAX_TIME + 1))
        self.reason("DEADLINE_RESET", lambda: replace(state.deadline, end=state.deadline.end + 1))
        failed = wait().observe(exited(wait()), deadline().end, deadline().end)
        self.assertEqual("DEADLINE_EXPIRED_OR_REGRESSED", failed.failure)
        self.assertIs(failed, failed.observe(exited(wait()), 11, deadline().end))

    def test_shutdown_wait_chronology_rejects_early_equal_and_future_proofs(self):
        events = ("UI_STOPPED", "EOF", "R_PROTOCOL_DONE", "HOST_DISPOSED", "agent_wait", "guardian_wait", "router_wait", "drain", "absence")
        for child, timestamp, reason in (("A", 11, "AGENT_WAIT_BEFORE_EOF"),
                ("A", 12, "AGENT_WAIT_BEFORE_EOF"), ("G", 14, "GUARDIAN_WAIT_BEFORE_AGENT_WAIT"),
                ("G", 15, "GUARDIAN_WAIT_BEFORE_AGENT_WAIT"), ("R", 13, "ROUTER_WAIT_BEFORE_DISPOSAL"),
                ("R", 14, "ROUTER_WAIT_BEFORE_DISPOSAL"), ("R", 18, "WAIT_OBSERVATION_IN_FUTURE")):
            with self.subTest(child=child, timestamp=timestamp):
                ledger = self.ledger()
                ledger = replace(ledger, waits=tuple(replace(w, last_now=timestamp)
                    if w.binding.child == child else w for w in ledger.waits))
                for now, event in enumerate(events, 11):
                    ledger = ledger.advance(event, now, ledger.deadline.end)
                self.assertEqual(reason, ledger.failure)
                self.assertFalse(ledger.complete_model)
                self.assertIs(ledger, ledger.advance("absence", 20, ledger.deadline.end))
        ledger = self.ledger().advance("UI_STOPPED", 11, deadline().end)
        self.assertEqual("EVENT_TIME_NOT_STRICT", ledger.advance("EOF", 11, deadline().end).failure)
        self.assertEqual("DEADLINE_EXPIRED_OR_REGRESSED", ledger.advance("EOF", deadline().end, deadline().end).failure)

    def test_shared_gate_ids_reconcile_all_channels_provenance_and_aliases(self):
        original = self.agent_exec()
        original.validate()
        for changes in ({"channel": "credential"}, {"creator": "G"}, {"required": False}):
            with self.subTest(changes=changes):
                current = tuple(replace(h, **changes) if h.channel == "gate" else h for h in original.inherited)
                self.reason("GATE_HISTORY_INCONSISTENT", lambda: replace(original, inherited=current).validate())
        for changes in ({"alias_of": None}, {"position": "scratch:other"}):
            current = tuple(replace(h, **changes) if h.endpoint == "gate_alias" else h for h in original.inherited)
            self.reason("GATE_HISTORY_INCONSISTENT", lambda: replace(original, inherited=current).validate())
        replace(original, inherited=original.survivors).validate()

    def test_startup_constructor_rejects_malformed_prefilled_prefixes(self):
        order = ("A_READY", "UI_READY", "R_UI_READY", "R_EXPOSE", "listener")
        cases = ((["A_READY"], "INVALID_STARTUP_PREFIX"), ((1,), "INVALID_STARTUP_PREFIX"),
                 (("bogus",) * 4, "UNKNOWN_STARTUP_EVENT"),
                 (("A_READY", "A_READY"), "DUPLICATE_STARTUP_EVENT"),
                 (("UI_READY",), "STARTUP_PREFIX_ORDER"),
                 (("A_READY", "R_UI_READY"), "STARTUP_PREFIX_ORDER"),
                 (("UI_READY", "A_READY"), "STARTUP_PREFIX_ORDER"),
                 (order + ("listener",), "STARTUP_PREFIX_TOO_LONG"))
        for events, reason in cases:
            with self.subTest(events=events):
                self.reason(reason, lambda: p.Ledger(deadline("startup"), (), startup_events=events))
                self.reason(reason, lambda: replace(p.Ledger(deadline("startup"), ()), startup_events=events))

    def test_valid_startup_prefixes_preserved_by_success_and_rejected_transitions(self):
        order = ("A_READY", "UI_READY", "R_UI_READY", "R_EXPOSE", "listener")
        for length in range(len(order) + 1):
            ledger = p.Ledger(deadline("startup"), (), startup_events=order[:length])
            for event in order[length:]:
                ledger = ledger.advance(event, 11, ledger.deadline.end)
                self.assertIsNone(ledger.failure)
            self.assertEqual(order, ledger.startup_events)
        for event in ("A_READY", "R_UI_READY", "R_EXPOSE", "listener"):
            ledger = p.Ledger(deadline("startup"), (), startup_events=("A_READY",))
            failed = ledger.advance(event, 11, ledger.deadline.end)
            self.assertEqual("PREMATURE_EXPOSURE", failed.failure)
            self.assertIs(failed, failed.advance("UI_READY", 12, ledger.deadline.end))
        complete = p.Ledger(deadline("startup"), (), startup_events=order)
        self.assertEqual("PREMATURE_EXPOSURE", complete.advance("listener", 11, complete.deadline.end).failure)

    def test_required_child_set_has_distinct_pids_and_numeric_parent_binding(self):
        ledger = self.ledger()
        p.validate_wait_set(ledger.waits)
        duplicate = p.WaitState(replace(binding("R"), child_pid=43), deadline(), 2)
        self.reason("DUPLICATE_REQUIRED_CHILD_PID", lambda: replace(ledger, waits=ledger.waits[:2] + (duplicate,)))
        wrong_parent = p.WaitState(replace(binding("A"), parent_pid=42), deadline(), 2)
        self.reason("WRONG_PARENT_PID", lambda: replace(ledger, waits=(wrong_parent,) + ledger.waits[1:]))
        self.reason("WRONG_PARENT_PID", lambda: replace(binding("G"), parent_pid=2))
        self.reason("PARENT_CHILD_PID_COLLISION", lambda: replace(binding("A"), parent_pid=44))
        self.reason("INCOMPLETE_REQUIRED_CHILD_SET", lambda: replace(ledger, waits=ledger.waits[:2]))


if __name__ == "__main__":
    unittest.main()
