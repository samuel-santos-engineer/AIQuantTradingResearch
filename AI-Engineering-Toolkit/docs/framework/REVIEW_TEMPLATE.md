
# REVIEW_TEMPLATE.md

# Engineering Review Template

## Purpose

This document defines the canonical structure for Engineering Review Playbooks within the AI Engineering Toolkit.

An Engineering Review Playbook evaluates existing engineering artifacts against established standards, architectural principles, engineering practices, and quality expectations. Unlike Generation Playbooks, Review Playbooks do not create new assets; they assess existing ones and produce objective findings and recommendations.

Every Engineering Review Playbook should promote consistency, traceability, and actionable engineering feedback.

---

# Review Principles

Engineering reviews should follow these principles:

* Objectivity
* Evidence-based evaluation
* Consistency
* Traceability
* Constructive feedback
* Technology independence
* Standards compliance
* Continuous improvement

Reviews should identify opportunities for improvement without prescribing unnecessary changes.

---

# Review Workflow

Every Engineering Review Playbook follows the same lifecycle.

```text
Engineering Artifact
        │
        ▼
Context Analysis
        │
        ▼
Standards Evaluation
        │
        ▼
Quality Assessment
        │
        ▼
Findings
        │
        ▼
Recommendations
        │
        ▼
Review Report
```

---

# Engineering Review Structure

Every Engineering Review Playbook should follow the structure below.

```text
Metadata
│
├── Review Objective
├── Scope
├── Context
├── Review Inputs
├── Evaluation Criteria
├── Standards
├── Review Procedure
├── Findings Classification
├── Recommendations
├── Review Deliverables
├── Acceptance Criteria
├── Review Checklist
├── References
└── Version History
```

---

# Review Objective

Defines the engineering purpose of the review.

Examples:

* Architecture Review
* Code Review
* Documentation Review
* Security Review
* Testing Review
* PowerShell Review
* DevOps Review

The objective should clearly identify what is being evaluated.

---

# Scope

Defines the review boundaries.

Examples:

* Entire repository
* Single document
* Source code
* PowerShell scripts
* GitHub workflows
* Pull request
* Architecture assets

Items outside the defined scope should not influence the review.

---

# Context

Provides engineering background required to understand the artifact.

Context may include:

* Repository architecture
* Applicable standards
* Technology stack
* Business constraints
* Previous architectural decisions

---

# Review Inputs

Identifies the engineering artifacts being reviewed.

Examples:

* Source files
* Documentation
* Configuration files
* Repository structure
* Architecture diagrams
* Scripts

Inputs should be explicitly identified.

---

# Evaluation Criteria

Review criteria define how quality is assessed.

Typical criteria include:

* Correctness
* Completeness
* Maintainability
* Readability
* Reusability
* Security
* Performance
* Consistency
* Standards compliance

Criteria should be measurable whenever practical.

---

# Standards

Engineering reviews should reference applicable standards rather than duplicate them.

Examples:

* QUALITY_GUIDELINES.md
* NAMING_CONVENTIONS.md
* PROMPT_METADATA.md
* PROMPT_ARCHITECTURE.md

Standards establish the baseline for evaluation.

---

# Review Procedure

Reviews should follow a consistent process.

Typical workflow:

1. Understand the objective.
2. Identify review scope.
3. Examine engineering artifacts.
4. Compare against standards.
5. Record findings.
6. Classify findings.
7. Produce recommendations.
8. Generate the review report.

The procedure should remain repeatable across projects.

---

# Findings Classification

Review findings should be categorized by impact.

| Severity    | Description                                                                       |
| ----------- | --------------------------------------------------------------------------------- |
| Critical    | Prevents successful implementation or introduces significant engineering risk.    |
| Major       | Requires correction before approval but does not invalidate the overall solution. |
| Minor       | Improvement recommended but not mandatory for acceptance.                         |
| Observation | Informational suggestion or engineering insight.                                  |

Classification should remain objective and evidence-based.

---

# Recommendations

Recommendations should:

* Explain the issue.
* Describe why it matters.
* Propose a practical improvement.
* Reference applicable standards when relevant.

Recommendations should avoid subjective opinions whenever possible.

---

# Review Deliverables

Every review should produce a structured Engineering Review Report.

Typical contents include:

* Executive Summary
* Scope
* Standards Applied
* Findings
* Recommendations
* Overall Assessment
* Review Outcome

Deliverables should be concise, actionable, and reproducible.

---

# Acceptance Criteria

A completed review should satisfy the following:

* Scope fully evaluated.
* Standards consistently applied.
* Findings classified.
* Recommendations documented.
* Evidence provided.
* Review report completed.

Acceptance criteria define when the review itself is considered complete.

---

# Review Checklist

Before publishing a review, verify that:

* Review objective is clear.
* Scope is complete.
* Context is sufficient.
* Evaluation criteria are documented.
* Applicable standards are referenced.
* Findings are evidence-based.
* Severity classifications are consistent.
* Recommendations are actionable.
* Deliverables are complete.

---

# Review Quality

Engineering reviews should be:

* Consistent
* Fair
* Objective
* Actionable
* Traceable
* Reproducible

A review should help engineers improve quality rather than merely identify defects.

---

# Common Review Pitfalls

Avoid:

* Personal opinions presented as facts.
* Vague recommendations.
* Missing evidence.
* Inconsistent severity classifications.
* Ignoring repository standards.
* Reviewing outside the defined scope.

Maintaining discipline in the review process improves confidence in the results.

---

# References

Engineering Review Playbooks should reference:

* Repository standards
* Engineering Playbooks
* Framework templates
* Reference implementations
* Architectural documentation

References strengthen traceability and consistency.

---

# Version History

Document significant structural or procedural changes made to the review playbook.

Version history supports governance and long-term maintenance.

---

# Conclusion

The Engineering Review Template establishes a consistent methodology for evaluating engineering artifacts within the AI Engineering Toolkit.

By standardizing review objectives, evaluation criteria, findings classification, recommendations, and reporting, Engineering Review Playbooks promote objective quality assessment, continuous improvement, and engineering excellence across repositories, technologies, and AI-assisted development workflows.
