# GPT-5.6 Terra --- Release 1.12 WP04 Local Signing Recovery & Validation Resume Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, reconciliation,
    acceptance criteria, governance, read-only planning.
-   **GPT-5.6 Terra** --- PRIMARY for this prompt: local environment
    recovery, validation execution, approved operator handoff, exact
    mutation accounting.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

------------------------------------------------------------------------

## 1. Governed starting state

Resume:

**Phase 4 --- Release 1.12 WP04: Persistent SQLite Initialization, Data
Update & Recovery**

Issue:

`#263`

Canonical base:

`40a9a236dae789864f35e64bb5c1afd358e7b3db`

Current branch:

`release/1.12-wp04-persistent-sqlite`

The E2-A implementation is present locally and remains constrained to
the already-authorized eleven-path set.

Current blocker:

``` text
SignTool error: No certificates were found that met all the given criteria.
Directory.Build.targets(21,5): error MSB3073
WP04_BUILD_EXIT_CODE=1
```

This failure occurred before tests and is currently classified as a
**local signing-environment prerequisite failure**, not a WP04
compile/test failure.

Current markers:

`RELEASE 1.12 WP04 — ELEVEN-PATH ALLOWLIST VERIFIED: PASS`

`RELEASE 1.12 WP04 — NON-ROOT REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

No Docker, Azure, GHCR, provider, GitHub, staging, commit, push, PR,
issue, Project, milestone, or lifecycle mutation occurred.

Unrelated untracked `prompters/` files remain untouched.

------------------------------------------------------------------------

## 2. Authority objective

Restore only the local Authenticode signing prerequisite required by the
repository's existing `Directory.Build.targets`, then rerun the WP04
local validation from the build gate.

This authority is **not** a code-governance amendment.

Do not change the WP04 eleven-path allowlist.

Do not weaken, bypass, disable, or remove the repository signing
contract merely to make the build pass.

------------------------------------------------------------------------

## 3. Mandatory read-only reconciliation first

Before any local environment mutation:

1.  inspect `Directory.Build.targets`;
2.  identify the exact signing command and certificate-selection
    criteria;
3.  inspect any repository documentation/scripts governing local
    development signing;
4.  inspect prior project-local evidence for the expected development
    certificate behavior;
5.  determine whether the required certificate is:
    -   missing,
    -   expired,
    -   installed in the wrong store,
    -   missing private key,
    -   mismatched by subject/thumbprint criteria,
    -   inaccessible to the current user,
    -   or blocked by another local signing prerequisite.

Do not infer.

Required markers:

`RELEASE 1.12 WP04 — SIGNING CONTRACT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — SIGNING FAILURE CLASSIFICATION: <MISSING_CERT|EXPIRED_CERT|WRONG_STORE|NO_PRIVATE_KEY|CRITERIA_MISMATCH|ACCESS_FAILURE|OTHER>`

`RELEASE 1.12 WP04 — REPOSITORY SIGNING CONTRACT CHANGE REQUIRED: NO`

If repository evidence proves a code/config change is actually required,
STOP for Luna re-governance.

------------------------------------------------------------------------

## 4. Local signing recovery rules

Use the smallest local-machine-only recovery that restores the existing
repository signing contract.

Allowed local environment actions may include, only if supported by
repository evidence:

-   generate/import the expected development Authenticode signing
    certificate;
-   place it in the correct current-user certificate store;
-   ensure a usable private key exists;
-   verify subject/thumbprint/EKU/store criteria;
-   verify `signtool` can locate it;
-   verify the current user can access the private key.

Do not:

-   commit certificates;
-   add certificate files to the repository;
-   expose private keys;
-   weaken certificate selection;
-   change `Directory.Build.targets`;
-   disable signing;
-   add `/p:SignAssembly=false` or equivalent bypasses;
-   disable security controls merely to satisfy the build;
-   export private key material into project paths;
-   alter unrelated trust stores.

If interactive Windows-user context is required, provide exact
PowerShell commands for user `sabsf`, then STOP and wait for
stdout/stderr and exit codes.

------------------------------------------------------------------------

## 5. Secret and certificate hygiene

Certificate evidence must not expose private key material.

Safe evidence may include:

-   certificate subject;
-   thumbprint;
-   validity dates;
-   store location;
-   `HasPrivateKey`;
-   intended EKU;
-   `signtool` success/failure output that contains no secret material.

Required:

`RELEASE 1.12 WP04 — PRIVATE KEY DISCLOSURE: ABSENT`

`RELEASE 1.12 WP04 — CERTIFICATE REPOSITORY MUTATION: ABSENT`

------------------------------------------------------------------------

## 6. Build-gate resumption

After signing recovery succeeds, rerun from the build gate.

At minimum:

1.  `dotnet build`
2.  Domain tests
3.  Application tests
4.  Architecture tests
5.  Infrastructure tests
6.  preserved unavailable-path regression test
7.  targeted SQLite persistence tests
8.  diagnostics tests
9.  PowerShell validation for both WP04 scripts
10. shell syntax validation for `container/entrypoint.sh`
11. Gitleaks across the eleven-path payload
12. `git diff --check`
13. exact eleven-path payload audit

Report fresh exact counts only.

Required:

`RELEASE 1.12 WP04 — BUILD VALIDATION: PASS`

`RELEASE 1.12 WP04 — DOMAIN TESTS: PASS`

`RELEASE 1.12 WP04 — APPLICATION TESTS: PASS`

`RELEASE 1.12 WP04 — ARCHITECTURE TESTS: PASS`

`RELEASE 1.12 WP04 — INFRASTRUCTURE TESTS: PASS`

`RELEASE 1.12 WP04 — REGRESSION TEST: PASS`

`RELEASE 1.12 WP04 — SQLITE TARGETED TESTS: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTICS TESTS: PASS`

`RELEASE 1.12 WP04 — POWERSHELL VALIDATION: PASS`

`RELEASE 1.12 WP04 — ENTRYPOINT SHELL VALIDATION: PASS`

`RELEASE 1.12 WP04 — GITLEAKS: PASS`

`RELEASE 1.12 WP04 — DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — ELEVEN-PATH PAYLOAD AUDIT: PASS`

If any gate fails after signing recovery, stop at that failure and
report it distinctly from the signing prerequisite.

------------------------------------------------------------------------

## 7. Docker boundary

Do **not** execute Docker under this authority unless and until the full
local validation stack passes.

If all local gates pass, return control to the existing E2-A
Docker-remediation authority and report:

`RELEASE 1.12 WP04 — LOCAL VALIDATION RESTORED: PASS`

`RELEASE 1.12 WP04 — RETURN TO NON-ROOT DOCKER REMEDIATION: READY`

Do not create a new Docker design.

Do not alter the already-governed Docker acceptance semantics.

------------------------------------------------------------------------

## 8. Repository/GitHub/cloud boundary

This authority does not authorize:

-   repository file edits;
-   staging;
-   commit;
-   push;
-   PR creation/merge;
-   Docker execution before full local validation;
-   Azure;
-   GHCR;
-   providers;
-   issue closure;
-   Project #2 mutation;
-   milestone mutation;
-   WP05 execution.

The only permitted mutation class is the narrowly required **local
Windows signing environment** repair.

Required:

`RELEASE 1.12 WP04 — REPOSITORY MUTATION DURING SIGNING RECOVERY: 0`

`RELEASE 1.12 WP04 — CLOUD/GITHUB MUTATION DURING SIGNING RECOVERY: 0`

------------------------------------------------------------------------

## 9. Mutation accounting

Report exact local-machine mutations, for example:

-   certificate created/imported: `<count>`
-   certificate removed/replaced: `<count>`
-   certificate store modified: `<YES|NO>`
-   repository files changed: `0`
-   staged paths: `0`
-   commits: `0`
-   pushes: `0`
-   PRs: `0`
-   Docker mutations: `0` until local validation fully passes
-   Azure/GHCR/provider/GitHub lifecycle mutations: `0`

Required:

`RELEASE 1.12 WP04 — SIGNING RECOVERY MUTATION AUDIT: PASS`

------------------------------------------------------------------------

## 10. Required output

If signing recovery succeeds and all local gates pass:

`RELEASE 1.12 WP04 — LOCAL SIGNING RECOVERY: PASS`

`RELEASE 1.12 WP04 — FULL LOCAL VALIDATION: PASS`

`RELEASE 1.12 WP04 — RETURN TO NON-ROOT DOCKER REMEDIATION: READY`

If signing recovery is blocked:

`RELEASE 1.12 WP04 — LOCAL SIGNING RECOVERY: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

If signing succeeds but another validation gate fails:

`RELEASE 1.12 WP04 — LOCAL SIGNING RECOVERY: PASS`

`RELEASE 1.12 WP04 — FULL LOCAL VALIDATION: FAIL`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

Terminal:

`RELEASE 1.12 WP04 — TERRA LOCAL SIGNING RECOVERY AUTHORITY COMPLETE`
