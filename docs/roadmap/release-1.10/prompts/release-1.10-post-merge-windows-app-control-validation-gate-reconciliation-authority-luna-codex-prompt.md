# Release 1.10 — Post-Merge Windows App Control Validation-Gate Reconciliation Authority

## Model assignment

- **GPT-5.6 Luna** — PRIMARY authority for contract, policy, architecture, reconciliation, acceptance criteria, and the exact post-merge validation recovery boundary.
- **GPT-5.6 Terra** — implementation/validation execution only after Luna freezes the recovery procedure.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Purpose

Reconcile one narrow Release 1.10 post-merge validation blocker:

Freshly rebuilt and validly signed first-party .NET DLLs fail to load during the required Infrastructure post-merge validation under Windows App Control with:

`0x800711C7`

The objective is NOT to modify the product.

The objective is to determine, prove, and freeze the exact environment-only recovery and validation procedure that GPT-5.6 Terra may execute to resume the already-started PR #250 immediate post-merge verification authority.

---

# Binding release state

PR #250 is merged.

Accepted candidate:

`7148c9b347b5b7f0a162157e6c8dee25fdee372c`

Accepted candidate parent:

`5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`

Verified PR #250 merge commit:

`eb9601596d9a9dd68f1f8a7c963906a76e5a2833`

Verified merged payload:

- exact 103/103 canonical candidate paths;
- zero missing;
- zero extra;
- payload exactly matches accepted candidate.

Repository synchronization:

- local `main` safely fast-forwarded;
- local `main` = `origin/main`;
- staging empty;
- tracked changes = 0;
- 16 untracked execution-control prompts preserved.

GitHub lifecycle remains:

- #242–#249 Closed/Done;
- milestone #59 Open, 0 open / 8 closed;
- no tag/version/GitHub Release publication authorized.

The prior immediate post-merge authority is BLOCKED only because required Infrastructure validation cannot currently obtain a terminal test result under Windows App Control.

Emit:

`RELEASE 1.10 WAC RECONCILIATION ENTRY: PASS`

---

# Known evidence

Treat as evidence to verify, not assumptions to broaden:

- failure occurs after fresh rebuild;
- affected artifacts are first-party DLL outputs;
- affected DLLs were signed using the governed local development signing procedure;
- certificate is valid;
- Authenticode signatures are valid;
- load still fails with Windows App Control error `0x800711C7`;
- no repository content mutation has been required;
- no proven product/test regression exists;
- earlier pre-merge Infrastructure suite obtained 191/191 PASS after environment recovery;
- earlier runner/hang reconciliation separately proved a non-interactive wrapper/runner issue and obtained Infrastructure 191/191 PASS in 29.3 seconds;
- current blocker is specifically the post-merge environment/load gate.

Do not collapse the prior runner/hang issue into the current App Control issue unless evidence proves they are causally connected.

---

# Authority boundary

This is a **Luna reconciliation authority**.

Allowed:

- read-only repository inspection;
- read-only Git/GitHub inspection;
- inspect Windows App Control / Code Integrity evidence;
- inspect certificate stores;
- inspect Authenticode signatures;
- inspect file hashes/timestamps/paths;
- inspect build/test outputs;
- inspect existing signing/runbook documentation;
- inspect prior frozen Release 1.10 authorities/planning;
- execute bounded diagnostic commands that do not mutate repository content;
- perform disposable/environment-only experiments when reversible and explicitly recorded;
- terminate only processes launched/owned by this diagnostic.

Forbidden:

- production source edits;
- test edits;
- documentation edits;
- planning edits;
- authority-file edits in repository;
- project/package edits;
- schema/migration edits;
- signing configuration edits;
- permanent machine security-policy weakening;
- disabling Windows App Control;
- bypassing Smart App Control/WDAC policy;
- adding trust exceptions merely to make tests pass;
- Git staging/commit/push/branch-history mutation;
- GitHub issue/Project/milestone/PR/tag/Release mutation.

Repository content mutations must remain ZERO.

---

