# Windows Smart App Control — Local Development Signing

## Purpose

This Windows-only guide documents local-development Authenticode signing for
Smart App Control / App Control compatibility while keeping enforcement
enabled. It is not production signing, public trust, or a general signing
solution.

## Observed symptom and diagnosis

```text
System.IO.FileLoadException:
Could not load file or assembly
AIQuantTradingResearch.Infrastructure.Tests.dll.
An Application Control policy has blocked this file.
(0x800711C7)
```

The observed Code Integrity evidence was Event ID 3077 under
`VerifiedAndReputableDesktop`, policy ID
`0283ac0f-fff1-49ae-ada1-8a933130cad6`; other machines may differ.

```powershell
(CiTool.exe -lp -json | ConvertFrom-Json).Policies | Where-Object { $_.IsEnforced -eq "True" } | Select-Object PolicyID,FriendlyName,BasePolicyID,IsEnforced | Format-List
Get-WinEvent -FilterHashtable @{ LogName='Microsoft-Windows-CodeIntegrity/Operational'; Id=3077; StartTime=(Get-Date).AddMinutes(-60) } | Select-Object -First 10 TimeCreated,Id,Message | Format-List
```

## Windows SDK

```powershell
winget install Microsoft.WindowsSDK.10.0.22621
```

This supplies `signtool.exe`. The observed installed package was Windows
Software Development Kit `10.0.22621.2428`; servicing versions may vary.

## Local certificate

Run [create-self-signed-certificate.ps1](../../eng/sec/create-self-signed-certificate.ps1)
once on the developer machine. It creates CodeSigning certificate
`CN=AIQuantTradingDev` in `Cert:\CurrentUser\My`, exports a temporary `.cer`
under `$env:TEMP`, and imports it into `Cert:\CurrentUser\Root`.

This is local setup only. Never commit private keys, PFX files, passwords, or
other private certificate material.

## AutoSignTestBinaries

`src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj`
defaults `AutoSignTestBinaries` to `false`. Its target runs after `Build` only
when the property is `true` and `Configuration` is `Debug`. It invokes the
configured Windows SDK `signtool.exe` and signs the project's own `$(TargetPath)`
with `AIQuantTradingDev`, SHA-256, and the configured timestamp URL. It does not
sign every dependency.

## Local opt-in

The project imports repository-root `Directory.Build.local.props` when present:

```xml
<Project>
  <PropertyGroup>
    <AutoSignTestBinaries>true</AutoSignTestBinaries>
  </PropertyGroup>
</Project>
```

This file is machine-specific, already ignored by `.gitignore`, and must remain
uncommitted. Remove it or set the property to `false` to disable signing.

## Build, test, and verification

```powershell
dotnet build
dotnet test --no-build
Get-AuthenticodeSignature .\tests\AIQuantTradingResearch.Infrastructure.Tests\bin\Debug\net10.0\AIQuantTradingResearch.Infrastructure.Tests.dll | Format-List Status,StatusMessage,SignerCertificate,TimeStamperCertificate
```

Expect `Status = Valid` and the local development certificate as signer.
Repeat the `CiTool.exe` query to verify App Control remains enforced. If the
artifact is `NotSigned`, check the certificate stores, SDK tool path, local
props import, and whether a later build replaced signed output. Do not
automatically re-sign third-party dependencies.

## Security hygiene

This arrangement is local development only and makes no production-trust
claim. Never commit private keys, PFX files, passwords, certificate exports, or
machine-specific paths. Do not weaken global App Control policy or describe
this arrangement as an App Control “bypass”.

## WP08 separation

App Control initially prevented the WP08 test assembly from loading; local
signing resolved that environment prerequisite. After loading was restored,
Worker B still reproduced `0xC0000142` in the restart-specific WP08 lifecycle
scenario. SAC/App Control remediation and the WP08 lifecycle defect are
independent concerns; #233 remained Open / Backlog at that point.
