# PowerShell Engineering Reference Implementation

## Purpose

The PowerShell Engineering Reference Implementation demonstrates how to
design, implement, validate, test, document, secure, and review
production-quality PowerShell automation using the engineering
principles defined by the AI Engineering Toolkit.

Its purpose is to provide a concrete example of how PowerShell scripts
should be structured and governed when they are treated as real
engineering assets rather than disposable utility scripts.

This reference implementation demonstrates how the PowerShell Playbooks
can be translated into maintainable automation.

------------------------------------------------------------------------

# Objectives

This reference implementation aims to demonstrate:

-   Script architecture.
-   Script structure.
-   Parameter design.
-   Input validation.
-   Error handling.
-   Structured logging.
-   Testing.
-   Documentation.
-   Security.
-   Script review.
-   Idempotent behavior.
-   Safe file operations.
-   Meaningful exit codes.
-   AI-assisted script generation and validation.

------------------------------------------------------------------------

# Scope

This reference implementation focuses on PowerShell engineering
practices that can be reused across repository automation, bootstrap
scripts, build tooling, DevOps workflows, validation utilities, and
operational scripts.

It includes:

-   Script organization.
-   Parameter handling.
-   Configuration.
-   Validation.
-   Logging.
-   Error management.
-   Testing.
-   Security.
-   Documentation.
-   Execution contracts.
-   Review criteria.

It does not define one mandatory business-specific automation scenario.

The implementation should remain small enough to understand while
realistic enough to demonstrate production-quality practices.

------------------------------------------------------------------------

# Engineering Philosophy

PowerShell scripts should be treated as software.

A useful model is:

``` text
Engineering Requirement

↓

Script Design

↓

Structured Implementation

↓

Validation

↓

Testing

↓

Secure Execution

↓

Review

↓

Reliable Automation
```

A PowerShell script is complete only when its behavior can be
understood, validated, and maintained.

------------------------------------------------------------------------

# Reference Scenario

This reference implementation uses a repository engineering utility as
the primary example.

The script validates whether a repository contains required engineering
assets.

Example responsibility:

``` text
Validate-Repository.ps1
```

The script verifies required repository files and directories such as:

-   `src/`
-   `tests/`
-   `docs/`
-   `eng/`
-   `.github/`
-   `README.md`
-   `Directory.Build.props`
-   `Directory.Packages.props`
-   `global.json`

The scenario is intentionally simple enough to understand while
demonstrating realistic engineering concerns.

------------------------------------------------------------------------

# Reference Structure

A representative implementation may use:

``` text
02-powershell-engineering/
│
├── README.md
│
├── src/
│   ├── Validate-Repository.ps1
│   └── modules/
│       ├── Logging.psm1
│       ├── Validation.psm1
│       └── Repository.psm1
│
├── tests/
│   ├── Validate-Repository.Tests.ps1
│   ├── Validation.Tests.ps1
│   └── Repository.Tests.ps1
│
├── examples/
│   ├── valid-repository/
│   └── invalid-repository/
│
└── docs/
    └── DESIGN.md
```

The exact physical layout may vary.

The important principle is separation of responsibilities.

------------------------------------------------------------------------

# Script Responsibilities

The primary script should focus on orchestration.

It should:

1.  Receive parameters.
2.  Validate execution prerequisites.
3.  Load required modules.
4.  Execute repository validation.
5.  Produce structured results.
6.  Return an appropriate process exit code.

The script should avoid containing every implementation detail directly.

------------------------------------------------------------------------

# Script Architecture

A useful architecture is:

``` text
Entry Script

↓

Parameter Validation

↓

Execution Context

↓

Domain Operations

↓

Logging

↓

Result

↓

Exit Code
```

The entry script coordinates the workflow.

Reusable logic should remain in focused functions or modules where
complexity justifies it.

------------------------------------------------------------------------

# Entry Script

A representative entry point may be:

``` text
src/Validate-Repository.ps1
```

Its responsibilities should remain limited to:

-   Parameter binding.
-   Module import.
-   Top-level error handling.
-   Workflow orchestration.
-   Final status reporting.

------------------------------------------------------------------------

# Script Header

Production PowerShell scripts should contain comment-based help.

Example:

``` powershell
<#
.SYNOPSIS
Validates the engineering structure of a repository.

.DESCRIPTION
Checks required repository files and directories and returns
structured validation results.

.PARAMETER RepositoryPath
Path to the repository that should be validated.

.PARAMETER Strict
When specified, warnings are treated as validation failures.

.EXAMPLE
./Validate-Repository.ps1 -RepositoryPath ../sample-repository

.EXAMPLE
./Validate-Repository.ps1 -RepositoryPath ../sample-repository -Strict
#>
```

Documentation should remain synchronized with actual behavior.

------------------------------------------------------------------------

# Parameters

Parameters should define the public interface of the script.

Example:

``` powershell
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [ValidateNotNullOrEmpty()]
    [string]$RepositoryPath,

    [Parameter(Mandatory = $false)]
    [switch]$Strict
)
```

Parameters should be explicit, validated, and understandable.

------------------------------------------------------------------------

# Parameter Design Principles

Parameters should be:

-   Clearly named.
-   Strongly typed.
-   Validated where practical.
-   Minimal.
-   Stable.
-   Appropriate to the task.

Avoid ambiguous parameters such as:

``` text
-param1
-value
-option
```

Prefer intent-revealing names.

------------------------------------------------------------------------

# Mandatory Parameters

Parameters should be mandatory only when no safe default exists.

For example:

``` powershell
[Parameter(Mandatory = $true)]
[string]$RepositoryPath
```

is appropriate because the validation target must be known.

------------------------------------------------------------------------

# Optional Parameters

Optional parameters should have predictable behavior.

Example:

``` powershell
[switch]$Strict
```

should clearly mean that warnings become failures.

Optional behavior should not create surprising execution paths.

------------------------------------------------------------------------

# Default Values

Defaults should be safe.

Example:

``` powershell
[string]$RepositoryPath = (Get-Location).Path
```

may be appropriate if the current directory is a valid default for the
script.

Defaults should not point to production or destructive targets.

------------------------------------------------------------------------

# Parameter Validation

PowerShell-native validation attributes should be used where
appropriate.

Examples include:

``` powershell
[ValidateNotNullOrEmpty()]
[ValidateSet(...)]
[ValidateRange(...)]
[ValidateScript(...)]
```

Validation should fail early.

------------------------------------------------------------------------

# Path Validation

Repository paths should be normalized and validated.

Example:

``` powershell
$resolvedRepositoryPath = Resolve-Path -Path $RepositoryPath -ErrorAction Stop
```

The script should verify that the resolved path represents a directory.

------------------------------------------------------------------------

# Avoid Implicit Global State

Scripts should avoid unnecessary reliance on:

-   Current working directory.
-   Global variables.
-   User profile state.
-   Previously imported modules.
-   Environment-specific defaults.

Important execution dependencies should be explicit.

------------------------------------------------------------------------

# Execution Context

A script may create an execution context object.

Example:

``` powershell
$context = [pscustomobject]@{
    RepositoryPath = $resolvedRepositoryPath.Path
    Strict         = $Strict.IsPresent
    StartedAt      = Get-Date
}
```

Context objects can simplify passing shared execution information
without introducing global state.

------------------------------------------------------------------------

# Module Design

Reusable functionality may be separated into modules.

Potential modules include:

``` text
Logging.psm1
Validation.psm1
Repository.psm1
```

Modules should have cohesive responsibilities.

------------------------------------------------------------------------

# Logging Module

The logging module may expose functions such as:

``` powershell
Write-LogInformation
Write-LogWarning
Write-LogError
Write-LogSuccess
```

Logging should remain consistent across scripts.

------------------------------------------------------------------------

# Validation Module

The validation module may expose:

``` powershell
Test-RequiredDirectory
Test-RequiredFile
New-ValidationResult
```

Validation functions should return data rather than terminate execution
unexpectedly.

------------------------------------------------------------------------

# Repository Module

The repository module may encapsulate knowledge about required
repository assets.

Example:

``` powershell
Get-RequiredRepositoryDirectories
Get-RequiredRepositoryFiles
Test-RepositoryStructure
```

This separates repository policy from script orchestration.

------------------------------------------------------------------------

# Function Design

Functions should:

-   Have one clear responsibility.
-   Accept explicit input.
-   Return predictable output.
-   Avoid hidden side effects.
-   Be testable independently.

Example:

``` powershell
function Test-RequiredFile {
    param(
        [Parameter(Mandatory = $true)]
        [string]$RepositoryPath,

        [Parameter(Mandatory = $true)]
        [string]$RelativePath
    )

    $target = Join-Path -Path $RepositoryPath -ChildPath $RelativePath

    return Test-Path -Path $target -PathType Leaf
}
```

