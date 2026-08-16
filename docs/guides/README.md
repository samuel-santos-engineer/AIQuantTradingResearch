# AIQuantTradingResearch Guides

Practical, executable guides for running AIQuantTradingResearch and independently verifying the platform capabilities that exist today.

## Start Here

If this is your first time running the project, begin with:

**[LOCAL_PLATFORM_EXECUTION.md](./LOCAL_PLATFORM_EXECUTION.md)**

This is the AIQuantTradingResearch **Hello World** guide.

Its goal is simple:

```text
Clone
  ↓
Verify
  ↓
Configure
  ↓
Run
  ↓
Real market data processed
  ↓
SUCCESS
```

The guide takes the shortest path from the repository to a real provider-backed local execution.

After that succeeds, continue through the platform verification guides to understand and prove the behavior in greater depth.

---

## Guide Hierarchy

The guides are organized into two levels.

### Level 1 — Getting Started

[LOCAL_PLATFORM_EXECUTION.md](./LOCAL_PLATFORM_EXECUTION.md)

Answers:

> Can I run AIQuantTradingResearch locally and see a real result?

This is the entry point for developers, reviewers, recruiters, and contributors who want to see the platform working before exploring its internals.

### Level 2 — Platform Verification

The remaining guides each prove one independently observable platform property:

1. [REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md](./REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md)
2. [DURABLE_PERSISTENCE_SQLITE.md](./DURABLE_PERSISTENCE_SQLITE.md)
3. [IDEMPOTENCY_PROOF.md](./IDEMPOTENCY_PROOF.md)
4. [DATA_INTEGRITY_PROOF.md](./DATA_INTEGRITY_PROOF.md)
5. [RESTART_RECOVERY_PROOF.md](./RESTART_RECOVERY_PROOF.md)

Together they progressively answer:

```text
Can we acquire real data?
          ↓
Is the data durable?
          ↓
Are retries safe?
          ↓
Is the stored data consistent with the expected model?
          ↓
Can a new process recover and continue safely?
```

---

# Level 1 — Getting Started

## [LOCAL_PLATFORM_EXECUTION.md](./LOCAL_PLATFORM_EXECUTION.md) — Start Here

**Purpose:** Run the platform locally with the minimum required setup.

Target flow:

```text
Twelve Data
     ↓
Historical observations
     ↓
AIQuantTradingResearch
     ↓
SQLite
     ↓
SUCCESS
```

Use this guide when you want to answer:

> Does AIQuantTradingResearch work on my machine?

It intentionally avoids deep architecture and extensive verification.

Once this guide succeeds, the platform is ready for the deeper capability proofs below.

---

# Level 2 — Platform Verification

## 1. [REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md](./REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md)

**Question:**

> Can the platform acquire real external market data?

Proof:

```text
Twelve Data
     ↓
Historical observations acquired
```

This guide focuses on the real provider boundary, external configuration, provider execution, and successful acquisition of historical observations.

---

## 2. [DURABLE_PERSISTENCE_SQLITE.md](./DURABLE_PERSISTENCE_SQLITE.md)

**Question:**

> Do accepted observations survive beyond the process that acquired them?

Proof:

```text
Historical observations
        ↓
Persistence boundary
        ↓
SQLite
        ↓
Worker terminates
        ↓
Database reopened
        ↓
Observations still exist
```

This guide proves that accepted historical state is durable rather than process-local.

---

## 3. [IDEMPOTENCY_PROOF.md](./IDEMPOTENCY_PROOF.md)

**Question:**

> Can the same historical observations be safely processed again?

Proof:

```text
First execution
100 NewlyAccepted

Second execution
100 Idempotent

Database
100 logical observations
0 duplicate history
```

The exact count depends on the currently configured acquisition size.

The important invariant is:

```text
Equivalent replay
        ↓
Same durable final state
```

This guide proves that retries do not create duplicate logical history.

---

