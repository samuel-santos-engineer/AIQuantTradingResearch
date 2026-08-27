# Release 1.9 — Windows Smart App Control Local-Development Signing Documentation Authority

## Model
Use **GPT-5.6 Luna**.

## Sole authority
This is a **narrow documentation-only authority** for the AIQuantTradingResearch repository.

Its sole purpose is to document the now-working Windows local-development Authenticode signing setup used to keep Smart App Control / App Control enabled while allowing locally built AIQuantTradingResearch test binaries to load.

This authority does **not** authorize implementation changes.

No production mutation.
No test mutation.
No MSBuild behavior change.
No PowerShell behavior change.
No certificate regeneration.
No package change.
No GitHub lifecycle mutation.
No WP09.

# Accepted working implementation

Treat the following as accepted existing implementation and document it faithfully.

## Windows SDK prerequisite

Installed successfully with:

```powershell
winget install Microsoft.WindowsSDK.10.0.22621
```

Observed installed package:

`Windows Software Development Kit - Windows 10.0.22621.2428`

The SDK provides the required Windows signing tooling such as `signtool.exe`.

## Local certificate bootstrap

Existing script:

`eng/sec/create-self-signed-certificate.ps1`

Purpose:
- create/install the local development self-signed code-signing certificate used on this developer machine;
- support local Authenticode signing only;
- not a production/public-trust certificate workflow.

Do not change this script under this authority.

## MSBuild signing integration

Existing project integration:

`src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj`

The project contains an `AutoSignTestBinaries` post-build signing target/instruction.

Accepted final activation semantics:

- **opt-in**;
- **development-only**;
- **Debug-only**;
- disabled by default;
- enabled only through a local uncommitted setting;
- intended for local Windows Smart App Control/App Control compatibility.

Do not change the target under this authority.

## Local uncommitted activation

Accepted local file:

`Directory.Build.local.props`

Expected conceptual content:

```xml
<Project>
  <PropertyGroup>
    <AutoSignTestBinaries>true</AutoSignTestBinaries>
  </PropertyGroup>
</Project>
```

This file is developer-machine-specific and must remain uncommitted/ignored.

Do not commit local certificate material, passwords, exported private keys, PFX files, or other machine-specific secrets.

## Working result

The local signing setup resolved the Windows Application Control assembly-load blocker:

```text
System.IO.FileLoadException
Could not load file or assembly
AIQuantTradingResearch.Infrastructure.Tests.dll.
An Application Control policy has blocked this file.
(0x800711C7)
```

Code Integrity evidence had identified:

- Event ID 3077;
- policy:
  `VerifiedAndReputableDesktop`;
- policy ID:
  `0283ac0f-fff1-49ae-ada1-8a933130cad6`.

After local signing setup, the test assembly could load and restart-specific WP08 tests could execute.

Important:
This environment remediation did **not** resolve WP08 #233 itself. After the SAC block was removed, Worker B still reproduced `0xC0000142` in the restart-specific lifecycle scenario.

# Documentation terminology

Use:

**“local-development Authenticode signing for Windows Smart App Control compatibility”**

Do **not** describe this as:
- “Smart App Control bypass”;
- production signing;
- public trust;
- general signing solution for other machines.

Explain clearly that this is a developer-machine trust arrangement.

# Objective

Create/update documentation so future developers understand:
1. the symptom;
2. how to identify the Smart App Control/App Control block;
3. how to install the required Windows SDK;
4. how the local certificate bootstrap works;
5. how `AutoSignTestBinaries` works conceptually;
6. how to enable signing locally without committing machine-specific state;
7. how to verify signatures;
8. how to verify SAC/App Control enforcement remains active;
9. how to troubleshoot rebuild/sign ordering;
10. how this relates to WP08 without conflating environment remediation with the still-open lifecycle issue.

# Authorized documentation paths

## Primary new guide

Create exactly:

`docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`

If `docs/development` does not exist, create only that directory as needed.

## Minimal cross-reference paths

Read repository structure and identify the canonical developer/setup documentation.

Authorize **at most two** minimal cross-reference edits from among existing docs such as:
- root `README.md`;
- `docs/README.md`;
- an existing developer setup/onboarding guide;
- Release 1.9/WP08 evidence/readme documentation.

Do not invent multiple duplicate guides.

## Release 1.9 / WP08 cross-reference

