# Research Domain Model

## 1. Purpose

This document defines the smallest domain and ownership boundaries needed for
the first executable Release 0.9 research workflow. It is implementation
authority for WP03-WP12, but it is not source-code design and does not authorize
capabilities beyond the Release 0.9 execution plan.

The model answers one bounded question using deterministic, offline input:

> What is the arithmetic mean of an approved window of timestamped price
> observations?

This operation is intentionally simple. Its purpose is to prove meaningful
domain behavior through the existing Domain, Application, Infrastructure, and
Worker boundaries, not to claim analytical or market-performance value.

## 2. Release 0.9 Context

Release 0.8 delivered an eight-project solution skeleton with an acyclic
production graph. Release 0.9 must evolve that skeleton into one executable
vertical research capability while remaining offline, deterministic, and free
of real providers, persistence, plugins, AI/ML, cloud services, and trading or
backtesting behavior.

The authoritative dependency direction remains:

```text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application, Infrastructure
```

No new project or project reference is required by this discovery.

## 3. Discovery Method

Discovery proceeded from behavior rather than anticipated C# types:

1. Identify the smallest quantitative question that produces a verifiable
   result.
2. Remove every input not required to answer that question.
3. Assign pure meaning and invariants to Domain.
4. Assign use-case selection, orchestration, and external needs to Application.
5. Restrict Infrastructure to a deterministic implementation of an
   Application-owned need.
6. Restrict Worker to composition and reference execution.
7. Reject or defer concepts introduced only for future providers, persistence,
   trading, analytics frameworks, or AI/ML.

Existing business and data documents supplied vocabulary candidates, but most
describe planned long-term capabilities. Release 0.9 adopts only the minimum
source-independent meaning required below.

## 4. Reference Research Operation

The reference operation calculates the arithmetic mean of a requested number
of price observations for an Application-selected research target.

It answers:

> Across the deterministic observation window supplied for this request, what
> is the mean observed price?

The operation requires:

- an opaque, non-blank research-target key used by Application to request data;
- a positive requested observation count;
- exactly that many valid price observations from the observation boundary;
- Domain validation of each observation and the complete ordered series;
- Domain calculation of the arithmetic mean;
- an Application result that associates the target and count with the Domain
  outcome.

The operation is complete when Application either returns that result or
returns one of the explicitly defined expected failures. It does not acquire
real market data, analyze returns, create a signal, or evaluate a strategy.

## 5. Ubiquitous Language

| Term | Definition | Owner | Used in Release 0.9? | Notes |
| --- | --- | --- | --- | --- |
| Research target | Opaque key identifying which deterministic fixture set Application requests | Application | Yes | It is not an exchange symbol or Domain instrument model |
| Observation count | Positive number of observations requested for the reference window | Application | Yes | Use-case parameter, not Domain identity |
| Price observation | One source-independent positive price observed at a specific instant | Domain | Yes | No bid, ask, OHLCV, volume, exchange, or provider metadata |
| Observation series | Non-empty sequence of price observations with unique, strictly increasing instants | Domain | Yes | Owns sequence invariants |
| Arithmetic mean | Sum of observed prices divided by observation count | Domain | Yes | Pure deterministic behavior |
| Mean price | Positive Domain outcome of the arithmetic-mean calculation | Domain | Yes | Not a prediction, signal, or recommendation |
| Research request | Application contract carrying target and requested observation count | Application | Yes | Exact implementation name may vary, meaning may not |
| Research result | Application contract associating target/count with the mean-price outcome | Application | Yes | Application metadata wraps the Domain outcome |
| Observation source | Application-owned external boundary that supplies observations for a target/count | Application | Yes | Provider-neutral and persistence-neutral |
| Deterministic observation adapter | Offline Infrastructure implementation of the observation source | Infrastructure | Yes | Uses fixed immutable fixture data |
| Reference execution | Worker-initiated invocation of the approved request and presentation of its outcome | Worker | Yes | Contains no research logic |

## 6. Concept Ownership Matrix

