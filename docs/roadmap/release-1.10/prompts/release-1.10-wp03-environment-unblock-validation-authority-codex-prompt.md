# Release 1.10 WP03 — Narrow Environment Unblock & Validation Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, path ownership, and governance authority.
- **GPT-5.6 Terra** — PRIMARY execution authority for this narrow local-environment unblock and validation-only authority.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10**

Work package context:

**WP03 — Infrastructure Provider, Persistence & Failure Instrumentation**

Issue: **#244**

Milestone: **#59**

Project: **#2**

This authority exists solely because full Infrastructure validation is blocked by the local Windows Application Control / Smart App Control environment before Worker-dependent tests can execute their assertions.

Observed blocker:

- `System.IO.FileLoadException`
- `AIQuantTradingResearch.Worker.dll`
- Windows Application Control error `0x800711C7`

Focused WP03 listener validation already passes:

**25/25**

This authority is NOT:

- a WP03 implementation authority;
- a new observability contract;
- a Luna reconciliation;
- a project/package refactor;
- a release-signing authority;
- a source-code feature authority;
- a WP04 authority.

---

# Primary objective

Restore the **already-approved local development execution/signing path** required for Windows to load the Worker assembly, using the minimum local/environment-only action necessary.

Then rerun the previously blocked validation to determine whether WP03 itself passes.

The authority MUST NOT change application semantics, observability semantics, architecture, package contracts, or release artifacts.

---

# Entry evidence

Treat the following as accepted unless current inspection contradicts it:

- WP03 focused listener tests: **25/25 PASS**
- current WP03 production/test implementation remains within frozen scope
- Infrastructure build was clean
- full Infrastructure validation is blocked before Worker-dependent assertions by Application Control
- `AIQuantTradingResearch.Worker.dll` load fails with `0x800711C7`
- #244 remains Open / Backlog
- Git mutations: ZERO
- GitHub mutations: ZERO
- WP04 not started.

Emit after verification:

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK ENTRY AUDIT: PASS`

---

# Allowed scope

This authority may perform only the minimum local/environment action required to restore the already-approved development execution path.

Allowed categories include, only where already established by repository/dev setup:

1. invoking the existing local development signing procedure;
2. invoking an existing repository script/tool for local signing;
3. regenerating/reapplying an already-established local development certificate if that procedure is explicitly documented and does not change tracked repository contracts;
4. locally trusting/importing that development certificate when required;
5. signing the exact locally built assembly/artifact(s) required for Worker-dependent tests;
6. rebuilding locally with the existing documented signing mechanism;
7. cleaning/rebuilding generated local output only when necessary to produce executable signed binaries;
8. rerunning affected tests/validation.

Prefer an existing documented repository workflow over inventing a new mechanism.

---

# Explicitly forbidden scope

Do NOT:

- modify application source code;
- modify WP03 production or test code;
- modify `.csproj` files;
- modify package versions or dependencies;
- modify solution structure;
- modify build properties in tracked files;
- modify schema/migrations;
- modify Python files/dependencies;
- weaken or disable Windows security controls globally;
- disable Smart App Control/App Control;
- alter system security policy;
- add broad exclusions;
- bypass signature enforcement through unsafe mechanisms;
- change release signing configuration;
- create production/release certificates;
- commit certificates or private keys;
- expose or print private-key material;
- stage/commit/push/branch/merge/rebase/tag;
- mutate GitHub;
- close #244;
- begin WP04.

If the only way to proceed requires any forbidden action, BLOCK.

---

# Security constraints

Any certificate/key handling must remain local and development-only.

Never:

- output private key contents;
- commit certificate/key files;
- place keys in tracked repository paths unless an existing ignored/local-only path is explicitly established;
- publish development certificates;
- convert a dev-only process into release signing.

If a local certificate already exists, prefer reuse.

If certificate generation is required, it must be:

- local;
- self-signed/development-only;
- bounded to the existing documented workflow;
- excluded from Git.

Emit:

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK SECURITY BOUNDARY: PASS`

