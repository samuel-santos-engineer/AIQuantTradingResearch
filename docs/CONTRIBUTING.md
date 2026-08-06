
# Contributing to AIQuantTradingResearch

**Status:** Active
**Version:** 1.0
**Last Updated:** 2026-08-06
**Maintainers:** AIQuantTradingResearch Team

---

First of all, thank you for considering contributing to AIQuantTradingResearch.

Whether you are fixing a typo, improving documentation, reporting a bug, or implementing a new feature, your contribution is appreciated.

This project aims to demonstrate modern software engineering practices while fostering a collaborative and respectful open-source community.

---

# Code of Conduct

By participating in this project, you agree to interact respectfully with all contributors.

Constructive discussions, technical curiosity, and continuous learning are encouraged.

Personal attacks, harassment, and disrespectful behavior will not be tolerated.

---

# Before You Start

Please take a few minutes to read the following documents before contributing:

- PROJECT_CONSTITUTION.md
- ENGINEERING.md
- ARCHITECTURE.md
- CODING_STANDARDS.md
- ROADMAP.md

Understanding the project's engineering philosophy will help ensure your contribution aligns with its long-term vision.

---

# Ways to Contribute

You can contribute by:

- Reporting bugs
- Suggesting new features
- Improving documentation
- Refactoring existing code
- Writing automated tests
- Improving CI/CD
- Enhancing observability
- Reviewing pull requests
- Improving performance
- Correcting spelling or grammar

No contribution is too small.

---

# Development Workflow

The recommended contribution workflow is:

1. Fork the repository.
2. Create a feature branch.
3. Implement your changes.
4. Add or update automated tests.
5. Update documentation when necessary.
6. Verify that the solution builds successfully.
7. Submit a Pull Request.

---

# Branch Naming

Use descriptive branch names.

Examples:

```
feature/binance-market-data

feature/backtesting-engine

feature/lightgbm-model

bugfix/fix-websocket-reconnect

docs/update-architecture
```

---

# Commit Messages

Commit messages should follow the Conventional Commits specification.

Examples:

```
feat: add Binance market data provider

fix: handle websocket reconnection

docs: update architecture overview

refactor: simplify prediction pipeline

test: add market data integration tests

chore: update dependencies
```

Keep commits focused on a single logical change.

---

# Pull Requests

A Pull Request should:

- Address one concern.
- Build successfully.
- Pass all automated tests.
- Include documentation updates when applicable.
- Explain the motivation behind the change.
- Be small enough for effective review.

Avoid combining unrelated changes into a single Pull Request.

---

# Coding Standards

All contributions must follow the repository's coding standards.

Please review:

- CODING_STANDARDS.md

Formatting should be handled automatically through the project's tooling.

---

# Testing

Every production feature should include appropriate automated tests.

Whenever practical, include:

- Unit tests
- Integration tests

Bug fixes should include regression tests when possible.

---

# Documentation

Documentation is considered part of the implementation.

If your contribution changes behavior, architecture, APIs, or workflows, update the relevant documentation.

---

# Architecture Decisions

Significant architectural changes should be documented through an Architecture Decision Record (ADR).

An ADR should describe:

- Context
- Problem
- Alternatives considered
- Decision
- Consequences

---

# Quality Checklist

Before submitting a Pull Request, verify the following:

- The solution builds successfully.
- Tests pass.
- Documentation is updated.
- No compiler warnings have been introduced.
- Code follows the project's coding standards.
- No secrets or credentials are included.

---

# Asking Questions

Questions and discussions are welcome.

If you are unsure about an implementation or architectural decision, open a GitHub Discussion or Issue before investing significant development effort.

Early communication helps prevent duplicated work.

---

# Recognition

Every contributor helps improve AIQuantTradingResearch.

Contributions are valued based on their quality, clarity, and positive impact—not on their size.

Thank you for helping make this project better.