------------------------------------------------------------------------

# Pure Functions Where Practical

Functions should avoid side effects when their purpose is evaluation.

Prefer:

``` powershell
$result = Test-RequiredFile ...
```

over functions that both validate and modify repository state unless
modification is explicitly part of their responsibility.

------------------------------------------------------------------------

# Return Objects

Functions should return structured objects when multiple pieces of
information are relevant.

Example:

``` powershell
[pscustomobject]@{
    Name     = "Directory.Build.props"
    Path     = $target
    Exists   = $exists
    Severity = "Error"
}
```

Structured output supports testing, logging, and downstream automation.

------------------------------------------------------------------------

# Validation Result Model

A reusable validation result may contain:

``` text
Name
Category
Status
Severity
Message
Target
Evidence
```

Example:

``` powershell
[pscustomobject]@{
    Name     = "RequiredFile"
    Category = "RepositoryStructure"
    Status   = "Failed"
    Severity = "Error"
    Message  = "Required file was not found."
    Target   = "Directory.Build.props"
    Evidence = $target
}
```

------------------------------------------------------------------------

# Status Values

A consistent validation status model may include:

``` text
Passed
Warning
Failed
Skipped
```

Status semantics should remain stable.

------------------------------------------------------------------------

# Required Repository Directories

Example:

``` powershell
$requiredDirectories = @(
    "src",
    "tests",
    "docs",
    "eng",
    ".github"
)
```

The list should represent the policy being demonstrated.

------------------------------------------------------------------------

# Required Repository Files

Example:

``` powershell
$requiredFiles = @(
    "README.md",
    "Directory.Build.props",
    "Directory.Packages.props",
    "global.json",
    ".editorconfig",
    ".gitignore"
)
```

Policy data should be separated from execution logic where practical.

------------------------------------------------------------------------

# Repository Validation Workflow

A representative workflow is:

``` text
Resolve Repository Path

↓

Validate Directories

↓

Validate Files

↓

Aggregate Results

↓

Apply Strict Policy

↓

Report

↓

Return Exit Code
```

The workflow should be easy to follow.

------------------------------------------------------------------------

# Directory Validation

Example:

``` powershell
foreach ($directory in $requiredDirectories) {
    $exists = Test-RequiredDirectory `
        -RepositoryPath $context.RepositoryPath `
        -RelativePath $directory

    # Add structured validation result.
}
```

Individual checks should remain independent.

------------------------------------------------------------------------

# File Validation

Example:

``` powershell
foreach ($file in $requiredFiles) {
    $exists = Test-RequiredFile `
        -RepositoryPath $context.RepositoryPath `
        -RelativePath $file

    # Add structured validation result.
}
```

Validation should not modify the target repository.

------------------------------------------------------------------------

# Logging Philosophy

Logs should describe meaningful execution events.

A useful pattern is:

``` text
START
CHECK
PASS
WARN
FAIL
SUMMARY
```

Example:

``` text
[INFO] Starting repository validation.
[PASS] Directory exists: src
[FAIL] Required file missing: Directory.Build.props
[INFO] Repository validation completed.
```

Logs should not merely narrate every line of code.

------------------------------------------------------------------------

# Structured Logging

Where practical, logging functions should accept structured metadata.

Example:

``` powershell
Write-LogInformation `
    -Message "Repository validation started." `
    -Data @{
        RepositoryPath = $context.RepositoryPath
    }
```

Structured metadata improves diagnostics and machine processing.

------------------------------------------------------------------------

# Log Levels

A simple severity model may include:

``` text
Debug
Information
Warning
Error
```

Severity should communicate operational impact.

------------------------------------------------------------------------

# Log Security

Logs must not contain:

-   Passwords.
-   Tokens.
-   Private keys.
-   Secret configuration.
-   Sensitive environment data without need.

Validation output should remain safe to display in CI logs.

------------------------------------------------------------------------

# Error Handling Philosophy

Errors should be:

-   Detected early.
-   Classified.
-   Reported clearly.
-   Preserved with useful context.
-   Allowed to propagate when execution cannot continue safely.

The script should not hide failures.

------------------------------------------------------------------------

# Top-Level Error Handling

The entry script may use a top-level `try/catch`.

Example:

``` powershell
try {
    # Execute validation workflow.
}
catch {
    Write-Error "Repository validation failed: $($_.Exception.Message)"
    exit 2
}
```

