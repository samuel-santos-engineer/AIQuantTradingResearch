# Repository-wide LF line-ending policy

**Status:** Proposed project-wide governance amendment; effective on merge.
**Scope:** All project releases, branches, contributors, ChatGPT, Codex, automation and generated or modified text files.

## Mandatory rules

1. Every new or modified repository text file MUST use UTF-8 and LF (`\n`) line endings. CRLF (`\r\n`) and mixed line endings MUST NOT be introduced, including in PowerShell scripts (`.ps1`, `.psm1`).
2. Follow the root `.gitattributes`: text files use LF in the Git working tree and repository. Binary assets are exempt. Do not change binary content to satisfy a text policy.
3. All ChatGPT-authored plans, governance amendments, Codex prompts and implementation instructions MUST explicitly require LF-normalized text output.
4. Codex and other contributors MUST check changed text files for CRLF before committing and include `git diff --check` in validation. `git diff --check` alone does not prove LF compliance; perform a separate CRLF check.
5. Keep normalization limited to the authorized files. Do not perform an unrelated repository-wide reformat or silently alter historical artifacts.
6. If a third-party tool requires another working-tree encoding or line ending, document the exception and obtain a separately reviewed governance change rather than silently overriding this policy.

## Practical verification

For staged files, inspect the Git attributes with `git check-attr text eol -- <path>`, and verify staged text blobs contain no carriage-return bytes used as CRLF. For a local checkout, `git ls-files --eol` helps detect working-tree and index differences. Correct any changed text files before opening or updating a pull request.

This policy applies beyond Release 1.15 and does not authorize unrelated code, release or deployment changes.