| Concept | Owner | Required now? | Ownership reason |
| --- | --- | --- | --- |
| Price observation | DOMAIN | Yes | Carries price/time meaning and protects valid values |
| Observation series | DOMAIN | Yes | Protects non-empty, ordered, unique observation semantics |
| Arithmetic-mean behavior | DOMAIN | Yes | Pure quantitative behavior over valid Domain input |
| Mean price | DOMAIN | Yes | Represents the validated Domain outcome |
| Research target | APPLICATION | Yes | Selects external observations but does not affect the calculation |
| Observation count | APPLICATION | Yes | Defines the use-case request and adapter query |
| Research request/result | APPLICATION | Yes | Defines the use-case boundary and result metadata |
| Research orchestration | APPLICATION | Yes | Obtains observations, invokes Domain behavior, maps outcome |
| Observation source abstraction | APPLICATION | Yes | Expresses an external need without Infrastructure knowledge |
| Deterministic observation adapter | INFRASTRUCTURE | Yes | Fulfils the Application boundary offline and repeatably |
| Reference input and result presentation | WORKER | Yes | Starts and surfaces the composed use case |
| Instrument, exchange, provider, dataset, experiment | FUTURE | No | Not needed to calculate the reference mean |
| Generic repository, service, result, or error framework | REJECTED | No | Adds ceremony without reference-operation value |

## 7. Domain Concepts

### 7.1 Price observation

A price observation is one decimal price at one absolute instant. The instant
exists because series membership and order are meaningful. The observation is
source-independent: it does not know how, where, or from which market the value
was obtained.

Required state:

- absolute observation instant;
- decimal observed price.

### 7.2 Observation series

An observation series is a complete, non-empty sequence of price observations
whose instants are unique and strictly increasing. It is not a persisted
dataset, provider response, candle collection, or generic time-series
framework.

The series owns the sequence invariants because any Domain calculation must
operate on coherent input regardless of which Application or Infrastructure
component supplied it.

### 7.3 Mean price

Mean price is the arithmetic-mean outcome of a valid observation series:

```text
mean = sum(observed prices) / number of observations
```

It is a descriptive result for the supplied window. It is not a forecast,
signal, investment recommendation, return, benchmark, or performance metric.

### 7.4 Relationships

```text
Price observation (1..n)
          |
          v
Observation series --calculate arithmetic mean--> Mean price
```

No aggregate root, entity identity, repository, domain event, domain service,
factory hierarchy, or base abstraction is required.

## 8. Domain Invariants

| Concept | Invariant | Reason | Invalid example | Expected behavior | Ownership |
| --- | --- | --- | --- | --- | --- |
| Price observation | Instant must represent an absolute offset-aware instant | Equivalent observations must be ordered consistently | Unspecified/local time without an offset | Reject construction | Domain |
| Price observation | Price must be greater than zero | Zero or negative prices have no meaning in this reference operation | `0`, `-1.25` | Reject construction | Domain |
| Observation series | Series must contain at least one observation | Arithmetic mean is undefined for an empty set | `[]` | Reject construction | Domain |
| Observation series | Instants must be strictly increasing | The window must have one unambiguous chronological order | Later item precedes earlier item | Reject construction | Domain |
| Observation series | Instants must be unique | Two prices at the same instant make the minimal observation meaning ambiguous | Two entries at `2024-01-01T00:00:00Z` | Reject construction | Domain |
| Mean price | Result must be derived from the complete valid series using decimal arithmetic | Partial or floating/random behavior would break determinism | Divide by requested count when fewer observations exist | Do not produce a Domain result | Domain |

The research-target key and requested count are not Domain invariants because
the calculation itself does not require them. They are Application request
rules.

## 9. Application Boundary

Application owns the reference use case and contracts.

The input contract carries:

- a non-blank research-target key;
- a requested observation count greater than zero.

The output contract carries:

- the same target key;
- the actual observation count;
- the Domain mean-price outcome.

Application orchestration must:

1. validate the request;
2. ask its observation-source abstraction for the target/count;
3. require exactly the requested number of observations;
4. construct or invoke the approved Domain model;
5. return the associated research result.

Application does not calculate the mean, generate fixture data, know a
provider, or reference Infrastructure.

## 10. External Observation Boundary

Application requires one provider-neutral abstraction capable of supplying an
ordered collection of source-independent price observations for an opaque
target key and requested count.

The boundary must communicate enough information for Application to
distinguish:

- supported target with the complete requested window;
- unsupported target;
- supported target with insufficient observations.