## 4. [DATA_INTEGRITY_PROOF.md](./DATA_INTEGRITY_PROOF.md)

**Question:**

> Does persisted history preserve the observation information expected by the platform?

Proof:

```text
Provider-backed acquisition
        ↓
Normalized observations
        ↓
SQLite persistence
        ↓
Expected target
Expected count
Unique logical identities
Required timestamps
Required prices
0 duplicate history
        ↓
DATA INTEGRITY PROVEN
```

This guide verifies the semantic integrity of the current normalized and persisted observation model.

---

## 5. [RESTART_RECOVERY_PROOF.md](./RESTART_RECOVERY_PROOF.md)

**Question:**

> Can a new application process safely continue from existing durable state?

Proof:

```text
Acquire + persist
       ↓
Worker terminates
       ↓
SQLite retains state
       ↓
New Worker process starts
       ↓
Same database reopened
       ↓
Existing history recognized
       ↓
Equivalent history → Idempotent
New history        → NewlyAccepted
       ↓
0 duplicate history
       ↓
RESTART RECOVERY PROVEN
```

This guide verifies the boundary between process lifetime and data lifetime.

---

# Verification Journey

The complete executable journey is:

```text
┌───────────────────────────────────────────┐
│ LOCAL_PLATFORM_EXECUTION.md               │
│                                           │
│ "Can I run it?"                           │
└─────────────────────┬─────────────────────┘
                      │
                      ▼
┌───────────────────────────────────────────┐
│ REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md  │
│                                           │
│ "Can it acquire real data?"               │
└─────────────────────┬─────────────────────┘
                      │
                      ▼
┌───────────────────────────────────────────┐
│ DURABLE_PERSISTENCE_SQLITE.md             │
│                                           │
│ "Does the data survive?"                  │
└─────────────────────┬─────────────────────┘
                      │
                      ▼
┌───────────────────────────────────────────┐
│ IDEMPOTENCY_PROOF.md                      │
│                                           │
│ "Are retries safe?"                       │
└─────────────────────┬─────────────────────┘
                      │
                      ▼
┌───────────────────────────────────────────┐
│ DATA_INTEGRITY_PROOF.md                   │
│                                           │
│ "Is the stored data consistent?"          │
└─────────────────────┬─────────────────────┘
                      │
                      ▼
┌───────────────────────────────────────────┐
│ RESTART_RECOVERY_PROOF.md                 │
│                                           │
│ "Can it recover and continue?"            │
└───────────────────────────────────────────┘
```

In compact form:

```text
RUNNABLE
   ↓
REAL DATA
   ↓
DURABLE DATA
   ↓
RETRY-SAFE DATA
   ↓
VERIFIED DATA
   ↓
RECOVERABLE DATA PLATFORM
```

---

## Current Guide Catalog

| Order | Guide | Role | Primary Outcome |
| --- | --- | --- | --- |
| Start | [LOCAL_PLATFORM_EXECUTION.md](./LOCAL_PLATFORM_EXECUTION.md) | Getting started | Run the platform locally and see a real result. |
| 1 | [REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md](./REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md) | Verification | Acquire real historical observations from Twelve Data. |
| 2 | [DURABLE_PERSISTENCE_SQLITE.md](./DURABLE_PERSISTENCE_SQLITE.md) | Verification | Prove accepted observations survive process termination. |
| 3 | [IDEMPOTENCY_PROOF.md](./IDEMPOTENCY_PROOF.md) | Verification | Prove equivalent replay does not create duplicate history. |
| 4 | [DATA_INTEGRITY_PROOF.md](./DATA_INTEGRITY_PROOF.md) | Verification | Verify expected observation identity and values in durable storage. |
| 5 | [RESTART_RECOVERY_PROOF.md](./RESTART_RECOVERY_PROOF.md) | Verification | Prove a new process can safely reuse existing durable state. |

---

## Why These Guides Exist

