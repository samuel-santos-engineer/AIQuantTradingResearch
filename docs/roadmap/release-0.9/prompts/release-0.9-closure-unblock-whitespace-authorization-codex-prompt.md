# Codex Authorization Prompt --- Release 0.9 Closure Unblock Whitespace Normalization

## Purpose

Resolve only the authority conflict reported as:

``` text
U09-02 — BLOCKER
Supplied authoritative governance files fail mandatory cached whitespace validation.

U09-03 — BLOCKER
The unblock requires both preserved governance meaning and a passing cached diff.
```

This prompt grants **narrow, explicit authority to remove trailing
whitespace only** from two Release 0.9 governance prompt files.

It does not authorize any semantic, structural, technical, product,
test, architecture, GitHub-governance, or Release 1.0 change.

After applying this authorization, resume the existing:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt.md
```

from the exact staging-validation point where execution stopped.

------------------------------------------------------------------------

# 1. Authorized Files

Whitespace-only normalization is authorized for exactly:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt.md
```

No other file receives new editing authority from this prompt.

The previously authorized `.gitattributes` correction and governance
artifact staging remain governed by the existing closure-unblock prompt.

------------------------------------------------------------------------

# 2. Authorized Transformation

For the two files above, Codex may:

``` text
remove trailing spaces
remove trailing tabs
```

at line ends where required for:

``` text
git diff --check
git diff --cached --check
```

to pass.

The intended known findings are:

``` text
release-0.9-closure-codex-prompt.md
  14 trailing-whitespace findings

release-0.9-closure-unblock-codex-prompt.md
  10 trailing-whitespace findings

known total
  24
```

Treat those counts as previous evidence, not permission to manufacture
exactly 24 edits if current inspection differs. Normalize only actual
trailing whitespace currently present.

------------------------------------------------------------------------

# 3. Semantic Preservation Requirement

The normalization must preserve all substantive content exactly.

Do not change:

``` text
words
characters inside meaningful text
headings
paragraph wording
commands
paths
filenames
SHA values
issue numbers
PR numbers
tables or table meaning
code examples
checklists
authorized scope
prohibited scope
decision criteria
stop conditions
final decisions
execution sequence
governance meaning
```

Markdown table padding at the ends of lines may be removed when it is
only trailing whitespace outside meaningful Markdown content.

Do not reflow paragraphs.

Do not reformat Markdown generally.

Do not reorder content.

Do not "clean up" style.

Do not correct spelling, grammar, terminology, or formatting unrelated
to trailing whitespace.

------------------------------------------------------------------------

# 4. Content-Equivalence Proof

Before editing, capture hashes or another reliable representation of the
two files.

After editing, prove the only textual transformation was removal of
trailing horizontal whitespace.

Use a comparison method that normalizes only line-ending trailing
spaces/tabs and demonstrates semantic equivalence.

At minimum inspect:

``` text
git diff --word-diff=porcelain -- <two authorized files>
git diff --check -- <two authorized files>
```

The diff must show no substantive token/content change.

If any non-whitespace semantic change appears:

``` text
WHITESPACE AUTHORIZATION BLOCKED
```

Restore only the attempted unauthorized semantic edit safely; do not
discard unrelated user work.

------------------------------------------------------------------------

# 5. Required Whitespace Validation

After normalization run:

``` text
git diff --check
git diff --cached --check
```

If the files need to be restaged after normalization, restage only the
already-authorized closure-unblock delta.

Expected:

``` text
working-tree diff check = PASS
cached diff check = PASS
```

Do not add a Git attribute exception.

Do not suppress whitespace diagnostics.

Do not weaken any validation gate.

------------------------------------------------------------------------

# 6. Resume Existing Closure-Unblock Execution

Once whitespace validation passes, this authorization is complete.

Resume:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt.md
```

from the existing staging-validation point.

Do not restart or redesign completed work unnecessarily.

Preserve the already proven correction:

``` text
.gitattributes

before:
  *.cs text diff=csharp

after:
  *.cs text eol=lf diff=csharp
```

Do not change global/system:

``` text
core.autocrlf
```

Continue the existing required sequence:

``` text
exact staged-diff validation
      ↓
corrective commit
      ↓
post-commit validation
      ↓
isolated fresh-checkout proof
      ↓
eng/verify.ps1 PASS
      ↓
41/41 tests PASS
      ↓
canonical Worker PASS
      ↓
push release/0.9-closure-unblock
      ↓
create/reuse corrective PR to main
      ↓
inspect PR/check state
      ↓
STOP BEFORE MERGE
```

------------------------------------------------------------------------

# 7. Existing Scope Remains Binding

This authorization does not expand the closure-unblock scope.

The only blockers being resolved remain:

``` text
C09-01 — repository checkout/format line-ending conflict
C09-02 — Release 0.9 closure governance artifacts not yet integrated
```

Do not modify:

``` text
production behavior
test semantics
architecture
packages
projects
solution membership
Worker behavior
WP13 documentation
GitHub milestone #40
issues #69–#82
labels
project state
tags
GitHub Releases
Release 1.0
```

Do not merge the corrective PR.

------------------------------------------------------------------------

# 8. Required Reporting

In the resumed Release 0.9 Closure Unblock Execution Report, add a
subsection:

``` text
Whitespace Authorization Resolution
```

Report:

``` text
authorization prompt read:
authorized files:
pre-normalization trailing-whitespace findings:
post-normalization trailing-whitespace findings:
semantic/content changes:
git diff --check:
git diff --cached --check:
assessment:
```

Expected semantic/content changes:

``` text
NONE
```

Expected final whitespace findings in the authorized files:

``` text
0
```

Then complete all remaining reporting required by the existing
closure-unblock prompt.

------------------------------------------------------------------------

# 9. Stop Conditions

Stop with:

``` text
WHITESPACE AUTHORIZATION BLOCKED
```

if:

``` text
a substantive content change is required
the whitespace findings occur outside the two authorized files and are not already authorized by the existing unblock
removing trailing whitespace changes meaningful content
the existing staged delta contains an unexplained file
the .gitattributes correction has drifted from the already proven minimum correction
```

Do not broaden this authorization.

------------------------------------------------------------------------

# 10. Completion Criteria

This authorization is successfully consumed when:

``` text
only trailing spaces/tabs were removed from the two authorized prompt files
their substantive content remains equivalent
git diff --check passes
git diff --cached --check passes
the existing closure-unblock execution resumes
```

The final overall unblock decision must still come from the existing
closure-unblock authority:

``` text
RELEASE 0.9 CLOSURE UNBLOCK READY FOR MERGE AUTHORIZATION
RELEASE 0.9 CLOSURE UNBLOCK READY WITH ACTIONS
RELEASE 0.9 CLOSURE UNBLOCK BLOCKED
```

This authorization prompt does not replace that decision model.

------------------------------------------------------------------------

# Final Instruction

Explicitly authorize removal of trailing whitespace only from:

``` text
docs/roadmap/release-0.9/prompts/release-0.9-closure-codex-prompt.md
docs/roadmap/release-0.9/prompts/release-0.9-closure-unblock-codex-prompt.md
```

Preserve every substantive instruction and all semantic content.

Make no other change under this authorization.

Prove both working-tree and cached `git diff --check` pass.

Then resume the existing Release 0.9 closure-unblock execution from
exact staging/commit validation through isolated fresh-checkout proof,
push, and corrective PR creation.

Stop before merge.

> This authorization resolves only the conflict between immutable
> governance meaning and repository whitespace hygiene; it does not
> expand Release 0.9 scope.
