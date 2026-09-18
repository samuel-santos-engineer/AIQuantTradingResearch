# GPT-5.6 Terra --- Release 1.12 WP04 Docker Qualification Helper Correction & Clean-Recreate Rerun Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, reconciliation,
    acceptance criteria, governance, read-only planning.
-   **GPT-5.6 Terra** --- PRIMARY: validation execution, interactive
    Docker handoff correction, and exact mutation accounting.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

## 1. Current state

Resume **Phase 4 --- Release 1.12 WP04: Persistent SQLite
Initialization, Data Update & Recovery**.

Issue: `#263`

Canonical base: `40a9a236dae789864f35e64bb5c1afd358e7b3db`

The E2-A + D3 implementation remains within the already-authorized
eleven-path repository set.

The latest Docker qualification did **not** reach the fresh-image phase.

Confirmed successful cleanup:

``` text
WP04_PRE_REMEDIATION_IMAGE_CLEANED=True
WP04_PRE_REMEDIATION_VOLUME_CLEANED=True
```

The operator script then failed in its helper/finally path because this
expected missing-container probe:

``` powershell
& docker container inspect $Name *> $null
```

was promoted to a terminating PowerShell error.

Missing container:

``` text
aiq-r112-wp04-initialize-b09109943214425b909e583b15f3d4e6
```

This is a **qualification-helper defect only**. It is not evidence of
Docker semantic success or failure.

No fresh qualification image/volume was proven created, and no
persistence result from that attempt is valid.

## 2. Repository boundary

Do **not** modify any of the eleven governed WP04 repository paths for
this helper defect.

Required:

`RELEASE 1.12 WP04 — REPOSITORY MUTATION FOR HELPER DEFECT: ABSENT`

## 3. Helper correction

Correct all Docker existence probes so an expected "does not exist"
result does not terminate the script.

Preferred bounded pattern:

``` powershell
function Test-DockerContainerExists {
    param(
        [Parameter(Mandatory)]
        [string]$Name
    )

    $previousErrorActionPreference = $ErrorActionPreference
    try {
        $ErrorActionPreference = 'Continue'
        & docker container inspect $Name 2>$null 1>$null
        return ($LASTEXITCODE -eq 0)
    }
    finally {
        $ErrorActionPreference = $previousErrorActionPreference
    }
}
```

Use equivalent bounded helpers for images and volumes where necessary.

Requirements:

-   suppress only expected native stderr from existence probes;
-   preserve failure detection for actual Docker build/run/remove
    commands;
-   restore the prior `$ErrorActionPreference`;
-   use `$LASTEXITCODE` from the Docker CLI probe;
-   do not globally weaken error handling;
-   do not reinterpret failed Docker mutations as success.

Required:

`RELEASE 1.12 WP04 — DOCKER INSPECT HELPER CORRECTED: PASS`

`RELEASE 1.12 WP04 — QUALIFICATION ERROR HANDLING PRESERVED: PASS`

## 4. Clean-recreate starting condition

The previous pre-remediation image and volume were already successfully
removed.

Before fresh qualification:

-   confirm prior failed image absent;
-   confirm prior failed volume absent;
-   confirm no stale initialize/reopen container from the aborted helper
    attempt exists;
-   remove only a stale WP04 qualification container if actually
    present;
-   do not touch unrelated Docker resources.

Required:

`RELEASE 1.12 WP04 — PRE-REMEDIATION CLEANUP STATE RECONCILED: PASS`

## 5. Repeat fresh qualification from zero

After helper correction, rerun the complete fresh clean-recreate Docker
qualification with new unique identifiers.

Create:

-   one fresh image;
-   one fresh named persistence volume;
-   one initialize container;
-   one reopen container.

Do not reuse the aborted run identifiers as qualification evidence.

### Initialize phase

Prove:

-   fresh image build succeeds;
-   fresh named volume is created;
-   startup ownership preparation executes;
-   configured persistence parent becomes `aiq:aiq`;
-   configured persistence parent mode is `0750`;
-   application/qualification process executes as uid/gid 1000;
-   application-owned SQLite initialization succeeds;
-   versioned D3 `initialize` record succeeds;
-   schema version = 4;
-   journal mode = `delete`;
-   integrity = healthy/ok;
-   quick-check = healthy/ok;
-   deterministic accepted evidence identity/count is reported.

### Reopen phase

Remove the initialize container while retaining the named volume.

Run the reopen container against the same volume and prove:

-   runtime remains non-root;
-   storage ownership/mode remains valid;
-   governed persistence reopens;
-   previously accepted evidence is retrieved through application-owned
    abstractions;
-   identity/count or governed continuity proof is preserved;
-   schema version remains 4;
-   journal mode remains `delete`;
-   integrity and quick-check remain healthy;
-   persistence continuity passes.

No direct shell/Python SQLite inspection is permitted.

## 6. Final cleanup

Remove:

-   initialize container;
-   reopen container;
-   fresh named volume;
-   fresh image.

Expected final markers:

``` text
WP04_LOCAL_PERSISTENCE_RECREATE_PASS=True
WP04_LOCAL_NONROOT_RUNTIME_PASS=True
WP04_LOCAL_STORAGE_OWNER_PASS=True
WP04_LOCAL_STORAGE_MODE_PASS=True
WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=False
WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=False
WP04_LOCAL_CLEANUP_COMPLETE=True
```

Existence checks during cleanup must use corrected non-terminating probe
behavior.

## 7. Mutation accounting

Previously established cleanup:

-   pre-remediation image removed: 1
-   pre-remediation volume removed: 1

For this rerun report:

-   fresh image created: `<0|1>`
-   fresh volume created: `<0|1>`
-   initialize container created: `<0|1>`
-   initialize container removed: `<0|1>`
-   reopen container created: `<0|1>`
-   reopen container removed: `<0|1>`
-   fresh volume removed: `<0|1>`
-   fresh image removed: `<0|1>`

Repository mutations: `0`

Azure mutations: `0`

GHCR mutations: `0`

Provider mutations: `0`

GitHub/lifecycle mutations: `0`

Required:

`RELEASE 1.12 WP04 — DOCKER QUALIFICATION MUTATION AUDIT: PASS`

## 8. Stop boundary

This authority does not authorize:

-   repository edits;
-   staging;
-   commit;
-   push;
-   PR creation/merge;
-   Azure;
-   GHCR;
-   provider access;
-   issue closure;
-   Project #2 mutation;
-   milestone mutation;
-   WP05 execution.

After rerun, return full qualification stdout/stderr and exit codes.

If helper correction works but any semantic Docker gate fails:

`RELEASE 1.12 WP04 — DOCKER QUALIFICATION: FAIL`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

If all Docker gates pass:

`RELEASE 1.12 WP04 — DOCKER QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — PERSISTENCE RECREATE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — NEXT CONTINUATION AUTHORITY: READY`

Terminal:

`RELEASE 1.12 WP04 — TERRA DOCKER QUALIFICATION RERUN AUTHORITY COMPLETE`
