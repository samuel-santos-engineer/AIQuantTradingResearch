# AIQuantTradingResearch — Duplicate Authority File Correction — Codex Authority

## 1. Mission

Perform one extremely narrow corrective action for the blocked Roadmap Reconciliation / Milestone #57 correction workflow in:

`samuel-santos-engineer/AIQuantTradingResearch`

Unexpected untracked file:

`docs/roadmap/release-1.9/prompts/release-roadmap-malformed-milestone-57-correction-codex-prompt-chat (1).md`

Expected canonical file:

`docs/roadmap/release-1.9/prompts/release-roadmap-malformed-milestone-57-correction-codex-prompt-chat.md`

This authority exists to:

1. prove whether the `(1)` file is an accidental duplicate of the canonical chat authority;
2. delete only the `(1)` file if and only if equivalence is proven;
3. preserve both canonical authority files;
4. resume the already-authorized malformed milestone #57 correction without restarting successful roadmap reconciliation work;
5. continue the original roadmap reconciliation workflow only through its existing authorities and gates.

No unrelated cleanup is authorized.

---

## 2. Accepted Preserved State

Preserve the already-verified state:

- branch: `docs/roadmap-release-sequencing`;
- HEAD: `0bffb508d1e5a716214ff3a92a8f8c1da4a44be0`;
- malformed milestone #57 fingerprint already verified:
  - title: `System.Collections.Specialized.OrderedDictionary.title`;
  - description: `System.Collections.Specialized.OrderedDictionary.description`;
  - OPEN;
  - 0 open / 0 closed issues;
  - no Project #2 membership;
- milestone #50 remains Release 2.1 Machine Learning;
- milestone #51 remains Release 2.2 Explainable AI;
- Project #2 Release taxonomy remains exactly 24 options through `2.3`;
- milestone #49 unchanged;
- milestone #56 unchanged;
- no GitHub/repository mutation occurred in the immediately preceding blocked corrective run.

Do not redo successful taxonomy or #50/#51 mutations.

---

## 3. Mandatory Repository Gate

Before mutation verify:

- correct repository and remote;
- branch is `docs/roadmap-release-sequencing`;
- HEAD remains the accepted SHA above unless separately authorized;
- staged paths: 0;
- no merge/rebase/cherry-pick/revert in progress;
- canonical correction authority files are present as expected;
- the unexpected `(1)` file exists at exactly the reported path;
- no additional unexplained tracked/untracked state exists.

If another unexplained path exists, stop.

---

## 4. Canonical Files — Protected

The following canonical files MUST NOT be deleted, renamed, overwritten, or silently replaced:

`docs/roadmap/release-1.9/prompts/release-roadmap-malformed-milestone-57-correction-codex-prompt.md`

`docs/roadmap/release-1.9/prompts/release-roadmap-malformed-milestone-57-correction-codex-prompt-chat.md`

Likewise preserve the canonical original roadmap reconciliation authority pair.

Deletion authority applies only to:

`docs/roadmap/release-1.9/prompts/release-roadmap-malformed-milestone-57-correction-codex-prompt-chat (1).md`

---

## 5. Duplicate Equivalence Proof

Read both chat files completely.

Compare the unexpected `(1)` file against the canonical chat file using both:

### A. Byte-level comparison

Calculate a cryptographic hash such as SHA-256 for both files.

If hashes are equal, byte-for-byte identity is proven.

### B. Normalized-text comparison

Also compare after narrowly normalizing only:

- UTF-8 BOM presence;
- CRLF versus LF line endings;
- one terminal newline.

Do NOT normalize:

- wording;
- whitespace within lines;
- line ordering;
- filenames;
- punctuation;
- references;
- instructions.

Require both files to represent the same exact 5 non-empty instruction lines after permitted normalization.

Report:

- paths;
- sizes;
- SHA-256 values;
- non-empty line counts;
- byte equality;
- normalized-text equality.

---

## 6. Classification Rule

Classify the `(1)` file as:

`ACCIDENTAL_DUPLICATE_EXECUTION_INPUT`

only if either:

1. byte-for-byte equality is proven; or
2. byte inequality is explained solely by the permitted encoding/line-ending/terminal-newline normalization AND normalized semantic text is exactly equal.

If any meaningful difference exists:

- do not delete;
- do not merge content;
- do not choose one version;
- stop.

Use:

`ROADMAP DUPLICATE AUTHORITY FILE CORRECTION BLOCKED`

---

## 7. Secret/Sensitivity Gate

Before deletion, verify neither comparison reveals that the duplicate contains:

- credentials;
- tokens;
- API keys;
- secrets;
- unexpected private machine data;
- additional execution instructions absent from the canonical file.

If unexpected content exists, stop.

Do not print secret values.

---

## 8. Authorized File Mutation

If Sections 5–7 pass:

Delete exactly:

`docs/roadmap/release-1.9/prompts/release-roadmap-malformed-milestone-57-correction-codex-prompt-chat (1).md`

Authorized repository filesystem mutation count at this stage:

- files deleted: exactly 1;
- files modified: 0;
- files renamed: 0;
- files created: 0.

Immediately verify:

- `(1)` file absent;
- canonical chat file still present and unchanged;
- canonical full correction prompt still present and unchanged;
- original roadmap reconciliation authority pair still present and unchanged;
- no other working-tree path changed because of this deletion.

Do not commit the accidental duplicate.

---

## 9. Revalidate Corrective Starting State

After deletion, re-run the narrow starting-state gate required by:

`release-roadmap-malformed-milestone-57-correction-codex-prompt.md`