The boundary does not expose HTTP, files, databases, exchanges, providers,
credentials, caching, retries, or selection frameworks. Application owns the
contract; Infrastructure later implements it.

## 11. Infrastructure Responsibility

WP06 may provide exactly one deterministic offline adapter for the
Application-owned observation boundary.

Its responsibility is to:

- recognize the approved reference target;
- return the approved immutable observations in chronological order;
- return the same observations for equivalent requests;
- report unsupported targets or insufficient counts according to the
  Application contract;
- operate without network, credentials, filesystem persistence, database,
  randomness, or current time.

This document does not authorize or name the concrete class.

## 12. Worker Responsibility

Worker remains the composition root and thinnest executable entry point. WP08
may:

- use the existing host and registrations;
- construct the canonical reference request;
- invoke the Application use case;
- surface the successful result or expected failure in a human-readable form;
- complete through the approved host lifecycle.

Worker must not validate Domain values, calculate the mean, generate
observations, select a provider, or contain reusable research orchestration.

## 13. Error and Invalid-Input Semantics

The minimum failure model distinguishes invalid state from expected use-case
failure without creating a general error framework.

| Condition | Owner | Semantics |
| --- | --- | --- |
| Blank research target | Application | Reject before calling the external boundary |
| Requested count less than one | Application | Reject before calling the external boundary |
| Unsupported target | Application/external contract | Return an explicit expected use-case failure; do not leak Infrastructure details |
| Fewer observations than requested | Application | Return an explicit insufficient-observations failure; do not calculate a partial result |
| Non-positive observed price | Domain | Reject invalid Domain construction |
| Missing/non-absolute instant | Domain | Reject invalid Domain construction |
| Empty, duplicate, or unordered series | Domain | Reject invalid Domain construction |
| Unexpected programming or operational defect | Owning layer | Propagate according to existing error policy; do not convert it into normal success/failure control flow |

Exact C# exception/result shapes belong to their implementation WPs. Expected
unsupported/insufficient cases must be explicit at the Application boundary;
technology-specific Infrastructure exceptions must not leak outward.

## 14. Deterministic Reference Scenario

### Reference input

```text
Research target: SAMPLE-USD
Requested observation count: 3
```

`SAMPLE-USD` is a synthetic fixture key. It does not name a real exchange,
provider, asset class, or supported market.

### Known observations

| Sequence | Instant | Price |
| ---: | --- | ---: |
| 1 | `2024-01-01T00:00:00+00:00` | `100.00` |
| 2 | `2024-01-02T00:00:00+00:00` | `110.00` |
| 3 | `2024-01-03T00:00:00+00:00` | `120.00` |

### Expected Domain behavior

```text
(100.00 + 110.00 + 120.00) / 3 = 110.00
```

### Expected research result

```text
Research target: SAMPLE-USD
Observation count: 3
Mean price: 110.00
```

### Invalid cases

- blank target: Application rejects the request;
- requested count `0`: Application rejects the request;
- unknown target: explicit unsupported-target outcome;
- request for `4` observations when only `3` are available: explicit
  insufficient-observations outcome, with no partial calculation;
- price `0.00`: Domain rejects the observation;
- duplicate or out-of-order instant: Domain rejects the series.

### Determinism proof

The scenario uses fixed decimal values and fixed offset-aware instants. It does
not read the network, clock, random generator, credentials, filesystem, or
database. Equivalent requests receive the same ordered observations and
therefore the same result.

## 15. Candidate Concepts Considered