Top-level handling should prevent unhandled failures while preserving
diagnostic information.

------------------------------------------------------------------------

# Avoid Empty Catch Blocks

Never use:

``` powershell
try {
    # operation
}
catch {
}
```

Failures should not disappear.

------------------------------------------------------------------------

# ErrorAction

Commands that must fail predictably should use appropriate error
behavior.

Example:

``` powershell
Resolve-Path `
    -Path $RepositoryPath `
    -ErrorAction Stop
```

This makes exception flow explicit.

------------------------------------------------------------------------

# Error Context

Error messages should include meaningful execution context.

Prefer:

``` text
Unable to resolve repository path '/sample/path'.
```

over:

``` text
Operation failed.
```

Useful errors reduce troubleshooting time.

------------------------------------------------------------------------

# Exit Codes

The script should use meaningful process exit codes.

A simple contract is:

``` text
0 = Validation Passed
1 = Validation Failed
2 = Execution Error
```

If additional codes are introduced, they should be documented.

------------------------------------------------------------------------

# Validation Failure vs Execution Failure

The script should distinguish:

``` text
Validation Failure
    ↓
Repository does not satisfy policy.

Execution Failure
    ↓
Script could not complete validation.
```

These conditions represent different engineering states.

------------------------------------------------------------------------

# Summary Output

At the end of execution, the script should provide a concise summary.

Example:

``` text
Repository Validation Summary

Passed:   9
Warnings: 1
Failed:   2

Result: Failed
```

The summary should be derived from structured validation results.

------------------------------------------------------------------------

# Machine-Readable Results

Advanced implementations may support structured output.

Example:

``` powershell
$result | ConvertTo-Json -Depth 5
```

This enables use in CI or other automation.

------------------------------------------------------------------------

# Output Modes

A script may support:

``` text
Human-readable output
Machine-readable output
```

For example:

``` powershell
-OutputFormat Text
-OutputFormat Json
```

Only add output modes when they provide real engineering value.

------------------------------------------------------------------------

# Idempotency

Validation scripts should naturally be idempotent.

Repeated execution should:

-   Produce equivalent results for equivalent repository state.
-   Not modify the target repository.
-   Not create duplicate artifacts.
-   Not depend on previous execution.

Example:

``` text
Run 1
    ↓
Failed

Repository unchanged

Run 2
    ↓
Failed with same evidence
```

------------------------------------------------------------------------

# Safe File Operations

If a PowerShell script modifies files, it should validate:

-   Target path.
-   Existing content.
-   Overwrite policy.
-   Backup or recovery requirements.
-   Scope.

This reference validation script intentionally performs read-only
repository checks.

------------------------------------------------------------------------

# Destructive Operations

The reference implementation should not demonstrate destructive
operations unless they are central to the scenario.

If destructive behavior is later added, it should require:

-   Explicit parameter.
-   Target validation.
-   Appropriate confirmation or approval model.
-   Clear logging.
-   Recovery strategy.

------------------------------------------------------------------------

# Configuration

Script policy may be externalized when it becomes sufficiently complex.

Example:

``` json
{
  "requiredDirectories": [
    "src",
    "tests",
    "docs",
    "eng",
    ".github"
  ],
  "requiredFiles": [
    "README.md",
    "Directory.Build.props",
    "Directory.Packages.props"
  ]
}
```

Configuration should be introduced only when it improves
maintainability.

------------------------------------------------------------------------

# Configuration Validation

External configuration should be validated before execution.

The script should verify:

-   File exists.
-   Format is valid.
-   Required fields exist.
-   Values are supported.

Invalid configuration should fail clearly.

------------------------------------------------------------------------

# Environment Variables

Environment variables may be used for non-sensitive operational
configuration where appropriate.

They should not become hidden substitutes for required parameters.

Important behavior should remain discoverable.

------------------------------------------------------------------------

# Cross-Platform Behavior

PowerShell automation should remain cross-platform where practical.

Avoid unnecessary use of:

-   Windows-only paths.
-   Registry dependencies.
-   Platform-specific utilities.

Path handling should use PowerShell abstractions such as:

``` powershell
Join-Path
Resolve-Path
```

------------------------------------------------------------------------

# Path Separators

Do not manually assume `\` or `/` when PowerShell path functions can
provide portable behavior.

------------------------------------------------------------------------

# Encoding

Files created or modified by scripts should use explicit and
repository-compatible encoding where necessary.

Encoding behavior should remain consistent across development
environments.

------------------------------------------------------------------------

# Testing Strategy

This reference implementation should demonstrate PowerShell testing.

The test strategy should include:

``` text
Unit Tests

