
# Data Glossary

**Status:** Draft
**Version:** 1.0
**Last Updated:** 2026-08-09
**Owners:** AIQuantTradingResearch Team

---

# Purpose

The Data Glossary establishes a shared vocabulary for market data, quantitative research, and data engineering within AIQuantTradingResearch.

It ensures contributors use consistent terminology when discussing datasets, market observations, analytics, and machine learning workflows.

The glossary complements the Business Domain Model by defining data-centric concepts rather than business entities.

---

# Guiding Principles

The glossary should:

* Promote a common language.
* Reduce ambiguity.
* Improve communication.
* Support architectural consistency.
* Serve as the authoritative reference for data terminology.

Definitions should remain technology-independent whenever possible.

---

# Market Data Concepts

## Tick

The smallest observable market event.

A tick represents a single market update, such as a trade or a quote.

---

## Quote

A point-in-time market observation containing bid, ask, or last traded information.

Quotes describe market conditions rather than completed transactions.

---

## Trade

A completed transaction between market participants.

Trades contribute to market price discovery and may be aggregated into candles.

---

## Candle

A time-based aggregation of market activity.

A candle summarizes trading activity during a defined interval.

Typical attributes include:

* Open
* High
* Low
* Close
* Volume

---

## OHLCV

A standard market data representation composed of:

* Open
* High
* Low
* Close
* Volume

OHLCV is the most common format for historical market analysis.

---

## Order Book

A snapshot of active buy and sell orders for a financial instrument.

Order books provide insight into market liquidity and supply-demand dynamics.

---

## Bid Price

The highest price currently offered by buyers.

---

## Ask Price

The lowest price currently offered by sellers.

---

## Spread

The difference between the best ask price and the best bid price.

Spread is often used as an indicator of market liquidity.

---

## Mid Price

The midpoint between the current bid and ask prices.

---

## Volume

The quantity traded during a market event or aggregation interval.

---

# Time Concepts

## Timestamp

The recorded time associated with a market observation.

Timestamps should always be expressed using a consistent time standard.

---

## Time Interval

The duration represented by a candle or aggregated observation.

Examples include:

* 1 minute
* 5 minutes
* 1 hour
* 1 day

---

## Trading Session

A period during which a market is open for trading.

---

## Historical Data

Market observations collected from past trading activity.

Historical data is used for research, feature engineering, and backtesting.

---

## Real-Time Data

Market observations received with minimal delay while trading is occurring.

---

# Dataset Concepts

## Dataset

A curated collection of market observations prepared for analysis.

Datasets should be versioned, traceable, and reproducible.

---

## Schema

The structural definition of a dataset.

A schema describes available fields, data types, and relationships.

---

## Metadata

Information describing a dataset rather than its contents.

Examples include:

* Provider
* Coverage period
* Schema version
* Creation date
* Quality metrics

---

## Data Lineage

The complete history of how data was acquired, transformed, and published.

Lineage supports traceability and reproducibility.

---

## Data Provenance

Information describing the original source of a dataset or observation.

---

# Feature Engineering Concepts

## Feature

A measurable attribute derived from raw market data.

Features provide input to analytical models.

---

## Feature Set

A collection of related features used during experimentation or model training.

---

## Label

The expected outcome associated with historical observations.

Labels enable supervised machine learning.

---

## Feature Pipeline

The sequence of transformations that converts market observations into analytical features.

---

# Machine Learning Concepts

## Training Dataset

The dataset used to train a machine learning model.

---

## Validation Dataset

The dataset used to evaluate model performance during development.

---

## Test Dataset

An independent dataset used to assess model generalization.

---

## Prediction

The output produced by a trained model.

Predictions represent analytical results rather than trading decisions.

---

## Inference

The process of generating predictions from a trained model.

---

# Backtesting Concepts

## Strategy

A formal collection of investment rules evaluated using historical data.

---

## Backtest

A historical simulation of a strategy using previously observed market data.

---

## Benchmark

A reference strategy or market index used for comparison.

---

## Drawdown

The decline from a portfolio's historical peak value.

Maximum Drawdown is commonly used as a measure of downside risk.

---

## Sharpe Ratio

A risk-adjusted performance metric comparing excess return to volatility.

---

## Win Rate

The percentage of profitable trades generated by a strategy.

---

# Data Engineering Concepts

## Canonical Model

The platform's standardized representation of market information.

Provider-specific formats are transformed into the canonical model before downstream processing.

---

## Normalization

The process of converting heterogeneous observations into the canonical model.

---

## Validation

The process of verifying that observations satisfy predefined quality rules.

---

## Data Quality

The measurable level of confidence in a dataset based on defined quality dimensions.

---

## Data Catalog

A searchable registry of available datasets and their metadata.

---

## Confidence Score

A quantitative assessment of trust in a dataset.

The Confidence Score summarizes quality dimensions such as completeness, accuracy, consistency, traceability, and reproducibility.

It provides a standardized indication of dataset reliability for downstream consumers.

---

# Relationship to Other Architecture Documents

This glossary complements:

* Business Domain Model
* Data Platform Vision
* Data Lifecycle
* Data Quality
* Data Catalog
* Domain Context Map

Together, these documents establish a consistent language for business concepts, market data, and engineering practices.

---

# Maintaining the Glossary

New terms should be added whenever new business capabilities or architectural concepts are introduced.

Definitions should remain concise, unambiguous, and independent of implementation technologies.

The glossary should evolve alongside the platform while preserving consistency across all project documentation.

---

# Guiding Statement

A shared vocabulary is the foundation of effective engineering.

The Data Glossary ensures that contributors, architects, researchers, and developers describe market information using a consistent and precise language, enabling clearer communication and more maintainable software.
