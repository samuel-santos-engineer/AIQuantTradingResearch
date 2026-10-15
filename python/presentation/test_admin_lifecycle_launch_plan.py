"""Offline launch obligations: no process, identity, FD or wait operations."""
import importlib.util
from dataclasses import FrozenInstanceError, replace
from pathlib import Path
import sys
import unittest

spec = importlib.util.spec_from_file_location("admin_lifecycle_launch_plan",
    Path(__file__).resolve().parents[2] / "container/admin_lifecycle_launch_plan.py")
p = importlib.util.module_from_spec(spec)
sys.modules[spec.name] = p
spec.loader.exec_module(p)


class LaunchPlanTests(unittest.TestCase):
    def setUp(self):
        self.plan = p.canonical_plan()

    def reject(self, **changes):
        with self.assertRaises(p.PlanError):
            p.validate(replace(self.plan, **changes))

    def test_valid_plan_is_not_attestation(self):
        self.assertIsNone(p.validate(self.plan))
        self.assertFalse(p.PUBLIC_RUNTIME_VERIFIED)
        self.assertEqual(self.plan.roles[3].uid, self.plan.roles[4].uid)
        self.assertNotEqual(self.plan.roles[3].process, self.plan.roles[4].process)

    def test_immutable(self):
        with self.assertRaises(FrozenInstanceError): self.plan.pid1_role = "router"
        with self.assertRaises(FrozenInstanceError): self.plan.roles[0].uid = 0

    def test_wrong_parentage(self):
        for index, parent in ((0, "R"), (1, "G"), (2, "R"), (3, "S"), (4, "G")):
            roles = list(self.plan.roles); roles[index] = replace(roles[index], parent=parent)
            self.reject(roles=tuple(roles))

    def test_duplicate_role_and_process(self):
        for field, value in (("name", "supervisor"), ("process", "S")):
            roles = list(self.plan.roles); roles[1] = replace(roles[1], **{field: value})
            self.reject(roles=tuple(roles))

    def test_invalid_ids_and_assignment(self):
        for field in ("uid", "gid"):
            for value in (True, 1200.0, None, "1200", 0, -1, p.MAX_TIME + 1, 1100):
                roles = list(self.plan.roles); roles[0] = replace(roles[0], **{field: value})
                self.reject(roles=tuple(roles))
        self.reject(pid1_role="router"); self.reject(root_becomes="router")
        self.reject(identity_allocation="allocated")

    def test_missing_session_and_hardening(self):
        for index in range(5):
            roles = list(self.plan.roles); roles[index] = replace(roles[index], session="shared")
            self.reject(roles=tuple(roles))
            for obligation in p.HARDENING:
                roles = list(self.plan.roles)
                roles[index] = replace(roles[index], hardening=p.HARDENING - {obligation})
                self.reject(roles=tuple(roles))

    def test_secret_and_privilege_boundaries(self):
        self.reject(credential_policy="credential_before_fork")
        self.reject(startup=("credential_before_fork",) + self.plan.startup)
        for field in ("privileged_managed_startup", "retained_root_helper", "cross_uid_signaling", "descriptor_passing"):
            self.reject(**{field: True})

    def test_fd_ownership_is_per_process(self):
        self.assertIsNone(p.validate(self.plan))  # FD4 differs by owner/channel.
        descriptors = list(self.plan.descriptors)
        descriptors[1] = replace(descriptors[1], fd=3)
        self.reject(descriptors=tuple(descriptors))
        self.reject(descriptors=self.plan.descriptors + (p.Descriptor("UI", "SR", 3, ("runtime",)),))

    def test_c2_metadata_survives_exec_once_then_closes(self):
        index = next(i for i, d in enumerate(self.plan.descriptors) if d.channel == "A_metadata")
        for life in (("close_before_agent_exec",), ("survive_agent_exec", "runtime"),
                     ("survive_agent_exec", "validate_twice", "close_before_A_BIND")):
            ds = list(self.plan.descriptors); ds[index] = replace(ds[index], lifetime=life)
            self.reject(descriptors=tuple(ds))
        ds = list(self.plan.descriptors); ds[index] = replace(ds[index], channel="launch_gate")
        self.reject(descriptors=tuple(ds))

    def test_gate_closes_before_exec_no_invented_bootstrap_fd(self):
        for index, d in enumerate(self.plan.descriptors):
            if d.channel == "launch_gate":
                ds = list(self.plan.descriptors); ds[index] = replace(d, lifetime=("survive_agent_exec",))
                self.reject(descriptors=tuple(ds))
                if d.process == "A":
                    self.assertIsNone(d.fd)
                    ds[index] = replace(d, fd=4); self.reject(descriptors=tuple(ds))

    def test_missing_reordered_readiness_and_premature_exposure(self):
        for message in ("A_EXEC_ACK", "agent_exec", "validate_agent_metadata_once", "A_READY", "UI_READY", "R_EXPOSE"):
            self.reject(startup=tuple(m for m in self.plan.startup if m != message))
        stages = list(self.plan.startup); a = stages.index("A_READY"); b = stages.index("START_UI")
        stages[a], stages[b] = stages[b], stages[a]; self.reject(startup=tuple(stages))
        self.reject(startup=("public_listener",) + tuple(m for m in self.plan.startup if m != "public_listener"))

    def test_deadline_reset_overflow_and_types(self):
        self.reject(deadline_resets=("agent_exec",)); self.reject(clock="Stopwatch")
        for field in ("startup_origin_ns", "privileged_deadline_ns", "startup_deadline_ns",
                      "shutdown_origin_ns", "shutdown_deadline_ns", "docker_grace_ns"):
            for value in (True, 0.0, None, "0", -1, p.MAX_TIME + 1): self.reject(**{field: value})
        self.reject(startup_origin_ns=p.MAX_TIME); self.reject(shutdown_origin_ns=p.MAX_TIME)
        self.reject(shutdown_deadline_ns=35_000_000_000)
        self.reject(privileged_deadline_ns=6_000_000_000)
        self.reject(docker_grace_ns=4_000_000_000)

    def test_nonzero_clock_origin_without_reset(self):
        plan = replace(self.plan, startup_origin_ns=7, privileged_deadline_ns=5_000_000_007,
                       startup_deadline_ns=30_000_000_007, shutdown_origin_ns=40_000_000_007,
                       shutdown_deadline_ns=44_000_000_007)
        self.assertIsNone(p.validate(plan))

    def test_wait_proof_substitutions_and_wrong_owners(self):
        for index, wait in enumerate(self.plan.waits):
            for evidence in ("EOF", "ECHILD", "report", "injected_observation", "terminal_absence"):
                waits = list(self.plan.waits); waits[index] = replace(wait, evidence=evidence)
                self.reject(waits=tuple(waits))
            for change in ({"owner": "R"}, {"normal_exit": False}, {"exit_code": 7}, {"exit_code": False}):
                waits = list(self.plan.waits); waits[index] = replace(wait, **change)
                self.reject(waits=tuple(waits))

    def test_eof_absence_and_teardown_are_not_exit_proof(self):
        self.reject(eof_policy="clean_exit_proof")
        self.reject(absence_policy="before_positive_waits")
        self.reject(teardown_policy="clean_success")
        for message in ("HOST_DISPOSED", "guardian_wait_agent", "supervisor_wait_guardian", "supervisor_wait_router", "adopted_child_drain"):
            self.reject(shutdown=tuple(m for m in self.plan.shutdown if m != message))

    def endpoint_reject(self, identity, **changes):
        endpoints = tuple(replace(e, **changes) if e.identity == identity else e for e in self.plan.endpoints)
        self.reject(endpoints=endpoints)

    def test_phase_snapshots_and_ui_empty_inheritance(self):
        self.assertEqual(p.PHASES, tuple(phase.name for phase in self.plan.phases))
        postexec = dict(next(phase.ownership for phase in self.plan.phases if phase.name == "post_exec"))
        self.assertEqual("A", postexec["A-meta"])
        self.assertNotIn("A-gate", postexec)
        self.assertNotIn("A-gate-alias", postexec)
        self.assertEqual((), self.plan.boundaries[-1].retained)
        phases = list(self.plan.phases)
        phases[-1] = replace(phases[-1], ownership=phases[-1].ownership + (("A-meta", "UI"),))
        self.reject(phases=tuple(phases))

    def test_transient_gate_never_reuses_ra_or_metadata(self):
        for identity in ("A-gate", "A-gate-alias"):
            for fd in (3, 4, 5):
                self.endpoint_reject(identity, fd=fd)  # No numeric allocation authorized.
        self.endpoint_reject("A-RA", fd=4)
        self.endpoint_reject("A-meta", fd=3)

    def test_gate_and_alias_require_explicit_close_and_cloexec(self):
        for identity in ("G-gate", "A-gate", "A-gate-alias"):
            self.endpoint_reject(identity, explicit_close=None)
            self.endpoint_reject(identity, cloexec=False)
            self.endpoint_reject(identity, live_phases=p.PHASES[1:5])
        self.endpoint_reject("A-gate-alias", alias_of="missing")
        self.endpoint_reject("A-gate-alias", alias_of="R-RA")

    def test_unused_pipe_ends_and_wrong_endpoint_owner(self):
        for index in range(len(self.plan.boundaries)):
            for change in ({"close_unused": False}, {"reject_unlisted_aliases": False}):
                boundaries = list(self.plan.boundaries)
                boundaries[index] = replace(boundaries[index], **change)
                self.reject(boundaries=tuple(boundaries))
        self.endpoint_reject("A-gate", owner="R")
        self.endpoint_reject("A-meta", owner="UI")
        boundaries = list(self.plan.boundaries)
        boundaries[-1] = replace(boundaries[-1], retained=("A-meta",))
        self.reject(boundaries=tuple(boundaries))

    def test_executed_agent_metadata_once_and_no_leak(self):
        self.endpoint_reject("A-meta", live_phases=p.PHASES[1:4])
        self.endpoint_reject("A-meta", validations=())
        self.endpoint_reject("A-meta", validations=("executed_agent_once", "executed_agent_once"))
        self.endpoint_reject("A-meta", validations=("bootstrap_once",))
        self.endpoint_reject("A-meta", explicit_close=None)
        self.endpoint_reject("A-meta", live_phases=p.PHASES[1:])

    def test_explicit_session_alias_sigcont_risk(self):
        for target, source in ((0, 3), (1, 3), (2, 3), (3, 2), (4, 0)):
            roles = list(self.plan.roles)
            roles[target] = replace(roles[target], session=roles[source].session)
            self.reject(roles=tuple(roles))

    def test_explicit_premature_r_expose_and_missing_wait(self):
        stages = list(self.plan.startup); stages.remove("R_EXPOSE")
        stages.insert(stages.index("A_READY"), "R_EXPOSE")
        self.reject(startup=tuple(stages))
        self.reject(waits=self.plan.waits[1:])
        self.reject(deadline_resets=("shutdown",))

    def test_ambiguous_endpoint_phase_and_duplicate_identity(self):
        self.endpoint_reject("A-meta", live_phases=("unknown",))
        self.reject(endpoints=self.plan.endpoints + (self.plan.endpoints[0],))
        self.reject(phases=self.plan.phases[::-1])

    def trace_mutation(self, target_operation, target_owner, **changes):
        trace = list(self.plan.trace)
        index = next(i for i, s in enumerate(trace) if s.operation == target_operation and s.owner == target_owner)
        trace[index] = replace(trace[index], **changes)
        self.reject(trace=tuple(trace))

    def test_canonical_creation_handoff_exec_shutdown_snapshots(self):
        snapshots = p.validate_trace(self.plan.trace)
        self.assertTrue(all(not handles for owner, handles in snapshots[-1]))
        fork = next(i for i, s in enumerate(self.plan.trace) if s.operation == "fork" and s.child == "A")
        parent = dict(dict(snapshots[fork - 1])["G"])
        self.assertTrue({"A-gate", "A-gate-alias", "A-RA", "A-meta"} <= parent.keys())
        closed = next(i for i, s in enumerate(self.plan.trace) if s.operation == "close_unused" and s.owner == "A")
        self.assertEqual(5, dict(dict(snapshots[fork])["A"])["G-meta"])
        self.assertNotIn("G-meta", dict(dict(snapshots[closed])["A"]))
        slot = next(i for i, s in enumerate(self.plan.trace) if s.operation == "require_free_slot")
        self.assertNotIn(5, dict(dict(snapshots[slot])["A"]).values())
        executed = next(i for i, s in enumerate(self.plan.trace) if s.operation == "exec")
        self.assertEqual({"A-RA": 3, "A-meta": 4}, dict(dict(snapshots[executed])["A"]))
        ui = next(i for i, s in enumerate(self.plan.trace) if s.operation == "fork" and s.child == "UI")
        ui_closed = next(i for i, s in enumerate(self.plan.trace) if s.operation == "close_unused" and s.owner == "UI")
        self.assertEqual((), dict(snapshots[ui_closed])["UI"])

    def test_unavailable_parent_handoff_and_wrong_parent(self):
        self.trace_mutation("fork", "G", items=("nonexistent",))
        self.trace_mutation("fork", "G", owner="R")
        trace = list(self.plan.trace)
        root_g = next(i for i, s in enumerate(trace) if s.operation == "fork" and s.child == "G")
        trace[root_g] = replace(trace[root_g], items=tuple(item for item in trace[root_g].items if item != "A-RA"))
        self.reject(trace=tuple(trace))

    def test_inherited_fd5_occupied_and_live_slot_overwrite(self):
        trace = list(self.plan.trace)
        slot = next(i for i, s in enumerate(trace) if s.operation == "require_free_slot")
        trace.insert(slot, p.Step("assign", "A", ("A-gate-alias",), fd=5))
        self.reject(trace=tuple(trace))
        assign = next(i for i, s in enumerate(self.plan.trace) if s.operation == "assign" and s.items == ("A-gate",))
        for fd in (3, 4):
            trace = list(self.plan.trace); trace[assign] = replace(trace[assign], fd=fd)
            self.reject(trace=tuple(trace))

    def test_retained_unused_fork_and_exec_aliases(self):
        self.trace_mutation("fork", "A", items=("A-RA",))  # UI must receive none.
        trace = list(self.plan.trace)
        close = next(i for i, s in enumerate(trace) if s.operation == "close" and s.owner == "A" and "A-gate" in s.items)
        trace[close] = replace(trace[close], items=("A-gate",))
        self.reject(trace=tuple(trace))
        self.trace_mutation("fork", "G", items=("G-gate", "A-gate", "A-gate-alias", "A-RA", "A-meta"))

    def test_guardian_gate_not_agent_metadata_and_duplicate_close(self):
        trace = list(self.plan.trace)
        gate = next(i for i, s in enumerate(trace) if s.operation == "close" and s.items == ("G-gate",))
        trace[gate] = replace(trace[gate], items=("A-meta",))
        self.reject(trace=tuple(trace))
        trace = list(self.plan.trace); trace.insert(gate + 1, trace[gate])
        self.reject(trace=tuple(trace))

    def test_shutdown_endpoint_leak_and_premature_control_close(self):
        for endpoint in ("A-RA", "R-RA", "R-SR", "S-SR", "G-SG", "S-SG"):
            trace = tuple(s for s in self.plan.trace if not (s.operation == "close" and s.items == (endpoint,)))
            self.reject(trace=trace)
        trace = list(self.plan.trace)
        close = next(i for i, s in enumerate(trace) if s.operation == "close" and s.items == ("A-RA",))
        step = trace.pop(close); trace.insert(1, step)
        self.reject(trace=tuple(trace))

    def test_handoff_phase_order_and_duplicate_creation(self):
        trace = list(self.plan.trace); trace.insert(1, trace[0]); self.reject(trace=tuple(trace))
        self.trace_mutation("R_EXPOSE", "S", operation="public_listener")
        self.reject(trace=tuple(s for s in self.plan.trace if s.operation != "permanent_hardening"))
        self.reject(trace=tuple(s for s in self.plan.trace if s.operation != "guardian_wait_agent"))

    def reason_reject(self, trace, reason):
        with self.assertRaises(p.PlanError) as error:
            p.validate_trace(tuple(trace))
        self.assertEqual(reason, str(error.exception))

    def test_metadata_emission_requires_established_child_binding(self):
        for message, before in (("META_ROUTER", "R"), ("META_GUARDIAN", "G"), ("META_AGENT", "A")):
            trace = list(self.plan.trace)
            emission = next(s for s in trace if s.operation == "emit_metadata" and s.message == message)
            index = next(i for i, s in enumerate(trace) if s.operation == "fork" and s.child == before)
            trace.insert(index, emission)
            self.reason_reject(trace, "METADATA_BINDING_MISSING")

    def test_metadata_validation_before_agent_binding_and_before_emission(self):
        trace = list(self.plan.trace)
        validation = next(s for s in trace if s.operation == "validate_metadata" and s.owner == "G")
        fork = next(i for i, s in enumerate(trace) if s.operation == "fork" and s.child == "A")
        trace.insert(fork, validation)
        self.reason_reject(trace, "METADATA_BINDING_MISSING")
        trace = list(self.plan.trace)
        trace = [s for s in trace if not (s.operation == "emit_metadata" and s.items == ("G-meta-writer",) and s.message == "META_AGENT")]
        self.reason_reject(trace, "METADATA_NOT_EMITTED")

    def test_missing_or_invalid_positive_binding_rejected_semantically(self):
        for binding in (None, p.PidBinding("A", "G", positive=False),
                        p.PidBinding("A", "G", positive=1), p.PidBinding("A", "G", source="placeholder"),
                        p.PidBinding("A", "R")):
            trace = [replace(s, binding=binding) if s.operation == "fork" and s.child == "A" else s for s in self.plan.trace]
            self.reason_reject(trace, "PID_BINDING_INVALID")

    def test_missing_inherited_copy_close_has_specific_reason(self):
        trace = [s for s in self.plan.trace if not (s.operation == "close_unused" and s.owner == "A")]
        self.reason_reject(trace, "INHERITED_COPY_NOT_CLOSED")
        trace = [replace(s, items=tuple(item for item in s.items if item != "G-meta"))
                 if s.operation == "close_unused" and s.owner == "A" else s for s in self.plan.trace]
        self.reason_reject(trace, "INHERITED_COPY_NOT_CLOSED")

    def test_fd5_occupancy_overwrite_and_parent_possession_reasons(self):
        trace = list(self.plan.trace)
        index = next(i for i, s in enumerate(trace) if s.operation == "require_free_slot")
        trace.insert(index, p.Step("assign", "A", ("A-gate-alias",), fd=5))
        self.reason_reject(trace, "FD5_OCCUPIED")
        trace = [replace(s, fd=4) if s.operation == "assign" and s.items == ("A-gate",) else s for s in self.plan.trace]
        self.reason_reject(trace, "FD_SLOT_OCCUPIED")
        trace = [replace(s, items=("nonexistent",)) if s.operation == "fork" and s.child == "A" else s for s in self.plan.trace]
        self.reason_reject(trace, "PARENT_ENDPOINT_UNAVAILABLE")

    def test_gate_and_control_close_order_reason(self):
        trace = list(self.plan.trace)
        gate = next(s for s in trace if s.operation == "close" and s.items == ("G-gate",))
        index = next(i for i, s in enumerate(trace) if s.operation == "A_EXEC")
        trace.insert(index, gate)
        self.reason_reject(trace, "GATE_CLOSE_BEFORE_ACK")
        trace = list(self.plan.trace)
        close = next(s for s in trace if s.operation == "close" and s.items == ("A-RA",))
        index = next(i for i, s in enumerate(trace) if s.operation == "STOP_UI")
        trace.insert(index, close)
        self.reason_reject(trace, "CONTROL_CLOSE_BEFORE_MILESTONE")

    def plan_reason_reject(self, reason, **changes):
        with self.assertRaises(p.PlanError) as error:
            p.validate(replace(self.plan, **changes))
        self.assertEqual(reason, str(error.exception))

    def test_guardian_metadata_phase_lifetime_matches_trace(self):
        metadata = next(e for e in self.plan.endpoints if e.identity == "G-meta")
        self.assertEqual(("bootstrap",), metadata.live_phases)
        post = next(phase for phase in self.plan.phases if phase.name == "post_hardening")
        self.assertNotIn(("G-meta", "G"), post.ownership)
        endpoints = tuple(replace(e, live_phases=e.live_phases + ("post_hardening",))
                          if e.identity == "G-meta" else e for e in self.plan.endpoints)
        self.plan_reason_reject("PHASE_TRACE_MISMATCH", endpoints=endpoints)
        endpoints = tuple(replace(e, live_phases=()) if e.identity == "G-meta" else e for e in self.plan.endpoints)
        self.plan_reason_reject("PHASE_TRACE_MISMATCH", endpoints=endpoints)

    def test_independent_trace_lifetime_change_mismatches_projection(self):
        trace = list(self.plan.trace)
        index = next(i for i, s in enumerate(trace) if s.operation == "close" and s.items == ("G-meta",))
        close = trace.pop(index)
        hardened = next(i for i, s in enumerate(trace) if s.operation == "permanent_hardening")
        trace.insert(hardened + 1, close)
        self.plan_reason_reject("PHASE_TRACE_MISMATCH", trace=tuple(trace))
        phases = tuple(replace(phase, ownership=phase.ownership + (("G-meta", "G"),))
                       if phase.name == "post_hardening" else phase for phase in self.plan.phases)
        self.plan_reason_reject("PHASE_TRACE_MISMATCH", phases=phases)

    def test_ui_child_inheritance_is_distinct_from_ui_exec(self):
        child = next(phase for phase in self.plan.phases if phase.name == "ui_child_bootstrap")
        executed = next(phase for phase in self.plan.phases if phase.name == "ui_exec")
        self.assertIn(("A-RA", "UI"), child.ownership)
        self.assertFalse(any(owner == "UI" for identity, owner in executed.ownership))
        self.assertEqual(self.plan.phases, p.project_phases(self.plan.trace))

    def test_ui_exec_rejects_each_unwanted_endpoint_category(self):
        # Direct semantic trace tests reach ui_exec before canonical comparison.
        for leaked in ("A-RA", "A-meta", "R-key", "A-gate", "A-gate-alias"):
            trace = list(self.plan.trace)
            fork = next(i for i, s in enumerate(trace) if s.operation == "fork" and s.child == "UI")
            if leaked != "A-RA":
                trace.insert(fork, p.Step("duplicate_endpoint", "A", ("A-RA", leaked)))
                fork += 1
            trace[fork] = replace(trace[fork], items=(leaked,))
            closed = next(i for i, s in enumerate(trace) if s.operation == "close_unused" and s.owner == "UI")
            trace[closed] = replace(trace[closed], items=() if leaked == "A-RA" else ("A-RA",))
            self.reason_reject(trace, "UI_EXEC_ENDPOINT_LEAK")

    def test_missing_ui_bootstrap_close_rejected_at_exec(self):
        trace = [s for s in self.plan.trace if not (s.operation == "close_unused" and s.owner == "UI")]
        self.reason_reject(trace, "UI_EXEC_ENDPOINT_LEAK")
        trace = list(self.plan.trace)
        executed = next(i for i, s in enumerate(trace) if s.operation == "ui_exec")
        step = trace.pop(executed)
        close = next(i for i, s in enumerate(trace) if s.operation == "close_unused" and s.owner == "UI")
        trace.insert(close, step)
        self.reason_reject(trace, "UI_EXEC_ENDPOINT_LEAK")

    def test_ui_cleanup_and_ready_cannot_precede_child_or_exec(self):
        trace = list(self.plan.trace)
        executed = next(s for s in trace if s.operation == "ui_exec")
        fork = next(i for i, s in enumerate(trace) if s.operation == "fork" and s.child == "UI")
        trace.insert(fork, executed)
        self.reason_reject(trace, "UI_EXEC_WITHOUT_CHILD_BOOTSTRAP")
        trace = [s for s in self.plan.trace if s.operation != "ui_exec"]
        self.reason_reject(trace, "UI_READY_BEFORE_EXEC")
        trace = list(self.plan.trace)
        close = next(s for s in trace if s.operation == "close_unused" and s.owner == "UI")
        fork = next(i for i, s in enumerate(trace) if s.operation == "fork" and s.child == "UI")
        trace.insert(fork, close)
        self.reason_reject(trace, "PARENT_ENDPOINT_UNAVAILABLE")


if __name__ == "__main__":
    unittest.main()