↓

Function Tests

↓

Script Tests

↓

Failure Tests

↓

Idempotency Tests
```

Tests should focus on observable behavior.

------------------------------------------------------------------------

# Test Framework

A PowerShell testing framework such as Pester may be used for reference
purposes.

The framework is an implementation choice.

The engineering principles remain authoritative.

------------------------------------------------------------------------

# Unit Tests

Unit tests should verify focused functions.

Example:

``` powershell
Describe "Test-RequiredFile" {
    It "returns true when the required file exists" {
        # Arrange
        # Act
        # Assert
    }

    It "returns false when the required file does not exist" {
        # Arrange
        # Act
        # Assert
    }
}
```

------------------------------------------------------------------------

# Script-Level Tests

Script tests should verify complete behavior.

Scenarios may include:

-   Valid repository.
-   Missing required directory.
-   Missing required file.
-   Invalid repository path.
-   Strict mode.
-   Repeated execution.

------------------------------------------------------------------------

# Temporary Test Repositories

Tests should avoid modifying real development repositories.

A useful model is:

``` text
Create Temporary Directory

↓

Create Test Repository Structure

↓

Execute Script

↓

Assert Results

↓

Clean Temporary Directory
```

This improves isolation and repeatability.

------------------------------------------------------------------------

# Valid Repository Fixture

A test fixture may contain:

``` text
valid-repository/
│
├── src/
├── tests/
├── docs/
├── eng/
├── .github/
├── README.md
├── Directory.Build.props
├── Directory.Packages.props
├── global.json
├── .editorconfig
└── .gitignore
```

The validation should succeed.

------------------------------------------------------------------------

# Invalid Repository Fixture

An invalid fixture may intentionally omit required assets.

Example:

``` text
invalid-repository/
│
├── src/
└── README.md
```

The validation should return expected failures.

------------------------------------------------------------------------

# Test Determinism

Tests should not depend on:

-   Test execution order.
-   Current developer repository.
-   User profile state.
-   Internet access.
-   Existing environment files.

Deterministic tests improve reliability.

------------------------------------------------------------------------

# Failure Testing

Tests should verify failure scenarios such as:

-   Invalid path.
-   Missing required configuration.
-   Module import failure.
-   Unsupported output format.

Failure behavior is part of the script contract.

------------------------------------------------------------------------

# Exit Code Testing

Script-level tests should verify process exit codes where practical.

Example expectation:

``` text
Valid repository
    ↓
Exit 0

Invalid repository
    ↓
Exit 1

Execution error
    ↓
Exit 2
```

------------------------------------------------------------------------

# Logging Tests

Testing may verify that important log events are emitted.

Avoid overly coupling tests to exact text unless log format is part of
the contract.

Prefer verification of:

-   Severity.
-   Event category.
-   Key structured properties.

------------------------------------------------------------------------

# Security Tests

Security-focused tests may verify:

-   Sensitive values are not logged.
-   Unsafe paths are rejected.
-   Unauthorized destructive behavior is unavailable.
-   External configuration is validated.

Security requirements should be executable where practical.

------------------------------------------------------------------------

# Documentation

The reference implementation should document:

-   Purpose.
-   Usage.
-   Parameters.
-   Examples.
-   Exit codes.
-   Output.
-   Validation.
-   Test execution.
-   Security considerations.
-   Known limitations.

Users should not need to inspect the script implementation to understand
basic usage.

------------------------------------------------------------------------

# Example Usage

Example:

``` powershell
./src/Validate-Repository.ps1 `
    -RepositoryPath ../../sample-repository
```

Strict mode:

``` powershell
./src/Validate-Repository.ps1 `
    -RepositoryPath ../../sample-repository `
    -Strict
```

------------------------------------------------------------------------

# Example Successful Output

``` text
[INFO] Repository validation started.

[PASS] Directory: src
[PASS] Directory: tests
[PASS] Directory: docs
[PASS] Directory: eng
[PASS] Directory: .github

[PASS] File: README.md
[PASS] File: Directory.Build.props
[PASS] File: Directory.Packages.props
[PASS] File: global.json

Repository Validation Summary

Passed: 9
Warnings: 0
Failed: 0

Result: Passed
```

------------------------------------------------------------------------

