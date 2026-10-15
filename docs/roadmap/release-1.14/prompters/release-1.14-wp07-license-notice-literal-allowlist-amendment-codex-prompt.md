# Release 1.14 WP07 — License-Notice Literal Allowlist Amendment

## Authority classification

**Narrow amendment to the previously approved Administrative REST API Narrow Offline Implementation Authority and conditional UID/GID amendment.** This document authorizes exactly three additional notice artifact paths and the minimum packaging/verification changes **within paths already authorized by the original implementation authority**. It does not grant a new phase or broaden the feature scope.

## Verified baseline and reason for STOP

- Branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Reported HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- The previous Codex run reported no repository changes and stopped at the license-notice allowlist gate.
- One isolated restore with SDK `10.0.103` resolved `Yarp.ReverseProxy` `2.3.0` (`net8.0`) and `System.IO.Hashing` `8.0.0` (`net8.0`), with no additional package dependencies; NuGet audit reported no warnings. This is not a comprehensive security clearance.
- Artifact review identified YARP MIT licensing and System.IO.Hashing license and third-party notices. The reviewed package lacked bundled YARP license text, requiring an authoritative upstream copy and provenance check.

Before doing any work, confirm branch, HEAD, current status and applicability of the previous authority. Preserve all pre-existing tracked modifications and untracked files. If baseline differs, STOP and report; do not reset, clean, stash, checkout or overwrite.

## Exact additional authorized paths

The following **three paths only** are added to the prior literal mutation allowlist:

```text
src/AIQuantTradingResearch.AdminDiagnostics/Notices/YARP-LICENSE.txt
src/AIQuantTradingResearch.AdminDiagnostics/Notices/SYSTEM-IO-HASHING-LICENSE.txt
src/AIQuantTradingResearch.AdminDiagnostics/Notices/SYSTEM-IO-HASHING-THIRD-PARTY-NOTICES.txt
```

No other new path is authorized by this amendment. Existing authorized `Dockerfile`, administrative project file and offline test paths may be changed **only to the extent already permitted** and only as needed to package and verify these exact notices. If a lockfile, extra notice, generated checked-in artifact, or any other new path is necessary, STOP and request a further literal-path amendment.

## Mandatory notice provenance and integrity

1. Obtain the authentic YARP MIT license text corresponding to the reviewed `v2.3.0` release from the authoritative versioned upstream repository. Record the source URL/revision and, where practical, a checksum in the implementation report; do not substitute an invented or paraphrased license.
2. Extract the actual license and third-party notice text from the verified `System.IO.Hashing` `8.0.0` package artifacts. Preserve notices completely, including attribution and license wording. Do not infer the text from general MIT conventions.
3. Do not alter copyright, attribution, disclaimer, third-party license text or legal terms. Only necessary line-ending normalization is acceptable; report any transformation.
4. Ensure the three notice files are included in the **published/distributed administrative application artifact**, not merely the source tree. Use existing authorized packaging paths and ensure build/publish verification detects missing or empty files.
5. Record package versions, notice origins, resolved package graph and audit outcome. Report limitations: a clean NuGet audit does not prove absence of vulnerabilities.
6. If authoritative texts cannot be obtained or if package contents contradict the prior report, STOP before fabricating notices or proceeding with implementation.

## Resumption of previously authorized work

Once the three notices and packaging disposition are established, resume **only** the previously approved narrow offline implementation and synthetic verification under the original authority, as amended for conditional administrative UID/GID `1100:1100`.

The identity is a **conditional candidate**, not a verified collision-free account. Implement mandatory build-time UID/GID collision rejection and startup identity checks; preserve existing presentation identity and public-runtime reservations. Do not claim runtime isolation from source review or offline tests.

Maintain all earlier architectural and security constraints: disabled-by-default administrative API, Azure App Service setting contract, bounded anonymous-pipe credential handoff, separate administrative/presentation identities, trusted immutable code, Model A lifecycle candidate, exact passive endpoint, authentication before collection, bounded response and reads, no private SQLite/Worker dependencies, and preserved disabled-mode Streamlit behavior. If any requirement conflicts with the existing authority, STOP for owner review.

## Prohibited activities

- No Azure access, configuration change, credential provisioning, live diagnostic call or deployment.
- No local container build or execution; no image identity qualification.
- No database/SQLite access, Worker launch, private acquisition or public Worker gate changes.
- No GitHub mutations, branch changes, commits or PRs unless separately authorized.
- No new packages, package upgrades, license modifications, new paths or broadened tests beyond the existing approved envelope.
- No claim that `PUBLIC_RUNTIME_VERIFIED` is true or that public Worker activation is permitted.

## Required execution order and STOP conditions

1. Confirm unchanged authorized baseline and inventory existing work without mutating unrelated files.
2. Revalidate exact package versions and obtain authentic notices; STOP on provenance or licensing ambiguity.
3. Add exactly the three authorized notice files and ensure distribution packaging within previously authorized files.
4. Verify notice inclusion and integrity through bounded offline inspection/tests.
5. Continue original offline implementation and synthetic tests, subject to every prior STOP condition and the conditional UID/GID checks.
6. Stop immediately on any dependency graph expansion, unapproved path requirement, UID/GID collision, secret exposure, unsafe lifecycle behavior, private-resource access or public gate modification.

## Required final Codex report

Report: baseline branch/HEAD and before/after `git status`; exact changed/new paths; authentic notice provenance and checksums; distribution packaging verification; package versions and dependency/audit results; implementation/test results with commands and durations; UID/GID collision-check status; unqualified runtime properties; any deviations/STOP; and a clear **GO/NO-GO recommendation for requesting separate local-container qualification authority**.

**This amendment grants only the three notice paths and their bounded packaging disposition; it does not itself authorize local-container or Azure qualification.**

`PUBLIC_RUNTIME_VERIFIED=False`. **Public Worker activation remains NO-GO.**
