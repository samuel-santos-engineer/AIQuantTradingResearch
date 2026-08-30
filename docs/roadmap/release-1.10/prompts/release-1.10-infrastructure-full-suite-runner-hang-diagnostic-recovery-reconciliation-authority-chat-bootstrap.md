Execute `release-1.10-infrastructure-full-suite-runner-hang-diagnostic-recovery-reconciliation-authority-codex-prompt.md`.

Use **GPT-5.6 Luna**.

Known state:
- canonical base and `origin/main`:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- repaired 103-path publication manifest remains intact.
- staging empty; no commit/push/PR/GitHub mutation.
- focused passes: Application 5/5, Infrastructure 4/4, Architecture 6/6.
- Domain full 11/11 and Application full 136/136 passed.
- initial full Infrastructure/Architecture failures were Windows App Control `0x800711C7`.
- documented local environment was restored by signing only first-party Debug DLL outputs with local `AIQuantTradingDev`.
- subsequent full Infrastructure run did not terminate and left six owned test-runner processes.
- exactly those six owned processes were terminated; no unrelated process was touched.

Diagnose the full Infrastructure hang with bounded, ownership-safe reproduction and isolation. Classify whether it is product, test, runner/host lifecycle, environment/App Control, external toolchain, or another exact cause. Freeze exact timeout/retry/cleanup/recovery rules and require a terminal full Infrastructure PASS before publication.

Repository content changes are forbidden except the Release 1.10 execution plan, and manifest only if needed to classify this diagnostic authority pair. Production/tests/packages/schema/signing configuration remain untouched.

Git mutations: ZERO.
GitHub mutations: ZERO.

If no implementation change is required, produce a deterministic GPT-5.6 Terra publication-resumption handoff. If a product/test change is required, freeze the minimum implementation authority instead.

End only with the exact COMPLETE or BLOCKED terminal.