# Example Failed Output

``` text
[INFO] Repository validation started.

[PASS] Directory: src
[FAIL] Directory: tests
[PASS] Directory: docs

[PASS] File: README.md
[FAIL] File: Directory.Build.props

Repository Validation Summary

Passed: 3
Warnings: 0
Failed: 2

Result: Failed
```

------------------------------------------------------------------------

# Security

The reference implementation should follow secure PowerShell practices.

It should not:

-   Hardcode credentials.
-   Execute downloaded scripts.
-   Disable security controls.
-   Use unnecessary elevated privileges.
-   Trust arbitrary input paths blindly.
-   Log sensitive values.

The validation scenario should require no administrative privilege.

------------------------------------------------------------------------

# Least Privilege

The script should operate with normal user permissions.

Read-only validation should not require elevated access.

If a target cannot be inspected because of permissions, the failure
should be reported rather than bypassed through privilege escalation.

------------------------------------------------------------------------

# Input Trust

All external input should be treated as untrusted.

Examples include:

-   Paths.
-   Configuration files.
-   Environment variables.
-   CLI parameters.

Input should be validated before use.

------------------------------------------------------------------------

# Path Safety

Path handling should ensure the script acts only against the intended
repository.

If later versions introduce write operations, scope should be checked
carefully before modification.

------------------------------------------------------------------------

# External Command Safety

If external commands are used, arguments should be constructed
explicitly.

Avoid unsafe dynamic command execution such as:

``` powershell
Invoke-Expression $userInput
```

unless a strongly justified and controlled scenario exists.

------------------------------------------------------------------------

# Avoid Invoke-Expression

`Invoke-Expression` should not be used for normal command execution.

Direct command invocation is easier to understand, test, and secure.

------------------------------------------------------------------------

# Dependency Management

PowerShell module dependencies should be minimized.

Before adding a module, consider whether:

-   PowerShell already provides the required capability.
-   The dependency is maintained.
-   The dependency is trusted.
-   The additional complexity is justified.

This reference should prefer native PowerShell where practical.

------------------------------------------------------------------------

# Module Import

Local modules should be imported through deterministic
repository-relative paths.

Example:

``` powershell
$modulePath = Join-Path `
    -Path $PSScriptRoot `
    -ChildPath "modules/Validation.psm1"

Import-Module $modulePath -Force
```

Avoid relying on arbitrary machine-wide module resolution for
repository-owned modules.

------------------------------------------------------------------------

# `$PSScriptRoot`

Repository-local paths should generally be based on `$PSScriptRoot`
rather than the current shell working directory.

This improves predictable execution.

------------------------------------------------------------------------

# Strict Mode

Scripts may use:

``` powershell
Set-StrictMode -Version Latest
```

when compatible with repository requirements.

Strict mode helps expose certain scripting errors earlier.

------------------------------------------------------------------------

# Error Preference

Scripts may set execution-local error behavior intentionally.

Example:

``` powershell
$ErrorActionPreference = "Stop"
```

when the script is designed around exception-based top-level handling.

Global user configuration should not be changed.

------------------------------------------------------------------------

# Progress Output

Progress output should be used only when it improves long-running
execution visibility.

Short validation scripts generally do not require progress bars.

Logging should remain the primary execution communication model.

------------------------------------------------------------------------

# Performance

PowerShell performance should remain appropriate to the automation
workload.

Avoid unnecessary:

-   Repeated filesystem scans.
-   Pipeline transformations in hot paths.
-   External process calls.
-   Large object accumulation.

Performance optimization should be evidence-driven.

------------------------------------------------------------------------

# Large Repositories

If repository validation scales to large structures, implementation may
evolve to:

-   Minimize recursive traversal.
-   Validate known paths directly.
-   Cache resolved paths where useful.
-   Avoid repeated filesystem calls.

The initial reference should remain simple.

------------------------------------------------------------------------

# Observability

PowerShell automation should expose enough operational information to
answer:

-   What was executed?
-   Against which target?
-   What succeeded?
-   What failed?
-   Why did it fail?
-   What is the final status?

Logs and structured results should provide that evidence.

------------------------------------------------------------------------

# Completion Report

A script or AI agent executing this reference should report:

``` text
Target Repository
Checks Executed
Passed Checks
Warnings
Failed Checks
Execution Errors
Exit Code
Overall Status
```

This provides a clear execution contract.

------------------------------------------------------------------------

# Review Checklist

