Resume Release 1.4 WP06/#158 under the original WP06 authority plus this identity clarification.
The clarification assigns WP06 the minimum Application-owned canonical computation of `FeatureDefinitionIdentity` and `FeatureSetIdentity` required for the accepted WP04 `FeatureSet`.
Implement `aiq-feature-identity-v1` exactly as frozen by WP03 with `simple-return-lag-1-v1`; do not redesign identity semantics or accepted WP04/WP05 contracts.
Keep lookup/orchestration, generalized validation, persistence/schema, DI, Worker, tests, WP07/#159, and Release 1.5 work out of scope.
Run every original WP06 gate and clarified identity check; close #158 only after success and end with `RELEASE 1.4 WP06 COMPLETE`.