If an existing Release 1.9/WP08 evidence/status document is the canonical place to record environment blockers/remediation, add one concise note/cross-reference there.

Do not rewrite WP08 implementation history.

# Forbidden documentation mutations

Do not modify:
- source files;
- tests;
- project files;
- PowerShell scripts;
- `.gitignore` unless documentation audit proves the ignored local props file is not already protected and the user explicitly wants repository hygiene fixed in a later implementation authority;
- packages;
- GitHub issues/project state.

If `.gitignore` does not currently protect `Directory.Build.local.props`, document the requirement and report the gap rather than changing it under this authority.

# Phase 0 — Read-only repository documentation inspection

Inspect:
- root README;
- docs structure;
- developer/setup docs;
- Release 1.9 roadmap/evidence docs;
- `.gitignore`;
- current `AutoSignTestBinaries` comments/usage;
- current local signing script path.

Determine exact cross-reference locations.

No mutation yet.

# Phase 1 — Primary guide structure

Create:

`docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`

Use this structure.

## Title

`# Windows Smart App Control — Local Development Signing`

## 1. Purpose

Explain:
- why local signing exists;
- Windows-only;
- development-only;
- preserves SAC/App Control enforcement;
- not production/public signing.

## 2. Symptom

Document the observed error:

```text
System.IO.FileLoadException:
Could not load file or assembly
AIQuantTradingResearch.Infrastructure.Tests.dll.
An Application Control policy has blocked this file.
(0x800711C7)
```

Document Code Integrity Event 3077 and `VerifiedAndReputableDesktop`.

## 3. Diagnose the block

Include commands:

```powershell
(CiTool.exe -lp -json | ConvertFrom-Json).Policies |
    Where-Object { $_.IsEnforced -eq "True" } |
    Select-Object PolicyID, FriendlyName, BasePolicyID, IsEnforced |
    Format-List
```

and:

```powershell
Get-WinEvent -FilterHashtable @{
    LogName   = 'Microsoft-Windows-CodeIntegrity/Operational'
    Id        = 3077
    StartTime = (Get-Date).AddMinutes(-60)
} |
Select-Object -First 10 TimeCreated, Id, Message |
Format-List
```

Document the observed policy ID:

`0283ac0f-fff1-49ae-ada1-8a933130cad6`

Do not claim all SAC systems always use only this policy.

## 4. Install Windows SDK

Document:

```powershell
winget install Microsoft.WindowsSDK.10.0.22621
```

Explain:
- supplies `signtool.exe`;
- expected installed package/version can vary;
- observed version was 10.0.22621.2428.

## 5. Create local development certificate

Reference:

`eng/sec/create-self-signed-certificate.ps1`

Document:
- one-time local setup;
- developer machine only;
- private key never committed;
- exported PFX/passwords/private material forbidden in source control.

Do not fabricate script parameters; read the actual script and document only factual invocation/behavior.

## 6. AutoSignTestBinaries

Reference:

`src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj`

Document accepted semantics:
- default false;
- Debug/development only;
- opt-in;
- AfterBuild signing;
- intended to sign locally built first-party binaries needed by tests.

Read the actual project file and list the exact artifacts/target behavior factually.

Do not assume six files unless current target actually signs those six.

If the target differs from prior intended list, document actual implementation and flag mismatch.

## 7. Enable locally

Document local, uncommitted:

`Directory.Build.local.props`

Example:

```xml
<Project>
  <PropertyGroup>
    <AutoSignTestBinaries>true</AutoSignTestBinaries>
  </PropertyGroup>
</Project>
```

Explain:
- machine-local;
- do not commit;
- remove/disable to turn signing off.

Check `.gitignore`.

If already ignored, state that fact.
If not ignored, prominently warn and report the hygiene gap.

## 8. Build/test workflow

Document actual safe workflow.

If AfterBuild signing reliably re-signs every build, show:

```powershell
dotnet build
dotnet test
```

If test rebuild can overwrite signed files before load, recommend:

```powershell
dotnet build
dotnet test --no-build
```

Choose based on actual MSBuild/test behavior observed in repository.

Do not guess.

## 9. Verify signatures

Include:

```powershell
Get-AuthenticodeSignature `
  .\tests\AIQuantTradingResearch.Infrastructure.Tests\bin\Debug\net10.0\AIQuantTradingResearch.Infrastructure.Tests.dll |
  Format-List Status,StatusMessage,SignerCertificate,TimeStamperCertificate