---

# Phase 0 — Read-only environment diagnosis

Before mutation/action, inspect:

- current repo/branch/HEAD;
- `git status --short`;
- documented local build/signing instructions;
- existing build/sign scripts;
- existing local signing configuration;
- Worker assembly path;
- signature state if deterministically inspectable;
- exact failing test command and exception;
- whether the failure is reproducible.

Confirm the failure is environmental and occurs before the relevant Worker-dependent test assertions.

Emit:

`RELEASE 1.10 WP03 APPLICATION CONTROL BLOCK: CONFIRMED`

If not reproducible, rerun the blocked validation and continue based on actual evidence.

---

# Phase 1 — Existing approved signing-path discovery

Identify the already-approved development mechanism.

Prefer, in order:

1. existing repository-local documented signing command/script;
2. existing local build configuration explicitly used by prior accepted releases;
3. already-present local development certificate/trust setup.

Do not design a new tracked signing architecture.

Report:

- mechanism;
- whether tracked files are mutated;
- certificate location class (local/user store/ignored local file; never secret contents);
- exact output artifacts affected.

Require tracked repository mutation:

**ZERO**

Emit:

`RELEASE 1.10 WP03 APPROVED DEV SIGNING PATH: FOUND`

If no approved path exists, BLOCK and request a narrow Luna/Terra governance decision rather than inventing one.

---

# Phase 2 — Minimal environment repair

Perform only the minimal action required to make the Worker assembly loadable.

Examples, if consistent with the existing approved workflow:

- apply/reapply local signing;
- trust the existing local dev certificate;
- regenerate the local dev certificate via the established mechanism;
- rebuild the Worker assembly through the existing signing path.

Do not alter tracked source/project/package files.

Immediately verify:

- assembly exists;
- expected local signature/trust state is present where inspectable;
- Git worktree tracked-path delta did not expand because of the unblock action.

Emit:

`RELEASE 1.10 WP03 WORKER LOCAL EXECUTION PATH: RESTORED`

---

# Phase 3 — Direct Worker load smoke test

Before running the full suite, perform the narrowest deterministic execution/load check available.

Require:

- `AIQuantTradingResearch.Worker.dll` is no longer rejected with `0x800711C7`;
- no `System.IO.FileLoadException` caused by Application Control;
- Worker-dependent test host can load the required assembly.

Do not claim functional correctness yet.

Emit:

`RELEASE 1.10 WP03 WORKER APPLICATION CONTROL LOAD CHECK: PASS`

---

# Phase 4 — Re-run previously blocked Infrastructure validation

Rerun the exact full Infrastructure validation that previously blocked.

Report:

- exact test count;
- passed;
- failed;
- skipped;
- warnings/errors;
- whether any Application Control load failure remains.

Distinguish:

## Environment result

Whether the Windows Application Control block is resolved.

## WP03 result

Whether tests now execute and expose any genuine WP03-attributable failures.

If all pass:

`RELEASE 1.10 WP03 INFRASTRUCTURE VALIDATION AFTER ENVIRONMENT UNBLOCK: PASS`

If tests execute but fail for WP03 logic:
do not repair WP03 here; hand back to same WP03 V2 authority with exact failures.

If Application Control still blocks:
continue only with additional already-approved local-environment steps inside this authority.

---

# Phase 5 — Re-run focused WP03 proof

Rerun the existing focused WP03 listener tests to ensure the environment action did not disturb them.

Expected prior baseline:

25/25 PASS.

Report actual result.

Require:

`RELEASE 1.10 WP03 FOCUSED LISTENER REVALIDATION: PASS`

---

# Phase 6 — Architecture/Application validation needed for handback

Run the minimum additional validation necessary to prove the environment action itself did not affect repository behavior.

At minimum, where applicable:

- Application tests;
- architecture/no-bypass tests;
- relevant build(s).

Do not use this phase to declare WP03 final acceptance unless the same WP03 V2 authority's remaining gates are separately satisfied.

