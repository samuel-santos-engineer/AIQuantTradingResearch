# VALIDATION_TEMPLATE.md

# Validation Template

## Purpose

The Validation Template defines the canonical framework for verifying that engineering artifacts satisfy their specified objectives, acceptance criteria, and engineering standards.

Unlike reviews, which evaluate quality and design, validation objectively confirms that an artifact fulfills its intended engineering contract and is ready to progress through its lifecycle.

Every validation should be evidence-based, repeatable, and independent of the artifact's author.

---

# Objectives

The Validation Template aims to:

* Standardize engineering validation.
* Verify engineering compliance.
* Confirm acceptance criteria.
* Improve engineering reliability.
* Support automated validation.
* Reduce release risk.
* Enable objective decision-making.
* Promote repeatable engineering practices.

Validation determines readiness rather than design quality.

---

# Scope

This template may be used to validate:

* Engineering Playbooks
* AI Prompts
* Documentation
* Source Code
* Configuration Files
* Repository Structure
* Build Assets
* GitHub Assets
* Test Assets
* Architecture Documents
* Operational Assets

The validation process remains consistent regardless of artifact type.

---

# Validation Metadata

Every validation should record:

| Property         | Description                                    |
| ---------------- | ---------------------------------------------- |
| Validation ID    | Unique validation identifier.                  |
| Artifact Name    | Name of the artifact being validated.          |
| Artifact Type    | Document, Prompt, Code, Playbook, etc.         |
| Version          | Version under validation.                      |
| Validator        | Person, team, or AI performing the validation. |
| Validation Date  | Date validation was performed.                 |
| Validation Scope | Full or Partial.                               |
| Status           | Pending, Passed, Failed, Blocked.              |

Metadata provides traceability and auditability.

---

# Validation Objectives

The validation should confirm that the artifact:

* Meets its stated objective.
* Satisfies all acceptance criteria.
* Complies with engineering standards.
* Produces the expected outputs.
* Maintains consistency with related artifacts.
* Is ready for the next lifecycle stage.

Validation focuses on objective verification rather than subjective assessment.

---

# Validation Categories

Each validation should evaluate the following categories.

| Category                | Purpose                                                        |
| ----------------------- | -------------------------------------------------------------- |
| Prerequisites           | Verify required conditions are satisfied before execution.     |
| Structure               | Verify the artifact follows the expected organization.         |
| Completeness            | Verify all required components are present.                    |
| Acceptance Criteria     | Confirm every defined acceptance criterion has been satisfied. |
| Standards Compliance    | Verify adherence to engineering standards and conventions.     |
| Output Verification     | Confirm expected deliverables exist and are correct.           |
| Dependency Verification | Validate required dependencies and references.                 |
| Traceability            | Ensure inputs, outputs, and related artifacts can be traced.   |

Every category should produce observable evidence.

---

# Validation Workflow

```text
Identify Artifact
        │
        ▼
Verify Prerequisites
        │
        ▼
Validate Structure
        │
        ▼
Validate Acceptance Criteria
        │
        ▼
Validate Standards Compliance
        │
        ▼
Verify Deliverables
        │
        ▼
Collect Evidence
        │
        ▼
Determine Result
        │
        ▼
Publish Validation Report
```

Validation should follow the same sequence regardless of artifact type.

---

# Validation Checklist

The validator should confirm:

### General

* Artifact exists.
* Correct version is being validated.
* Required prerequisites are satisfied.
* Repository standards are followed.
* Required references are valid.

### Technical

* Deliverables exist.
* Outputs are complete.
* Naming conventions are respected.
* Dependencies are satisfied.
* Required files are present.

### Compliance

* Acceptance criteria have been met.
* Engineering standards are followed.
* Repository conventions are respected.
* Validation evidence has been recorded.

---

# Evidence Collection

Validation should be supported by objective evidence.

Examples include:

* Repository inspection.
* Build logs.
* Test results.
* Generated artifacts.
* Configuration verification.
* Documentation inspection.
* Automated validation reports.
* Command execution output.

Evidence should be reproducible whenever possible.

---

# Validation Results

Each validation should conclude with one of the following outcomes:

| Result                   | Description                                                                              |
| ------------------------ | ---------------------------------------------------------------------------------------- |
| Passed                   | All validation criteria satisfied.                                                       |
| Passed with Observations | Validation successful with non-blocking observations.                                    |
| Failed                   | One or more required validation criteria were not satisfied.                             |
| Blocked                  | Validation could not be completed due to external dependencies or missing prerequisites. |

The outcome should be supported by documented evidence.

---

# Validation Report

A validation should produce a report similar to:

```text
Artifact

Validation Scope

Validation Summary

Evidence

Acceptance Criteria Status

Compliance Status

Observations

Validation Result

Next Actions
```

Reports should be factual, concise, and reproducible.

---

# Engineering Principles

Validation should be:

* Objective.
* Repeatable.
* Evidence-based.
* Independent.
* Deterministic.
* Traceable.
* Automation-friendly.

Validation should never rely solely on opinion or assumption.

---

# Common Pitfalls

Avoid:

* Validating against undocumented expectations.
* Mixing review comments with validation results.
* Omitting evidence.
* Ignoring failed acceptance criteria.
* Modifying artifacts during validation.
* Producing subjective conclusions.

Validation should measure conformance, not preference.

---

# Acceptance Criteria

The validation process is complete when:

* Every validation category has been evaluated.
* Evidence has been collected.
* Acceptance criteria have been verified.
* Standards compliance has been assessed.
* A validation result has been assigned.
* Next actions have been documented.

Completion indicates the artifact has been objectively assessed.

---

# Automation Considerations

Validation should be designed to support automation whenever practical.

Automated validation may include:

* Repository structure verification.
* Naming convention checks.
* Build validation.
* Test execution.
* Documentation completeness checks.
* Link verification.
* Static analysis.
* Standards compliance verification.

Automation should complement, not replace, engineering judgment.

---

# Dependencies

This template depends on:

* Engineering Standards
* Quality Guidelines
* Naming Conventions
* REVIEW_TEMPLATE.md
* PLAYBOOK_TEMPLATE.md
* PROMPT_TEMPLATE.md

Validation builds upon the engineering framework established by these artifacts.

---

# Version History

Every validation framework should maintain:

* Version number.
* Revision history.
* Author.
* Change summary.

Version history supports governance and continuous improvement.

---

# Success Criteria

The Validation Template is successful when it enables validation processes that:

* Produce objective outcomes.
* Verify engineering contracts.
* Support repeatable execution.
* Integrate with automated workflows.
* Provide traceable evidence.
* Reduce engineering risk.

Success is measured by consistency, confidence, and reproducibility.

---

# Conclusion

The Validation Template establishes the engineering contract for objective verification within the AI Engineering Toolkit.

By defining a consistent validation structure, emphasizing measurable evidence, and separating validation from review, it enables trustworthy engineering decisions throughout the software lifecycle. This approach strengthens governance, supports automation, and ensures that artifacts advance only after demonstrating compliance with their defined engineering contracts.