The accidental duplicate must no longer be a blocker.

If another ambiguity appears, stop.

Do not silently classify another file.

---

## 10. Resume From Already-Proven #57 Gate

The immediately preceding run already proved the #57 fingerprint.

Nevertheless, immediately before destructive GitHub mutation, perform one fresh read-back of #57 to ensure no external change occurred.

Require again:

- #57 exists;
- exact malformed title;
- exact malformed description;
- OPEN;
- 0/0 issues;
- no Project #2 membership.

If unchanged, resume the existing milestone #57 corrective authority at its authorized deletion step.

Do not restart taxonomy reconciliation.

---

## 11. Resume Existing Corrective Authority

After deleting the accidental duplicate and revalidating #57, execution authority returns to:

`release-roadmap-malformed-milestone-57-correction-codex-prompt.md`

That authority governs:

- deletion of malformed #57 only;
- preservation of taxonomy and #50/#51;
- idempotent one-at-a-time creation/read-back of:
  - 1.9 Real-Time Financial Data Visualization;
  - 1.10 OpenTelemetry & Pipeline Observability;
  - 2.0 Lightweight Machine Learning Evaluation;
  - 2.3 Backtesting;
- zero detailed issues/WPs;
- resumption of roadmap documentation;
- dedicated branch commit;
- push;
- PR;
- verification;
- merge under the original roadmap reconciliation merge gate;
- final `main` merge SHA capture.

This authority does not broaden any of those permissions.

---

## 12. Governed Authority Markdown

The accidental `(1)` duplicate must not be committed.

If repository-resident, the canonical governed authority set must be committed by the resumed reconciliation workflow, including:

- original roadmap reconciliation full prompt;
- original roadmap reconciliation chat bootstrap;
- milestone #57 correction full prompt;
- milestone #57 correction chat bootstrap;
- this duplicate-file correction full prompt;
- this duplicate-file correction chat bootstrap.

Do not leave canonical governed authority Markdown untracked after successful reconciliation completion.

---

## 13. No New Scope

This corrective authority does NOT authorize:

- implementation;
- Release 1.9 definition;
- Release 1.9 WPs;
- package/dependency changes;
- schema changes;
- Python environment changes;
- new Project taxonomy changes;
- modification of #50/#51;
- modification of #49/#56;
- issue creation;
- dependency creation;
- arbitrary cleanup;
- deletion of any other duplicate-looking file.

---

## 14. Validation Matrix

Report PASS/FAIL:

- DUP1 — repository/branch/HEAD reconciled;
- DUP2 — only reported duplicate is unexpected;
- DUP3 — canonical correction prompt protected/present;
- DUP4 — canonical correction chat protected/present;
- DUP5 — duplicate and canonical read completely;
- DUP6 — SHA-256/size/line evidence captured;
- DUP7 — byte or narrowly normalized equality proven;
- DUP8 — no meaningful semantic difference;
- DUP9 — no secret/unexpected additional content;
- DUP10 — `(1)` classified ACCIDENTAL_DUPLICATE_EXECUTION_INPUT;
- DUP11 — exactly `(1)` file deleted;
- DUP12 — canonical authority files unchanged;
- DUP13 — no other filesystem mutation from duplicate correction;
- DUP14 — corrective starting state passes after deletion;
- DUP15 — fresh #57 fingerprint revalidation passes;
- DUP16 — #57 correction resumes without taxonomy restart;
- DUP17 — #50/#51/taxonomy/#49/#56 preserved;
- DUP18 — accidental duplicate excluded from commit;
- DUP19 — canonical authority Markdown remains governed for later commit;
- DUP20 — no Release 1.9 implementation or unauthorized scope introduced.

DUP1–DUP20 must PASS before declaring this narrow correction complete.

---

## 15. Stop Conditions

Stop with:

`ROADMAP DUPLICATE AUTHORITY FILE CORRECTION BLOCKED`

if:

- canonical file is missing;
- duplicate is missing unexpectedly before classification;
- another unexplained file exists;
- meaningful content differs;
- duplicate contains additional instructions or sensitive content;
- canonical file would need mutation;
- deletion affects another path;
- #57 fingerprint changed;
- taxonomy/#50/#51 historical state drifted;
- any permission beyond this authority is required.

Report exact blocker and smallest next authority.

---

## 16. Required Report

Report:

### Comparison
- canonical path;
- duplicate path;
- sizes;
- SHA-256 values;
- byte equality;
- normalized equality;
- non-empty line counts.

### Classification
- final duplicate classification;
- reason.

### Mutation
- exact deleted path;
- canonical preservation proof;
- working-tree state afterward.

### Resume Gate
- #57 fresh fingerprint;
- taxonomy preservation;
- #50/#51/#49/#56 preservation.

### DUP1–DUP20
Report every gate.

Then continue under the existing milestone #57 correction and original roadmap reconciliation authorities.

---

## 17. Success Markers

After the duplicate has been safely removed and the existing #57 correction has been successfully resumed, emit:

`ROADMAP DUPLICATE AUTHORITY FILE CORRECTION COMPLETE`

`ACCIDENTAL DUPLICATE AUTHORITY INPUT: REMOVED`

`CANONICAL AUTHORITY FILES: PRESERVED`

`NEXT AUTHORIZED ACTION: Continue the existing malformed milestone #57 correction authority from its deletion gate.`

If the resumed authority continues immediately in the same Codex execution, proceed under that authority rather than stopping merely because this narrow correction succeeded.

If blocked, end exactly:

`ROADMAP DUPLICATE AUTHORITY FILE CORRECTION BLOCKED`