The repository contains architecture, design, engineering, implementation, and roadmap documentation.

Those documents answer questions such as:

> How is the platform designed?

> Why was this boundary chosen?

> What are the architectural rules?

> What should the platform eventually support?

The guides answer a different question:

> What can the implemented platform actually do right now, and how can I prove it?

That distinction is intentional.

```text
Architecture documentation
        ↓
Explains the system

Executable implementation
        ↓
Provides the capability

Operational guide
        ↓
Reproduces the capability

Independent verification
        ↓
Provides evidence
```

---

## Guide Principle

The governing principle for this directory is:

> **One verification guide = one independently demonstrable platform property.**

A new guide should not exist merely because a topic could be documented.

A verification guide should correspond to a capability that can actually be executed and observed in the repository.

Examples:

```text
Real provider acquisition
        → executable today
        → guide exists

SQLite durability
        → executable today
        → guide exists

Idempotent persistence
        → executable today
        → guide exists
```

A future capability should receive a guide when there is a meaningful executable outcome to prove.

---

## Future Guide Candidates

Future guides may be added when the corresponding implementation exists.

Examples include:

```text
HISTORICAL_DATA_BACKFILL.md
DATA_QUALITY_PROOF.md
PIPELINE_EXECUTION.md
RESILIENCE_PROOF.md
OBSERVABILITY_PROOF.md
ANALYTICS_EXECUTION.md
FEATURE_ENGINEERING_EXECUTION.md
ML_EXPERIMENT_EXECUTION.md
```

These names are illustrative, not commitments.

The implementation should drive the guide catalog—not the other way around.

---

## Guide Naming Convention

Guide filenames use:

```text
UPPER_SNAKE_CASE.md
```

Examples:

```text
LOCAL_PLATFORM_EXECUTION.md
REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
DURABLE_PERSISTENCE_SQLITE.md
IDEMPOTENCY_PROOF.md
DATA_INTEGRITY_PROOF.md
RESTART_RECOVERY_PROOF.md
```

`README.md` remains the conventional directory entry point.

---

## Standard Verification Guide Structure

Detailed verification guides should normally include, when applicable:

```text
# GUIDE TITLE

## Purpose
## Target Outcome
## What This Guide Proves
## Prerequisites
## Invariants
## Configuration
## Step-by-Step Execution
## Independent Verification
## Expected Results
## Failure Modes / Troubleshooting
## Evidence Capture
## Definition of Done
## References
```

The exact structure should follow the capability being proven rather than forcing unnecessary sections.

`LOCAL_PLATFORM_EXECUTION.md` is intentionally simpler because it is the Hello World path.

---

## Evidence Model

The strongest guides separate:

```text
Application says it succeeded
```

from:

```text
The resulting state was independently verified
```

For example:

```text
Worker output
     ↓
Persistence outcome: NewlyAccepted
```

is application evidence.

While:

```text
SQLite opened independently
     ↓
Expected records queried
```

is independent persistence evidence.

Where practical, verification guides should include both.

---

## Evidence Quality

Useful evidence can include:

- terminal output;
- database queries;
- persisted records;
- generated artifacts;
- logs;
- test results;
- metrics;
- screenshots.

Evidence should be:

- reproducible;
- understandable without hidden context;
- safe to commit;
- free of credentials;
- tied to a specific platform property.

---

## Security

Never commit secrets while following these guides.

Sensitive values include:

- Twelve Data API keys;
- access tokens;
- cloud credentials;
- private certificates;
- database credentials;
- other authentication material.

Use external configuration.

Example:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Never use a real secret in committed examples.

Before committing evidence:

```powershell
git status
```

Review screenshots manually for credentials or unnecessary local-machine information.

---

## Runtime Data

Local SQLite databases are runtime artifacts.

A typical local path is:

```text
.local/data/
```

Runtime databases should not be committed unless an explicit repository decision says otherwise.

Always inspect:

```powershell
git status
```

after running the platform.

---

## Relationship to the Repository README

The root `README.md` is the project front door.

It should remain concise and point readers toward executable evidence rather than embedding every operational procedure.

The intended navigation is:

```text
Repository README
        ↓
docs/guides/README.md
        ↓
LOCAL_PLATFORM_EXECUTION.md
        ↓
Platform running
        ↓
Verification guides
```

This allows a new reader to move quickly from:

```text
"What is this project?"
```

to:

```text
"I ran it."
```

and then to:

```text
"I understand how its current capabilities are proven."
```

---

## Relationship to Architecture Documentation

Architecture and guides serve different purposes.

### Architecture

```text
How should the system work?
Why are the boundaries designed this way?
What constraints must implementations respect?
```

### Guides

```text
How do I execute this capability?
What should happen?
How do I verify it?
What evidence proves it?
```

A guide should link to architecture documentation when deeper explanation is useful, but it should not duplicate the architecture corpus.

---

## Relationship to Releases

A release introduces or evolves capabilities.

A guide demonstrates those capabilities.

```text
Release
   ↓
Implementation
   ↓
Executable capability
   ↓
Guide
   ↓
Reproducible evidence
```

Release acceptance criteria remain authoritative for release scope.

Guides remain the operational path for reproducing observable outcomes.

---

## Maintaining the Guides

When implementation changes an executable workflow, review the affected guides in the same change.

Common triggers include:

- configuration-key changes;
- Worker startup changes;
- provider changes;
- database schema changes;
- persistence semantic changes;
- project-path changes;
- command changes;
- new prerequisites;
- changed expected output;
- changed security requirements.

An executable guide that no longer reproduces its stated result should be treated as outdated documentation.

---

## Adding a New Verification Guide

Before adding one, ask:

1. Is the capability implemented?
2. Can another engineer execute it?
3. Does it prove something not already covered?
4. Is the outcome independently observable?
5. Can we define a clear PASS/FAIL condition?

If the answers are yes, the capability is a strong candidate for a guide.

The desired pattern is:

```text
Implemented capability
        ↓
One important property
        ↓
Repeatable procedure
        ↓
Observable evidence
        ↓
Definition of Done
```

---

## Current Platform Story

The guide suite currently tells a coherent engineering story:

```text
AIQuantTradingResearch can run locally.
              ↓
It can acquire real external market observations.
              ↓
It can persist those observations durably.
              ↓
It can safely recognize equivalent replay.
              ↓
Its durable observation state can be independently validated.
              ↓
A new application process can reuse that state safely.
```

Or, more simply:

```text
RUN
 ↓
ACQUIRE
 ↓
PERSIST
 ↓
RETRY
 ↓
VERIFY
 ↓
RECOVER
```

That is the purpose of `docs/guides/`.

---

## Quick Navigation

- **New to the project?** Start with [LOCAL_PLATFORM_EXECUTION.md](./LOCAL_PLATFORM_EXECUTION.md).
- **Want to understand real provider acquisition?** See [REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md](./REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md).
- **Want to prove persistence durability?** See [DURABLE_PERSISTENCE_SQLITE.md](./DURABLE_PERSISTENCE_SQLITE.md).
- **Want to prove retries are safe?** See [IDEMPOTENCY_PROOF.md](./IDEMPOTENCY_PROOF.md).
- **Want to verify durable data integrity?** See [DATA_INTEGRITY_PROOF.md](./DATA_INTEGRITY_PROOF.md).
- **Want to prove restart recovery?** See [RESTART_RECOVERY_PROOF.md](./RESTART_RECOVERY_PROOF.md).

---

## Guiding Principle

> Turn implemented platform capabilities into reproducible, independently verifiable engineering evidence.

A new reader should be able to progress from:

```text
"I found the repository."
```

to:

```text
"I ran the platform."
```

to:

```text
"I verified what it actually does."
```