Emit:

`RELEASE 1.10 WP03 ENVIRONMENT-UNBLOCK REGRESSION CHECK: PASS`

---

# Phase 7 — Repository/Git safety audit

Prove the environment unblock did not mutate tracked repository contracts.

Report:

- `git status --short`;
- any generated/untracked signing artifacts;
- whether each is pre-existing, ignored, or newly local-only;
- tracked diff attributable to this authority.

Required:

Tracked repository mutations attributable to this authority:

**ZERO**

Git mutations:

**ZERO**

GitHub mutations:

**ZERO**

Emit:

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK REPOSITORY MUTATIONS: ZERO TRACKED CONTRACT MUTATIONS`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK GITHUB MUTATIONS: ZERO`

---

# Phase 8 — Security/residue audit

Verify:

- no private key material printed;
- no certificate/key committed/staged;
- no global security control disabled;
- no persistent unsafe exclusion added;
- no unexpected testhost/Worker process residue;
- no unexpected listener residue.

Emit:

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK SECURITY/RESIDUE: PASS`

---

# Phase 9 — Handoff back to same WP03 V2 authority

This authority MUST NOT close #244 and MUST NOT emit WP03 final acceptance.

If the environment block is resolved and Infrastructure validation passes, hand control back to:

**Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority V2 — GPT-5.6 Terra**

The resumed WP03 V2 authority must continue the remaining gates, including as applicable:

- topology confirmation;
- cardinality confirmation;
- failure semantics;
- security;
- full affected validation accounting;
- functional preservation;
- exact path/hunk ownership;
- acceptance matrix;
- `RELEASE 1.10 WP03 ACCEPTANCE: PASS`;
- only then #244 Closed/Done.

Emit:

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK → WP03 V2 HANDOFF: READY`

---

# Required success markers

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK ENTRY AUDIT: PASS`

`RELEASE 1.10 WP03 APPLICATION CONTROL BLOCK: CONFIRMED`

`RELEASE 1.10 WP03 APPROVED DEV SIGNING PATH: FOUND`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK SECURITY BOUNDARY: PASS`

`RELEASE 1.10 WP03 WORKER LOCAL EXECUTION PATH: RESTORED`

`RELEASE 1.10 WP03 WORKER APPLICATION CONTROL LOAD CHECK: PASS`

`RELEASE 1.10 WP03 INFRASTRUCTURE VALIDATION AFTER ENVIRONMENT UNBLOCK: PASS`

`RELEASE 1.10 WP03 FOCUSED LISTENER REVALIDATION: PASS`

`RELEASE 1.10 WP03 ENVIRONMENT-UNBLOCK REGRESSION CHECK: PASS`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK REPOSITORY MUTATIONS: ZERO TRACKED CONTRACT MUTATIONS`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK GITHUB MUTATIONS: ZERO`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK SECURITY/RESIDUE: PASS`

`RELEASE 1.10 WP03 ENVIRONMENT UNBLOCK → WP03 V2 HANDOFF: READY`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Exact terminal:

`RELEASE 1.10 WP03 — NARROW ENVIRONMENT UNBLOCK & VALIDATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- no already-approved development signing/execution path exists;
- resolving Application Control would require tracked project/package/source changes;
- resolving it requires disabling/weaking system security policy;
- a certificate/private key would need to be committed or exposed;
- the assembly remains blocked after all already-approved local mechanisms are correctly applied;
- the environment is resolved but Infrastructure validation exposes genuine WP03 failures that must be repaired in WP03 code/tests.

If WP03 failures become visible after the environment is unblocked:

- do not repair them under this authority;
- report exact failing tests/assertions;
- hand back to the same WP03 V2 authority.

GitHub mutations remain ZERO.

#244 remains Open / Backlog.

WP04 remains blocked.

Exact blocked terminal:

`RELEASE 1.10 WP03 — NARROW ENVIRONMENT UNBLOCK & VALIDATION AUTHORITY BLOCKED`
