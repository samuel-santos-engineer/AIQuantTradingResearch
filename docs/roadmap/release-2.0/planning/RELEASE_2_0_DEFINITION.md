# Release 2.0 ? Lightweight Machine Learning Evaluation

Status: planning proposal, 2026-10-17. No model or implementation selected or authorized. See [discovery](DISCOVERY_RECORD.md), [dependencies/allowlists](WORK_PACKAGES.md), and [qualification design](QUALIFICATION_PLAN.md).

## Milestone purpose and scope

Actual milestone60 title: **Phase 5 - Release 2.0: Lightweight Machine Learning Evaluation**. It is open, has no due date and zero linked issues. Its description prefers one narrow deterministic hypothesis, temporal evaluation, reproducible metrics and baseline comparison; governed scikit-learn Logistic Regression is the initial direction unless definition finds a blocker. This is not a full ML platform or backtesting release.

Proposed bounded delivery: offline evaluation of one approved candidate on the existing synthetic simulation, with an immutable educational results panel. Preserve [synthetic-only governance](../../../governance/SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md), [Release1.14 closeout](../../release-1.14/RELEASE_1.14_MILESTONE_65_CLOSEOUT.md) and [original publication evidence](../../release-1.14/prompters/release-1.14-c2b-37-production-publication-evidence.md). Existing historical statements remain untouched.

Linux/amd64 Docker is the sole certification target; Azure development F1 only. A workstation is a Docker host, not a native test target. No production mutation/deployment, new dependency/service/spend, real feed, new instrument, trading recommendation, backtest, online inference or rolling dataset. Preserve PUBLIC_RUNTIME_VERIFIED=False, raw API restrictions and advanced WP07 ON HOLD. Preserve all12 chart selections, six C# indicator families, controls, synthetic labeling and System Health.

## Candidate comparison ? not a selection

| Method | Value | Risk/cost | Proposal |
|---|---|---|---|
| Training-class-prior and past-direction baselines |Expose imbalance/trivial predictability|Small, causal, train-only class statistics|Mandatory comparators|
| Logistic Regression + train-only StandardScaler |Interpretable bounded binary experiment; milestone preference|Convergence and floating-library variability|Preferred candidate for owner decision|
| Depth-limited decision tree |Simple nonlinear alternative|Overfits one generator; extra selection risk|Alternative only, no automatic model tournament|
| SGD logistic classifier |Small iterative model|Learning-rate/order choices without demonstrated need|Defer|
| Repeated expanding-window evaluation |Sensitivity across time segments|More fits and tuning opportunities|Alternative to single holdout, not extra automatic search|

No fitting occurs now. Owner must select exactly one candidate and fixed configuration. Conditional Logistic Regression proposal: lbfgs, C=1, max_iter=200, tol=1e-8, fixed seed114 where relevant; no parallel search. A convergence warning fails the fit, rather than silently increasing iterations or switching models.

## Proposed experiment and leakage boundaries

Question: does one bounded feature vector describe next simulated candle direction better than trivial baselines on a frozen holdout? This tests one generator/window, never real-market predictability.

Use the existing pinned candidate-v4 1h/90D artifact:2160 visible candles, UTC[2026-07-03,2026-10-01), with existing C# indicators calculated over250 hidden warmup rows. Do not export warmup or recompute indicators in Python. Other chart selectors retain their own values; the results panel always identifies this fixed experiment, not12 separate training runs.

Proposed label at bar t:1 if close(t+1)>close(t), otherwise0, including ties. Features become available at the close of t, not its displayed opening timestamp; label availability is the close of t+1. Propose at most8 named existing C# numeric fields, exact columns/units approved in WP01. Python may parse, form labels and perform approved ML preprocessing; it cannot replace C# indicator calculations. No post-test feature selection.

| Split | Half-open UTC candle-start bounds | Candles | Eligible labels |
|---|---|---|---|
| Train |2026-07-03 to2026-08-26|1296|1295|
| Validation |2026-08-26 to2026-09-13|432|431|
| Final test |2026-09-13 to2026-10-01|432|431|

Each target candle must be wholly inside its split: remove the last feature row of each split. Target close can equal the split boundary, never exceed it. Independently verify timestamps/counts. No shuffle, centered windows, backward fill, future-derived imputation, cross-boundary labels, global normalization or test-driven threshold/feature selection. Reject missing/nonfinite input rather than silently impute. Freeze baseline tie rules and train-only class priors.

Fit on train; assess validation with frozen choices; evaluate locked test once. Reopening test requires a new owner-reviewed experiment revision. Poor/negative results are valid: no accuracy target or requirement to beat a baseline is a delivery gate. Report class support, confusion matrix, accuracy/balanced accuracy; probability estimators also log loss/Brier. Optional ROC-AUC requires both evaluation classes. One-class training is invalid; undefined evaluation metrics are explicit null plus reason, never NaN/invented zero. WP01 must freeze these conventions.

## Proposed architecture/reproducibility

An isolated Linux build stage reads checksum-validated existing C# assets and writes a bounded versioned JSON report with network disabled during evaluation. Startup/requests only validate/read it. No runtime fit, background task, extra process/service, database, serialized pickle/joblib or downloadable weights/raw arrays. Invalid report hides ML results with an unavailable message while preserving the chart; never fall back to private data.

Record source hashes, feature/label/split versions, UTC counts, seed, parameters, source commit, exact dependency pins/architecture and report hash. Exclude local usernames, secrets, host paths and environment dumps. Keep timing/build timestamps outside deterministic bytes. Propose exact split/label/prediction agreement for pinned Linux reruns, metric tolerance1e-10 and canonical6-decimal report metrics; owner approval required before execution. Cross-version/platform bitwise equality is not promised.

UI wording: ?Synthetic simulation evaluation ? not real market data or trading advice?. Show baselines, fixed window and limitations honestly. Explain synthetic temporal correlation/generator structure, small sample and non-independent real-market generalization. A strong score conveys no investment evidence.

## Owner decisions before implementation

1. Approve fixed1h/90D hypothesis, label timing/purge and single-report scope.
2. Select candidate, exact feature list, parameters, metrics, one-class rules and tolerances.
3. Approve immutable report/UI contract and literal path proposals.
4. Approve measured resource budgets and full combination-matrix schedule on F1.
5. Grant scoped phase authority; registry/development deployment requires separate explicit authority. No production phase is proposed.
