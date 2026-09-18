# GPT-5.6 Terra — Release 1.12 WP04 Exact-Commit GHCR Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: build and publish the exact validated commit to GHCR and prove immutable public readability.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Authoritative candidate source commit:

`2add79d2063292687d9813f9022206e84b664627`

Authorized branch:

`release/1.12-wp04-persistent-sqlite`

Remote tip has already been proven to equal exactly:

`2add79d2063292687d9813f9022206e84b664627`

Accepted markers:

`RELEASE 1.12 WP04 — FINAL RETRIEVAL REMEDIATION PUSH: PASS`

`RELEASE 1.12 WP04 — EXACT COMMIT PUSH MUTATION AUDIT: PASS`

## 2. Exact publication identity

Build and publish a container image derived from exactly:

`2add79d2063292687d9813f9022206e84b664627`

Use candidate tag:

```text
wp04-2add79d2063292687d9813f9022206e84b664627
```

Target repository:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch
```

The authoritative publication identity after push is the resulting immutable digest, not the mutable/tag reference.

## 3. Mandatory pre-publication checks

Before Docker/GHCR mutation, verify:

- local `HEAD` = `2add79d2063292687d9813f9022206e84b664627`;
- `origin/release/1.12-wp04-persistent-sqlite` = same commit;
- worktree/index state does not introduce uncommitted build context changes affecting the image;
- no unrelated tracked path is modified;
- existing Dockerfile/entrypoint are unchanged from the validated candidate;
- repository signing correction remains preserved;
- no registry username/password will be configured in Azure by this authority.

If image build context could include unrelated local files that materially alter the image, STOP.

Required:

`RELEASE 1.12 WP04 — GHCR EXACT-COMMIT PRECHECK: PASS`

## 4. Build authority

Build the container from the exact candidate commit using the existing repository container contract.

Do not modify:

- Dockerfile;
- entrypoint;
- application code;
- package/dependency files;
- schema;
- build configuration;
- README.

One or more local Docker build/cache mutations are permitted only as necessary to produce the candidate image.

Capture the locally built image identity before push if available.

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE BUILD: PASS`

## 5. GHCR publication authority

Publish exactly the candidate tag:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp04-2add79d2063292687d9813f9022206e84b664627
```

No release tag, `latest`, branch tag, or additional alias is authorized.

After publication, capture the immutable digest.

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE PUBLICATION: PASS`

Record:

```text
WP04_FINAL_CANDIDATE_COMMIT=2add79d2063292687d9813f9022206e84b664627
WP04_FINAL_CANDIDATE_TAG=wp04-2add79d2063292687d9813f9022206e84b664627
WP04_FINAL_CANDIDATE_DIGEST=<sha256:...>
```

## 6. Anonymous manifest-read gate

Prove the published manifest is anonymously readable from GHCR.

Requirements:

- use the exact candidate tag and/or immutable digest;
- no authenticated credential may be necessary for the acceptance read;
- record the digest returned by the anonymous-read path;
- digest must match the published candidate digest exactly.

If anonymous access fails, STOP.

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE ANONYMOUS READ: PASS`

## 7. Source-to-image provenance

Prove:

```text
candidate source commit
    =
2add79d2063292687d9813f9022206e84b664627

published candidate tag
    =
wp04-2add79d2063292687d9813f9022206e84b664627

anonymous manifest digest
    =
published immutable digest
```

Required:

`RELEASE 1.12 WP04 — FINAL CANDIDATE SOURCE-IMAGE PROVENANCE: PASS`

## 8. Strict publication boundary

This authority permits only:

- local Docker build operations;
- one GHCR publication for the exact candidate tag;
- read-only verification of published manifest/digest.

This authority does **not** authorize:

- Azure image-reference update;
- Azure app-setting mutation;
- D3 qualification settings;
- Azure restart;
- Azure redeployment;
- PR creation;
- merge;
- issue closure;
- Project #2 status mutation;
- milestone closure;
- Git commit/push/tag mutation;
- repository file changes.

A separate exact-digest Azure deployment/D3 qualification authority is required after this publication succeeds.

## 9. Mutation accounting

Report exact actual mutations.

Potential authorized mutations:

```text
Local Docker build/cache mutations: exact count or concise description
GHCR candidate image publications: 1
```

Expected zero:

```text
Repository file mutations: 0
Git staging mutations: 0
Git commit mutations: 0
Git branch pushes: 0
Git tag mutations: 0
Azure mutations: 0
Provider mutations: 0
GitHub PR mutations: 0
Issue closures: 0
Project #2 status mutations: 0
Milestone mutations: 0
```

Required:

`RELEASE 1.12 WP04 — GHCR PUBLICATION MUTATION AUDIT: PASS`

## 10. Stop conditions

STOP if:

- candidate source does not equal the exact authorized commit;
- remote branch tip has changed;
- build context contains ungoverned tracked changes;
- additional tags would be required;
- Dockerfile/entrypoint would need modification;
- publication would require changing repository code;
- anonymous GHCR manifest read fails;
- published digest cannot be established unambiguously.

Do not self-authorize Azure deployment.

## 11. Return evidence

Return:

- exact build command or build mechanism;
- exact pushed image reference;
- immutable digest;
- anonymous manifest-read result;
- source-to-image provenance result;
- exact mutation accounting.

Do not return registry credentials or tokens.

## 12. Terminal markers

On success:

`RELEASE 1.12 WP04 — GHCR EXACT-COMMIT PRECHECK: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE BUILD: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE IMAGE ANONYMOUS READ: PASS`

`RELEASE 1.12 WP04 — FINAL CANDIDATE SOURCE-IMAGE PROVENANCE: PASS`

`RELEASE 1.12 WP04 — GHCR PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — EXACT-DIGEST AZURE QUALIFICATION AUTHORITY REQUIRED: YES`

`RELEASE 1.12 WP04 — TERRA EXACT-COMMIT GHCR PUBLICATION COMPLETE`

If blocked:

`RELEASE 1.12 WP04 — EXACT-COMMIT GHCR PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`