# Phase 1 — Reproduce and classify the gate

Reproduce the failure with the smallest governed command that loads the affected first-party Infrastructure test dependency.

Capture:

- exact command;
- exact failing assembly/path;
- exact HRESULT/error;
- process;
- build configuration;
- file timestamp/hash;
- signature status;
- signer certificate identity/thumbprint as appropriate;
- certificate validity/trust result;
- whether failure is deterministic.

Distinguish:

A. unsigned artifact;
B. invalid signature;
C. untrusted certificate;
D. App Control reputation/policy/cache/state issue despite valid signature;
E. path/file replacement behavior;
F. stale output;
G. dependency mismatch;
H. test/product failure unrelated to App Control;
I. unknown.

Do not prescribe recovery until classification evidence is sufficient.

Emit:

`RELEASE 1.10 WAC FAILURE CLASSIFICATION: FROZEN`

---

# Phase 2 — Establish repository innocence or identify contradiction

Prove whether the blocker is environment-only.

Required comparisons:

- `main` tree at `eb9601596...` versus merged candidate content;
- package/project/signing-config state;
- affected source/test files;
- schema state;
- clean rebuild outputs;
- signing operation provenance.

Required conclusion must be one:

`ENVIRONMENT-ONLY`

or

`REPOSITORY/PRODUCT CAUSALITY NOT EXCLUDED`

If repository/product causality cannot be excluded, BLOCK and hand back to Luna for a different scoped reconciliation. Do not authorize Terra validation resumption.

Emit only if proven:

`RELEASE 1.10 WAC REPOSITORY INNOCENCE: PASS — ENVIRONMENT-ONLY`

---

# Phase 3 — Inspect Windows security evidence

Use available Windows-native evidence to determine why a validly signed first-party DLL is rejected.

Inspect as applicable:

- Authenticode status;
- signer chain/trust;
- certificate store placement;
- Code Integrity / App Control event logs;
- Smart App Control / WDAC state;
- relevant event IDs and policy identifiers;
- file origin/zone metadata where relevant;
- whether rebuild/replacement creates a new hash requiring reevaluation;
- whether signing before/after build materially changes acceptance;
- whether the documented development signing procedure matches the actual artifact lifecycle.

Do not weaken machine security policy.

Record exact evidence sufficient for another operator to reproduce the diagnosis.

Emit:

`RELEASE 1.10 WAC SECURITY EVIDENCE: CAPTURED`

---

# Phase 4 — Bounded recovery experiments

Evaluate only safe, reversible, environment-only recovery procedures consistent with existing project governance.

Candidate experiments may include, if justified by evidence:

- clean affected first-party build outputs;
- rebuild;
- sign all required first-party executable/DLL outputs using the existing governed local development certificate/procedure;
- verify signatures after signing;
- ensure no subsequent build overwrites signed artifacts before test execution;
- run the affected test command directly/interactively rather than through a previously diagnosed problematic wrapper;
- start a fresh testhost/process after signing;
- clear only project-owned temporary/test output when safe;
- wait/retry only when evidence indicates App Control reevaluation/cache timing;
- use an existing documented repository script/procedure exactly as designed.

Not allowed:

- disable App Control;
- switch policy to Audit merely to pass;
- install arbitrary trust roots;
- permanently alter WDAC/App Control policy;
- download replacement binaries;
- edit project signing settings;
- patch test/product logic;
- suppress/ignore the failing validation.

For each experiment record:

- precondition;
- exact action;
- repository mutation count;
- security-policy mutation count;
- result;
- whether terminal Infrastructure validation becomes possible.

Emit:

`RELEASE 1.10 WAC BOUNDED RECOVERY EXPERIMENTS: COMPLETE`

---

# Phase 5 — Freeze the Terra recovery contract

If a safe deterministic recovery is established, freeze it literally.

The Terra contract must specify:

1. exact clean/build order;
2. exact first-party outputs requiring signing, preferably by deterministic selector rather than stale hard-coded filenames;
3. exact signing mechanism already governed by project documentation;
4. exact signature verification;
5. exact rule forbidding rebuild after signing before validation;
6. exact Infrastructure test invocation;
7. direct/interactive invocation requirement if needed;
8. timeout/termination expectations;
9. process ownership/cleanup rules;
10. proof that no repository/security-policy mutation occurs;
11. subsequent full post-merge validation sequence.

The contract must be executable without Terra making architectural/policy choices.

Emit:

`RELEASE 1.10 WAC → TERRA RECOVERY CONTRACT: FROZEN`

---

# Phase 6 — Acceptance semantics

Freeze what counts as post-merge validation PASS.

Infrastructure must produce a terminal result:

`191/191 PASS`

or the actual repository-defined total if authoritative post-merge discovery proves the count legitimately changed.

A mere absence of `0x800711C7` is insufficient.

A hung/non-terminating wrapper is insufficient.

Skipping affected tests is insufficient.

Ignoring App Control load failures is insufficient.

If the frozen recovery works and Infrastructure passes, Terra must continue the remaining immediate post-merge validation required by the original authority.

If the recovery fails again with valid signatures and the same environment gate, Terra must BLOCK without repository mutation and return exact evidence.

Emit:

`RELEASE 1.10 WAC POST-MERGE ACCEPTANCE SEMANTICS: FROZEN`

---

# Phase 7 — Lifecycle boundary

Freeze:

- #242–#249 remain Closed/Done;
- milestone #59 remains Open, 0 open / 8 closed;
- no tag/version;
- no GitHub Release;
- no issue/Project mutation.

Release-completion work remains forbidden until the immediate post-merge verification authority reaches COMPLETE.

Emit:

`RELEASE 1.10 WAC RELEASE-LIFECYCLE BOUNDARY: PRESERVED`

---

# Phase 8 — Mutation audit

Expected:

- repository content mutations: ZERO;
- staged paths: ZERO;
- commits: ZERO;
- pushes: ZERO;
- GitHub mutations: ZERO;
- permanent security-policy mutations: ZERO.

Environment-only diagnostic actions must be enumerated separately and must be reversible/bounded.

Preserve the 16 untracked execution-control prompts.

Emit:

`RELEASE 1.10 WAC RECONCILIATION MUTATION AUDIT: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 WAC RECONCILIATION ENTRY: PASS`

`RELEASE 1.10 WAC FAILURE CLASSIFICATION: FROZEN`

`RELEASE 1.10 WAC REPOSITORY INNOCENCE: PASS — ENVIRONMENT-ONLY`

`RELEASE 1.10 WAC SECURITY EVIDENCE: CAPTURED`

`RELEASE 1.10 WAC BOUNDED RECOVERY EXPERIMENTS: COMPLETE`

`RELEASE 1.10 WAC → TERRA RECOVERY CONTRACT: FROZEN`

`RELEASE 1.10 WAC POST-MERGE ACCEPTANCE SEMANTICS: FROZEN`

`RELEASE 1.10 WAC RELEASE-LIFECYCLE BOUNDARY: PRESERVED`

`RELEASE 1.10 WAC RECONCILIATION MUTATION AUDIT: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 — POST-MERGE WINDOWS APP CONTROL VALIDATION-GATE RECONCILIATION AUTHORITY COMPLETE`

---

# Block conditions

BLOCK if:

- repository/product causality cannot be excluded;
- diagnosis requires product/test/config edits;
- safe environment-only recovery cannot be established;
- recovery would require disabling/weakening App Control or WDAC;
- recovery requires permanent machine security-policy mutation;
- exact Terra execution contract cannot be frozen;
- evidence is insufficient to distinguish environment gate from product regression.

On BLOCK:

- do not broaden scope;
- preserve repository state;
- do not mutate Git/GitHub lifecycle;
- report exact evidence;
- identify the minimum next Luna authority.

# Exact blocked terminal

`RELEASE 1.10 — POST-MERGE WINDOWS APP CONTROL VALIDATION-GATE RECONCILIATION AUTHORITY BLOCKED`