| Candidate concept | Classification | Required now? | Reason | Key invariants/responsibility | Deferred alternative |
| --- | --- | --- | --- | --- | --- |
| Research target key | APPLICATION | Yes | Selects the deterministic fixture without affecting Domain behavior | Non-blank request value | Future instrument identity |
| Requested observation count | APPLICATION | Yes | Bounds the reference request | Greater than zero; exact count required | Future time range/window policy |
| Price observation | DOMAIN | Yes | Minimum value with price/time meaning | Positive price; absolute instant | Future quote/trade/candle |
| Observation series | DOMAIN | Yes | Owns coherent calculation input | Non-empty; unique, strictly increasing instants | Future generic time-series abstraction |
| Arithmetic-mean behavior | DOMAIN | Yes | The meaningful quantitative operation | Complete valid series; decimal arithmetic | Future analytics operations |
| Mean price | DOMAIN | Yes | Validated outcome | Derived only from complete series | Future analytics result hierarchy |
| Use-case request/result | APPLICATION | Yes | Stable execution boundary | Request validation; result association | Future experiment contract |
| Observation source | APPLICATION | Yes | Dependency inversion for external observations | Provider-neutral semantics | Future provider/data platform contracts |
| Deterministic adapter | INFRASTRUCTURE | Yes | Enables offline end-to-end execution | Same input, same output | Real providers are future scope |
| Reference runner | WORKER | Yes | Demonstrates composition and execution | No business logic | Future CLI/API/scheduling |
| Symbol | FUTURE | No | Existing documents define it as exchange-specific | Provider/exchange semantics | Instrument identity discovery |
| Instrument | FUTURE | No | Mean calculation does not require a modeled financial entity | Asset identity rules unresolved | Later research/data release |
| Dataset | FUTURE | No | Three in-memory observations do not require lifecycle/version/source governance | Reproducibility metadata | Later dataset capability |
| Experiment | FUTURE | No | Registry/configuration/history are beyond one operation | Reproducible experiment identity | Later experiment management |
| Generic repository/service/result framework | REJECTED | No | Adds abstraction without reference-operation behavior | None justified | Introduce only on demonstrated need |
| Aggregate root/domain event/factory hierarchy | REJECTED | No | No lifecycle, identity, or cross-entity consistency requires them | None justified | Re-evaluate only with new behavior |

## 16. Explicit Non-Goals

Release 0.9 does not implement or imply:

- financial advice, prediction, signal generation, or trading decisions;
- real market-data acquisition or provider selection;
- storage, dataset catalogs, ingestion, caching, or messaging;
- technical indicators, feature engineering, strategies, or backtests;
- portfolios, positions, orders, risk, or performance analysis;
- AI/ML models, training, inference, MLOps, or agents;
- plugins, REST APIs, web UIs, cloud deployment, or distributed execution;
- a generic analytics, time-series, repository, result, or error framework.

## 17. Future / Deferred Concepts

| Concept | Classification | Reason deferred/rejected for Release 0.9 |
| --- | --- | --- |
| Real market-data provider | FUTURE / NOT REQUIRED | Reference data is deterministic and offline |
| Provider selection | FUTURE / NOT REQUIRED | Exactly one fixture adapter is sufficient |
| Exchange and broker | FUTURE / NOT REQUIRED | No acquisition or execution occurs |
| Asset-class hierarchy | FUTURE / NOT REQUIRED | The synthetic target needs no taxonomy |
| OHLCV and candle | FUTURE / NOT REQUIRED | The mean requires one price per instant only |
| Order book, quote, and trade ticks | FUTURE / NOT REQUIRED | Their richer semantics are unused |
| Database and repository | FUTURE / NOT REQUIRED | No persistence or retrieval lifecycle exists |
| Historical ingestion | FUTURE / NOT REQUIRED | Fixture observations are already available |
| Cache and message broker | FUTURE / NOT REQUIRED | No remote or asynchronous pipeline exists |
| Dataset catalog | FUTURE / NOT REQUIRED | Dataset identity/version/source governance is outside the slice |
| Experiment registry | FUTURE / NOT REQUIRED | One invocation needs no experiment lifecycle |
| Strategy and backtest | FUTURE / NOT REQUIRED | The operation is descriptive, not evaluative |
| Portfolio, order, and risk | FUTURE / NOT REQUIRED | No investment or execution behavior exists |
| Plugin framework | FUTURE / NOT REQUIRED | No runtime extensibility is needed |
| Feature engineering | FUTURE / NOT REQUIRED | Mean price is Domain behavior, not a feature pipeline |
| AI/ML model and prediction | FUTURE / NOT REQUIRED | No learning or inference is involved |
| MLOps | FUTURE / NOT REQUIRED | No model lifecycle exists |
| Cloud deployment | FUTURE / NOT REQUIRED | Local offline execution is required |
| REST API and UI | FUTURE / NOT REQUIRED | Worker is the sole reference entry point |

## 18. Implementation Constraints for WP03-WP08

### WP03 - Research Domain Model

