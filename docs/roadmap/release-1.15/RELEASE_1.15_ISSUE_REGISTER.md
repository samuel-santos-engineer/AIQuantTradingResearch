# Release 1.15 Work-Package Issue Register

| WP | GitHub issue | Short name | Prerequisites |
| --- | --- | --- | --- |
| WP01 | [#367](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/367) | Baseline/contract/feasibility | None |
| WP02 | [#368](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/368) | Docker dual-process | WP01 |
| WP03 | [#369](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/369) | Ingestion/SQLite/recovery | WP01, WP02 runtime contracts |
| WP04 | [#370](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/370) | Research indicators/regimes | WP03 |
| WP05 | [#371](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/371) | Transformed snapshot API | WP02–WP04 |
| WP06 | [#372](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/372) | Streamlit historical chart | WP05 |
| WP07 | [#373](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/373) | Linux/F1 qualification | WP02–WP06 |
| WP08 | [#374](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/374) | Acceptance/runbook | WP07 |

Issues are implementation-tracking containers, not unconditional authority. Their order follows the Release 1.14 WP convention, with Release 1.15-specific dependencies and scopes. Source of truth for architecture is the merged Release 1.15 governance. No licensing-review milestone. All text output uses UTF-8 LF.
