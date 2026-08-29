Execute `release-1.10-wp03-environment-unblock-validation-authority-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — contract/governance/architecture.
- **GPT-5.6 Terra** — execute this narrow environment-unblock and validation authority.
- **GPT-5.6 Sol** — supporting analysis only.

Use **GPT-5.6 Terra**.

Purpose: resolve only the Windows Application Control / Smart App Control block preventing `AIQuantTradingResearch.Worker.dll` from loading (`0x800711C7`) during full Infrastructure validation.

Focused WP03 listener tests already pass 25/25.

Use only the already-approved local development signing/execution mechanism. Prefer the existing documented repo/local workflow. Do not invent a new tracked signing design.

Allowed: local dev signing/trust/rebuild actions required to make the Worker assembly loadable.

Forbidden:
- application/WP03 source or test edits;
- `.csproj`/package/schema/migration changes;
- disabling/weaking Smart App Control/App Control;
- committing/exposing certificates/private keys;
- Git mutations;
- GitHub mutations;
- closing #244;
- starting WP04.

After restoring the local Worker execution path:
1. prove the assembly load no longer fails with `0x800711C7`;
2. rerun the previously blocked full Infrastructure validation;
3. rerun focused WP03 listener tests;
4. run minimum Application/architecture regression checks;
5. prove zero tracked contract mutations and zero Git/GitHub mutations;
6. hand back to the SAME WP03 V2 authority.

If unblocking reveals genuine WP03 test failures, do not repair WP03 here; report them and hand back.

#244 remains Open/Backlog throughout this authority.

End only with the exact environment-unblock COMPLETE or BLOCKED terminal marker.