A PowerShell engineering review should evaluate:

``` text
Architecture
- Is orchestration separated from reusable logic?

Structure
- Are responsibilities organized predictably?

Parameters
- Are inputs explicit and validated?

Error Handling
- Are failures visible and actionable?

Logging
- Are meaningful events reported?

Validation
- Is repository state evaluated correctly?

Testing
- Are important scenarios automated?

Documentation
- Is usage understandable?

Security
- Are inputs and sensitive information handled safely?

Maintainability
- Can the script evolve without unnecessary complexity?
```

------------------------------------------------------------------------

# Script Review Outcome

A script may be classified as:

``` text
Approved
Approved with Recommendations
Changes Required
Rejected
```

Review should be evidence-based.

------------------------------------------------------------------------

# AI-Assisted PowerShell Engineering

Coding agents may use this reference implementation as contextual
guidance.

A recommended workflow is:

``` text
Engineer Defines Task

↓

Agent Reads PowerShell Playbooks

↓

Agent Inspects Existing Scripts

↓

Agent Uses Reference Implementation

↓

Agent Produces Plan

↓

Agent Implements

↓

Agent Runs Tests

↓

Agent Runs Validation

↓

Engineer Reviews
```

The reference implementation is an example, not an instruction to copy
every detail.

------------------------------------------------------------------------

# AI Agent Boundaries

When creating or modifying PowerShell scripts, an AI agent should
normally be allowed to:

-   Inspect repository scripts.
-   Modify approved PowerShell assets.
-   Add corresponding tests.
-   Run local tests.
-   Run local validation.

It should not automatically:

-   Elevate privileges.
-   Modify production systems.
-   Execute destructive commands.
-   Download untrusted scripts.
-   Expose secrets.
-   Refactor unrelated automation.

------------------------------------------------------------------------

# AI Output Contract

An AI-generated PowerShell change should report:

``` text
Files Created
Files Modified
Parameters Added or Changed
Behavior Changed
Tests Added or Updated
Validation Executed
Validation Results
Security Considerations
Remaining Risks
```

This makes generated automation reviewable.

------------------------------------------------------------------------

# Prompt Quality Integration

Prompts used for PowerShell engineering should apply:

-   Clear objectives.
-   Authoritative repository context.
-   Explicit script scope.
-   Defined modification boundaries.
-   Structured execution instructions.
-   Output contracts.
-   Validation requirements.
-   Failure handling.
-   Security and safety controls.

Prompt quality should match the script's operational risk.

------------------------------------------------------------------------

# Reference Implementation Validation

The reference implementation itself should be validated through:

``` text
PowerShell Parsing

↓

Module Import

↓

Unit Tests

↓

Script Tests

↓

Failure Tests

↓

Security Checks

↓

Repeated Execution

↓

Review
```

Every stage should produce observable evidence.

------------------------------------------------------------------------

# Syntax Validation

PowerShell files should parse successfully.

Validation may use PowerShell's parser APIs or equivalent repository
tooling.

Syntax validation should run before behavioral tests.

------------------------------------------------------------------------

# Test Execution

Example:

``` powershell
Invoke-Pester ./tests
```

The exact test command should be documented by the reference
implementation.

------------------------------------------------------------------------

# Static Analysis

Where repository standards require it, PowerShell static analysis may be
applied.

Analyzer results should be treated according to repository policy.

Static analysis complements but does not replace tests.

------------------------------------------------------------------------

# Reference Acceptance Criteria

The PowerShell Engineering Reference Implementation is accepted when:

-   The primary script has a clear responsibility.
-   Parameters are explicit and validated.
-   Repository paths are handled safely.
-   Reusable logic is separated appropriately.
-   Validation results are structured.
-   Logging is meaningful.
-   Validation and execution failures are distinguished.
-   Exit codes are documented.
-   Required tests pass.
-   Failure scenarios are covered.
-   Repeated execution is safe.
-   No unnecessary elevated privilege is required.
-   No secrets are hardcoded or exposed.
-   Documentation explains usage and behavior.
-   The implementation follows the PowerShell Playbooks.
-   Review criteria can be applied consistently.
-   AI agents can use the implementation as a concrete example without
    treating it as the authoritative standard.

------------------------------------------------------------------------

# Related PowerShell Playbooks

This reference implementation demonstrates concepts defined by:

``` text
PowerShell Playbooks/
├── README.md
├── 01-script-architecture.md
├── 02-script-structure.md
├── 03-parameter-design.md
├── 04-error-handling.md
├── 05-logging.md
├── 06-validation.md
├── 07-testing.md
├── 08-documentation.md
├── 09-security.md
└── 10-script-review.md
```

The playbooks remain the authoritative engineering methodology.

This reference implementation demonstrates one concrete application of
that methodology.

------------------------------------------------------------------------

# Related Repository Bootstrap Reference

This reference implementation complements:

``` text
01-repository-bootstrap.md
```

The Repository Bootstrap reference demonstrates where PowerShell
automation fits within a governed repository.

The PowerShell Engineering reference demonstrates how that automation
should itself be engineered.

------------------------------------------------------------------------

# Related GitHub Playbooks

PowerShell automation used in repository workflows should align with
applicable GitHub governance, including:

-   Pull request review.
-   Repository validation.
-   Release management.
-   Security.
-   Repository review.

Automation should reinforce repository governance rather than bypass it.

------------------------------------------------------------------------

# Related Prompt Quality Guidelines

Prompts used to generate or modify PowerShell automation should follow:

``` text
Prompt Quality Guidelines/
├── 01-prompt-quality-principles.md
├── 02-prompt-clarity.md
├── 03-context-management.md
├── 04-scope-and-boundaries.md
├── 05-instruction-design.md
├── 06-output-contracts.md
├── 07-validation-and-acceptance.md
├── 08-error-and-ambiguity-handling.md
├── 09-security-and-safety.md
└── 10-prompt-review.md
```

This ensures AI-generated scripting work remains controlled and
verifiable.

------------------------------------------------------------------------

# Reference Implementation Principles

This reference should demonstrate:

``` text
Explicit Parameters

↓

Validated Input

↓

Clear Responsibilities

↓

Structured Execution

↓

Structured Results

↓

Meaningful Logging

↓

Predictable Errors

↓

Automated Testing

↓

Security Controls

↓

Reviewable Outcome
```

These principles are more important than any particular function or
module layout.

------------------------------------------------------------------------

# What This Reference Should Not Demonstrate

This reference should not become:

-   A general-purpose PowerShell framework.
-   A large utility library.
-   A collection of unrelated scripts.
-   A dependency-heavy scripting platform.
-   A Windows-only automation model without necessity.
-   A substitute for the PowerShell Playbooks.
-   A production-administration script requiring broad privileges.

Its responsibility is to demonstrate disciplined PowerShell engineering.

------------------------------------------------------------------------

# Evolution

As the Toolkit evolves, this reference implementation should evolve when
changes affect:

-   PowerShell Playbooks.
-   Repository bootstrap practices.
-   Validation standards.
-   Logging standards.
-   Security requirements.
-   Testing practices.
-   Prompt execution models.
-   AI-assisted engineering workflows.

Changes should preserve the distinction between engineering guidance and
concrete implementation.

------------------------------------------------------------------------

# Success Criteria

The PowerShell Engineering Reference Implementation succeeds when an
engineer or AI agent can use it to understand:

-   How a production-quality PowerShell script should be organized.
-   How parameters should be designed and validated.
-   How reusable logic should be separated.
-   How errors should be handled.
-   How structured validation results should be represented.
-   How logging should communicate meaningful execution behavior.
-   How scripts should return useful exit codes.
-   How tests should validate both success and failure scenarios.
-   How scripts should remain safe and cross-platform where practical.
-   How security boundaries should be preserved.
-   How AI-generated PowerShell changes should be validated and
    reviewed.

The implementation should be realistic enough to guide engineering work
while remaining simple enough to study independently.

------------------------------------------------------------------------

# Conclusion

PowerShell automation should be engineered with the same discipline
applied to other software.

The reference model is:

``` text
Requirement

↓

Explicit Parameters

↓

Validated Context

↓

Structured Script

↓

Reusable Functions

↓

Logging

↓

Error Handling

↓

Automated Tests

↓

Security Validation

↓

Review

↓

Reliable Automation
```

The purpose of this reference implementation is not to demonstrate how
much can be accomplished in a PowerShell script.

It is to demonstrate how PowerShell automation can remain
understandable, testable, secure, predictable, and maintainable as
engineering systems grow.

The central principle is:

> **A PowerShell script becomes an engineering asset when its inputs are
> explicit, its behavior is controlled, its failures are observable, its
> outputs are verifiable, and its implementation can be safely
> maintained by both human engineers and AI coding agents.**