```

Expected local state:
- `Status = Valid`;
- signer = local development certificate.

Also include a first-party artifact scan if useful.

## 10. Verify App Control still enforced

Include `CiTool` policy query.

Explain that successful tests should not depend on disabling SAC/App Control.

## 11. Troubleshooting

Cover:
- signature returns NotSigned;
- Event 3077 remains;
- tests suddenly blocked after rebuild;
- third-party binaries.

Explain:
- build target may not be enabled;
- local props may not be loaded;
- certificate may be missing;
- signtool may be missing;
- inspect exact blocked binary;
- verify rebuild did not overwrite signed file;
- do not automatically re-sign third-party dependencies.

## 12. Security and repository hygiene

Explicitly state:
- local development only;
- no private keys/PFX/passwords in git;
- no production trust claims;
- no weakening global App Control policy required;
- no “bypass” terminology.

## 13. WP08 context

Document concise historical note:
- SAC/App Control prevented loading `AIQuantTradingResearch.Infrastructure.Tests.dll`;
- local signing resolved that environment-level blocker;
- after the assembly could load, restart-specific WP08 validation continued;
- Worker B still reproduced `0xC0000142`;
- therefore SAC remediation and WP08 lifecycle defect are separate concerns;
- #233 remained Open / Backlog at that point.

Do not claim current #233 state unless repository/GitHub evidence available under this documentation pass proves it.

# Phase 2 — Cross-reference edits

Add one concise reference from the canonical developer setup doc:

> Windows developers with Smart App Control enabled may need local Authenticode signing for locally built test binaries. See [Windows Smart App Control — Local Development Signing](...).

Add at most one concise Release 1.9/WP08 cross-reference:

> Windows App Control assembly-load remediation is documented separately; it is an environment prerequisite and not part of WP08 lifecycle semantics.

No duplicated procedure.

# Phase 3 — Factual audit

Before finalizing documentation, verify actual repository facts:
- exact script filename/path;
- actual script behavior;
- actual AutoSignTestBinaries condition;
- actual AfterBuild target name;
- actual binaries signed;
- actual certificate lookup/signing mechanism;
- actual local props import behavior;
- `.gitignore` state;
- actual commands needed.

If documentation would contradict implementation, document actual implementation and call out the discrepancy.

Do not “correct” implementation under this authority.

# Phase 4 — Security audit

Ensure docs do not expose:
- private certificate keys;
- passwords;
- certificate export secrets;
- sensitive thumbprints if repository policy treats them as private;
- personal machine identifiers.

A certificate subject/friendly-name pattern may be documented if already non-sensitive and needed.

# Phase 5 — Scope audit

Repository mutations must be documentation-only.

Expected:
- one new developer guide;
- at most two small existing-doc cross-references.

No:
- `.csproj`;
- `.ps1`;
- `.cs`;
- `.py`;
- `.gitignore`;
- GitHub mutation.

# Required completion report

## Primary guide
Path and sections created.

## Cross-references
Exact existing docs changed.

## Factual implementation mapping
- SDK;
- certificate script;
- AutoSignTestBinaries;
- local props;
- signed outputs;
- build/test ordering.

## Security/repository hygiene
What is local/uncommitted and what must never be committed.

## WP08 separation
Explicit statement that App Control remediation did not itself resolve #233.

## Gaps
Any discovered mismatch, especially:
- `Directory.Build.local.props` not ignored;
- signing target not actually default-off/Debug-only;
- documentation assumptions inconsistent with implementation.

Do not fix gaps under this authority.

## Mutation statement

`WINDOWS SMART APP CONTROL LOCAL-SIGNING DOCUMENTATION MUTATIONS: ZERO production/test/GitHub mutations`

# Terminal markers

Success:

`WINDOWS SMART APP CONTROL LOCAL-DEVELOPMENT SIGNING DOCUMENTATION COMPLETE`

Blocked:

`WINDOWS SMART APP CONTROL LOCAL-DEVELOPMENT SIGNING DOCUMENTATION BLOCKED`

Do not emit COMPLETE unless the documentation reflects the actual working local setup, security boundary, opt-in behavior, verification steps, and WP08/environment separation without changing implementation.
