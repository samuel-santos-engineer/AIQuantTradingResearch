
# AIQuantTradingResearch Coding Standards

## Purpose

This document defines the coding standards adopted by AIQuantTradingResearch.

The objective is to ensure that every contribution maintains a consistent level of readability, maintainability, quality, and professionalism.

These standards apply to all source code, tests, scripts, and documentation contained in this repository.

---

# Guiding Principles

Code should be:

- Simple
- Readable
- Maintainable
- Testable
- Secure
- Well documented

Software is written for people first and computers second.

---

# General Philosophy

Favor:

- Explicitness over cleverness.
- Readability over brevity.
- Composition over inheritance.
- Simplicity over premature optimization.
- Small, focused classes and methods.
- Meaningful names.

Avoid unnecessary abstractions.

---

# SOLID Principles

All production code should follow the SOLID principles whenever practical.

- Single Responsibility Principle
- Open/Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

SOLID should guide architectural decisions rather than become an excuse for unnecessary complexity.

---

# Clean Code

Code should strive to:

- Express intent clearly.
- Minimize side effects.
- Avoid duplicated logic.
- Keep functions small.
- Reduce nesting.
- Handle errors consistently.

---

# Naming Conventions

## Projects

```text
AIQuantTradingResearch.Api

AIQuantTradingResearch.SharedKernel

AIQuantTradingResearch.MarketData

AIQuantTradingResearch.Backtesting
```

---

## Namespaces

Match project names.

Example:

```csharp
namespace AIQuantTradingResearch.Api;
```

---

## Classes

Use PascalCase.

Examples:

```
MarketDataService

PredictionEngine

TradeSignal

PortfolioManager
```

---

## Interfaces

Prefix with "I".

Examples:

```
IMarketDataProvider

IPredictionService

ITradingStrategy
```

---

## Methods

Use PascalCase.

Methods should describe actions.

Examples:

```
GetHistoricalCandles()

TrainModel()

ExecuteBacktest()
```

---

## Variables

Use camelCase.

Names should be descriptive.

Avoid abbreviations.

---

## Constants

Use PascalCase.

Example:

```
MaxRetryAttempts
```

---

# File Organization

One public class per file.

File name should match the class name.

Keep related types together.

---

# Method Design

Methods should:

- Have one responsibility.
- Be easy to understand.
- Minimize dependencies.
- Avoid hidden side effects.

Prefer returning early instead of deep nesting.

---

# Exception Handling

Never silently ignore exceptions.

Catch only exceptions that can be handled meaningfully.

Log unexpected failures.

Use custom exceptions only when they add domain value.

---

# Dependency Injection

Prefer constructor injection.

Avoid service locators.

Dependencies should be explicit.

---

# Asynchronous Programming

Use async/await consistently.

Avoid blocking asynchronous code.

Support cancellation where appropriate.

---

# Nullable Reference Types

Nullable reference types shall remain enabled.

Nullability warnings should never be ignored.

---

# Logging

Logging should provide useful operational information.

Avoid logging:

- Secrets
- Credentials
- Personal information

Logs should support troubleshooting.

---

# Testing Standards

Every production feature should include automated tests.

Tests should be:

- Deterministic
- Fast
- Independent
- Readable

Test names should describe behavior.

Example:

```
Should_ReturnBuySignal_WhenMomentumIncreases()
```

---

# Comments

Prefer expressive code over comments.

Comments should explain **why**, not **what**.

Remove outdated comments immediately.

---

# Documentation

Public APIs should include XML documentation when appropriate.

Complex architectural decisions belong in ADRs rather than inline comments.

---

# Formatting

Formatting should be automated.

Developers should not manually debate whitespace or indentation.

The repository configuration (.editorconfig) is the source of truth.

---

# Static Analysis

Compiler warnings should be treated as errors whenever practical.

Code quality tools should run automatically during CI.

---

# Pull Requests

Every pull request should:

- Build successfully.
- Pass all tests.
- Update documentation if required.
- Follow repository standards.
- Remain focused on a single concern.

Large pull requests should be avoided.

---

# Definition of Done

Code is considered complete only when:

- It compiles successfully.
- Tests pass.
- Documentation is updated.
- Code follows these standards.
- No unnecessary technical debt has been introduced.

---

# Continuous Improvement

These standards will evolve as the project grows.

Improvements should be proposed through pull requests and justified using engineering reasoning rather than personal preference.

Consistency across the codebase is more valuable than individual coding style.
