
# REVIEW_TEMPLATE.md

# Review Template

## Purpose

The Review Template defines the standard process for evaluating engineering artifacts produced within the AI Engineering Toolkit.

It provides a consistent framework for assessing quality, completeness, compliance, maintainability, and readiness before an artifact is approved for publication or implementation.

Reviews are intended to improve engineering quality through objective evaluation rather than subjective opinion.

---

# Objectives

The Review Template aims to:

* Standardize engineering reviews.
* Improve artifact quality.
* Detect inconsistencies early.
* Verify compliance with engineering standards.
* Reduce technical debt.
* Improve maintainability.
* Support collaborative engineering.
* Enable repeatable review processes.

---

# Scope

This template may be used to review:

* Documentation
* Engineering Playbooks
* AI Prompts
* Source Code
* Configuration Files
* Build Assets
* Repository Structure
* Architecture Documents
* Design Documents
* Test Assets
* Operational Documentation

The same review principles apply regardless of artifact type.

---

# Review Metadata

Every review should record:

| Property      | Description                                           |
| ------------- | ----------------------------------------------------- |
| Review ID     | Unique review identifier.                             |
| Artifact Name | Name of the artifact under review.                    |
| Artifact Type | Document, Prompt, Code, Playbook, etc.                |
| Version       | Reviewed version.                                     |
| Reviewer      | Person or AI performing the review.                   |
| Review Date   | Date of the review.                                   |
| Status        | Draft, In Review, Approved, Rejected, Needs Revision. |

Review metadata provides traceability throughout the artifact lifecycle.

---

# Review Objectives

The review should determine whether the artifact:

* Meets its stated purpose.
* Satisfies engineering standards.
* Is complete.
* Is internally consistent.
* Is maintainable.
* Is understandable.
* Is suitable for long-term evolution.

---

# Review Categories

Each review should evaluate the following categories.

| Category        | Purpose                                                           |
| --------------- | ----------------------------------------------------------------- |
| Completeness    | Verify all required sections and deliverables are present.        |
| Correctness     | Verify technical accuracy and engineering validity.               |
| Consistency     | Verify alignment with repository standards and related artifacts. |
| Clarity         | Ensure the artifact is understandable and unambiguous.            |
| Maintainability | Assess ease of future updates and evolution.                      |
| Reusability     | Determine whether the artifact can be reused effectively.         |
| Traceability    | Verify references, dependencies, and relationships.               |
| Compliance      | Verify adherence to engineering policies and standards.           |

Each category should be evaluated independently.

---

# Review Workflow

```text
Receive Artifact
        │
        ▼
Understand Purpose
        │
        ▼
Evaluate Structure
        │
        ▼
Review Content
        │
        ▼
Identify Findings
        │
        ▼
Document Recommendations
        │
        ▼
Determine Outcome
```

The review process should be systematic and evidence-based.

---

# Review Checklist

The reviewer should verify:

### General

* Purpose is clearly defined.
* Scope is appropriate.
* Artifact follows repository standards.
* Naming conventions are respected.
* Structure is complete.

### Technical

* Information is technically correct.
* References are valid.
* Dependencies are identified.
* Assumptions are documented.
* Engineering decisions are justified.

### Quality

* Content is clear.
* Duplication is minimized.
* Terminology is consistent.
* Organization is logical.
* Long-term maintenance is supported.

---

# Findings Classification

Review findings should be categorized as:

| Severity   | Description                                             |
| ---------- | ------------------------------------------------------- |
| Critical   | Must be resolved before approval.                       |
| Major      | Significant issue affecting quality or maintainability. |
| Minor      | Improvement recommended but not blocking.               |
| Suggestion | Optional enhancement for future consideration.          |

Severity should reflect engineering impact rather than personal preference.

---

# Review Outcome

Each review concludes with one of the following outcomes:

* Approved
* Approved with Recommendations
* Needs Revision
* Rejected

The outcome should be supported by documented findings.

---

# Recommendations

Recommendations should:

* Be actionable.
* Explain the reason for the recommendation.
* Reference applicable standards where possible.
* Avoid subjective language.
* Focus on improving engineering quality.

Recommendations should facilitate improvement rather than merely identify issues.

---

# Engineering Principles

Reviews should be:

* Objective.
* Constructive.
* Evidence-based.
* Repeatable.
* Respectful.
* Focused on the artifact rather than its author.

The goal of a review is continuous improvement.

---

# Common Pitfalls

Avoid:

* Personal preferences presented as standards.
* Inconsistent review criteria.
* Undocumented review decisions.
* Ignoring engineering standards.
* Reviewing implementation instead of stated objectives.
* Providing vague recommendations.

A high-quality review should produce clear and actionable outcomes.

---

# Review Report

A review should produce a report similar to:

```text
Artifact

Status

Summary

Strengths

Findings

Recommendations

Review Outcome

Next Actions
```

Reports should be concise while providing sufficient detail for corrective actions.

---

# Acceptance Criteria

The review process is complete when:

* All review categories have been evaluated.
* Findings are documented.
* Recommendations are actionable.
* An outcome has been assigned.
* Review metadata is complete.

Completion indicates that the artifact has been evaluated consistently.

---

# References

Applicable repository assets include:

* Engineering Standards
* Quality Guidelines
* Playbook Template
* Prompt Template
* Validation Template
* Naming Conventions
* Repository Governance

These references ensure consistency across reviews.

---

# Version History

Every review framework should maintain:

* Version number.
* Change history.
* Author.
* Revision summary.

Version history supports continuous improvement of the review process.

---

# Conclusion

The Review Template establishes a standardized methodology for evaluating engineering artifacts within the AI Engineering Toolkit.

By separating artifact creation from artifact evaluation, it promotes objective quality assessment, consistent engineering practices, and continuous improvement. This framework enables teams and AI assistants to perform structured, repeatable reviews that improve clarity, maintainability, and compliance while supporting the long-term evolution of software projects.