- Implement only price observation, observation series, arithmetic-mean
  behavior, and mean-price outcome.
- Preserve positive-price, absolute-instant, non-empty, unique, and strictly
  increasing sequence invariants.
- Keep Domain dependency-free and source/provider/request agnostic.
- Do not introduce instrument identity, repositories, domain events, generic
  frameworks, or future analytical concepts.

### WP04 - Research Application Contracts

- Define only the target/count request, associated result, use-case boundary,
  and Application-owned observation-source abstraction.
- Keep provider, storage, HTTP, fixture, and Worker details out of contracts.
- Represent unsupported-target and insufficient-observation outcomes explicitly
  without introducing a generalized error framework.

### WP05 - Research Execution Use Case

- Validate target and count before dependency invocation.
- Obtain observations exclusively through the Application abstraction.
- Require the complete requested count and never calculate a partial result.
- Delegate observation/series validation and arithmetic mean to Domain.
- Return target/count metadata with the Domain outcome.

### WP06 - Research Infrastructure Adapter

- Implement only the Application-owned observation-source abstraction.
- Use the exact synthetic reference fixture and deterministic semantics.
- Do not use network, clock, randomness, filesystem, database, provider
  selection, or real-provider terminology.

### WP07 - Dependency Registration

- Register only the Application use case and deterministic adapter required by
  the reference flow.
- Keep existing Application and Infrastructure registration boundaries.
- Justify lifetimes; introduce no scanning, locator, plugin, or configuration
  framework.

### WP08 - Worker Research Execution

- Construct the canonical `SAMPLE-USD`, count `3` request.
- Resolve and invoke the Application use case through existing composition.
- Surface the result or expected failure without owning validation,
  observations, or arithmetic behavior.
- Keep the Worker deterministic and composition-focused.

These constraints approve semantics, not exact filenames or unnecessary type
hierarchies. Later WPs should choose the smallest clear implementation names
consistent with this vocabulary.

## 19. Testing Implications for WP09-WP12

### Domain tests

Prove:

- valid observations and series are accepted;
- zero/negative prices and non-absolute instants are rejected;
- empty, duplicate, and unordered series are rejected;
- arithmetic mean uses every observation and returns the reference `110.00`;
- equivalent input produces equivalent Domain outcomes.

### Application tests

Prove independently of concrete Infrastructure:

- valid request orchestration and result association;
- blank target and non-positive count rejection before source invocation;
- unsupported target handling;
- insufficient observations never produce a partial result;
- Domain behavior is coordinated rather than reimplemented.

### Infrastructure tests

Prove:

- the synthetic target returns the exact three approved observations;
- repeat requests return equivalent ordered observations;
- unsupported target and excessive count follow the Application contract;
- no external dependency, current time, or randomness is involved.

### Architecture tests

Preserve all seven Release 0.8 rules and objectively protect, where semantic
inspection permits:

- Domain independence from Application, Infrastructure, and Worker;
- Application ownership of the external observation abstraction;
- Infrastructure implementation without reverse dependency leakage;
- Worker composition rather than Domain/adapter ownership;
- production graph acyclicity.

No arbitrary coverage or architecture-test count is required.

## 20. Open Questions

No question affecting WP03 remains open.

Deferred questions include the future instrument identity model, time-range
selection, richer market observations, dataset provenance, and additional
analytics. They are explicitly outside the reference operation and must not
influence WP03.

## 21. Decision Summary

Release 0.9 approves one minimum Domain behavior: calculate the arithmetic mean
of a valid observation series made from positive, timestamped price
observations.

Application owns target/count request semantics, orchestration, expected
use-case failures, result association, and the external observation-source
abstraction. Infrastructure supplies the fixed offline fixture. Worker composes
and invokes the canonical scenario.

This model fits the accepted four-project graph, requires no new project or
package, contains no provider or persistence commitment, and leaves no blocking
Domain question for WP03.

## 22. Conclusion

The first Research Platform capability is intentionally modest but meaningful:
it turns a deterministic sequence of valid market observations into a
verifiable quantitative result through the intended architecture.

Its value is architectural and behavioral. It establishes precise vocabulary,
ownership, invariants, failure semantics, and a shared fixture before code
makes those decisions expensive, while preserving every future capability as
future scope.
