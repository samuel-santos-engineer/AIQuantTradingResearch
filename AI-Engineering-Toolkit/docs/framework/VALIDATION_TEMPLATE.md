
# VALIDATION_TEMPLATE.md

# Engineering Validation Template

## Purpose

This document defines the canonical structure for Engineering Validation Playbooks within the AI Engineering Toolkit.

An Engineering Validation Playbook verifies that generated engineering artifacts satisfy their documented requirements, acceptance criteria, and applicable engineering standards. Validation provides objective evidence that an artifact is suitable for its intended purpose.

Unlike Engineering Review Playbooks, which evaluate quality and recommend improvements, Validation Playbooks determine whether defined engineering objectives have been successfully achieved.

---

# Validation Principles

Engineering validation should follow these principles:

* Objectivity
* Repeatability
* Traceability
* Evidence-based decisions
* Standards compliance
* Automation where practical
* Reproducibility
* Simplicity

Validation conclusions should always be supported by verifiable evidence.

---

# Validation Workflow

Every Engineering Validation Playbook follows the same lifecycle.

```text
Engineering Artifact
        │
        ▼
Validation Scope
        │
        ▼
Validation Criteria
        │
        ▼
Validation Execution
        │
        ▼
Evidence Collection
        │
        ▼
Validation Results
        │
        ▼
Validation Report
```

---

# Engineering Validation Structure

Every Engineering Validation Playbook should follow the structure below.

```text
Metadata
│
├── Validation Objective
├── Scope
├── Context
├── Validation Inputs
├── Validation Criteria
├── Standards
├── Validation Procedure
├── Evidence Collection
├── Validation Results
├── Validation Deliverables
├── Acceptance Criteria
├── Validation Checklist
├── References
└── Version History
```

---

# Validation Objective

Defines what the validation is intended to confirm.

Examples include:

* Repository bootstrap validation
* Documentation validation
* Project structure validation
* Build validation
* Architecture compliance validation
* Playbook execution validation

The objective should be specific and measurable.

---

# Scope

Defines the boundaries of the validation activity.

Examples:

* Entire repository
* Single engineering artifact
* Folder structure
* Documentation set
* Build process
* Generated source code
* Infrastructure configuration

Artifacts outside the defined scope should not influence the validation outcome.

---

# Context

Provides engineering information required to understand the validation scenario.

Context may include:

* Repository architecture
* Implementation assumptions
* Applicable standards
* Supported technologies
* Environmental constraints

---

# Validation Inputs

Lists the artifacts required to perform validation.

Examples include:

* Source code
* Documentation
* Scripts
* Configuration files
* Build outputs
* Test results
* Architecture assets

Inputs should be clearly identified before validation begins.

---

# Validation Criteria

Validation criteria define what must be verified.

Typical criteria include:

* Functional completeness
* Structural correctness
* Standards compliance
* Naming consistency
* Documentation completeness
* Repository organization
* Build success
* Configuration correctness

Criteria should be measurable whenever possible.

---

# Standards

Validation should reference applicable repository standards rather than duplicate them.

Examples include:

* QUALITY_GUIDELINES.md
* NAMING_CONVENTIONS.md
* PROMPT_METADATA.md
* PLAYBOOK_TEMPLATE.md

Standards define the baseline for successful validation.

---

# Validation Procedure

Validation should follow a repeatable process.

Recommended workflow:

1. Confirm validation scope.
2. Collect required inputs.
3. Verify applicable standards.
4. Execute validation activities.
5. Record objective evidence.
6. Compare results against acceptance criteria.
7. Document conclusions.
8. Publish the validation report.

The procedure should be deterministic and reproducible.

---

# Evidence Collection

Validation evidence should be objective and verifiable.

Examples include:

* Generated artifacts
* Build logs
* Test results
* Repository structure
* Screenshots
* Command output
* Validation checklists
* Generated reports

Evidence should support every validation conclusion.

---

# Validation Results

Each validation criterion should receive one of the following outcomes.

| Result                 | Description                                       |
| ---------------------- | ------------------------------------------------- |
| Pass                   | Criterion fully satisfied.                        |
| Pass with Observations | Criterion satisfied with minor recommendations.   |
| Fail                   | Criterion not satisfied.                          |
| Not Applicable         | Criterion does not apply to the validation scope. |

Results should remain factual and evidence-based.

---

# Validation Deliverables

Every validation should produce a structured Validation Report.

Typical contents include:

* Executive Summary
* Validation Scope
* Standards Applied
* Validation Criteria
* Evidence Summary
* Validation Results
* Outstanding Issues
* Overall Outcome

The report should provide a complete audit trail of the validation process.

---

# Acceptance Criteria

Validation is considered complete when:

* Scope has been fully evaluated.
* Validation criteria have been assessed.
* Required evidence has been collected.
* Results have been documented.
* Applicable standards have been verified.
* The overall outcome has been recorded.

Acceptance criteria determine the completion of the validation activity itself.

---

# Validation Checklist

Before closing a validation activity, confirm that:

* The objective is clearly defined.
* Scope is complete.
* Inputs are available.
* Validation criteria are measurable.
* Standards have been applied.
* Evidence supports every result.
* Results are documented.
* Outstanding issues are identified.
* Validation report is complete.

---

# Validation Quality

A high-quality validation process should be:

* Objective
* Repeatable
* Traceable
* Complete
* Evidence-based
* Transparent
* Easy to reproduce

Validation should build confidence in engineering outcomes rather than rely on subjective judgment.

---

# Common Validation Pitfalls

Avoid:

* Validating undefined requirements.
* Missing or incomplete evidence.
* Ambiguous validation criteria.
* Mixing review findings with validation results.
* Ignoring repository standards.
* Declaring success without objective proof.

These practices reduce the reliability of validation activities.

---

# References

Engineering Validation Playbooks should reference:

* Repository standards
* Engineering Playbooks
* Framework templates
* Review reports
* Reference implementations
* Architectural documentation

References ensure consistency and traceability across engineering activities.

---

# Version History

Record significant updates to the validation methodology, structure, or execution process.

Version history supports governance and long-term maintainability.

---

# Conclusion

The Engineering Validation Template establishes a consistent methodology for verifying engineering artifacts within the AI Engineering Toolkit.

By emphasizing objective evidence, repeatable procedures, measurable criteria, and traceable outcomes, Engineering Validation Playbooks provide confidence that generated artifacts satisfy their intended purpose and comply with the engineering standards that govern the toolkit.
