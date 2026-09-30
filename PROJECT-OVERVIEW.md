# Automotive Intelligence --- Project Overview

## Executive Summary

Automotive Intelligence is a Pakistan-focused automotive market
intelligence and vehicle valuation platform developed as a
portfolio-grade .NET application.

It combines application architecture, REST API development, database
design, data ingestion, data quality, external data integration, market
analytics and vehicle valuation.

The current valuation engine is rule-based and backed by available
market observations. The architecture is designed to support future
machine-learning valuation without coupling the UI to the valuation
implementation.

## Problem

Used-car prices vary according to:

-   Make
-   Model
-   Variant
-   Model year
-   Mileage
-   City
-   Condition
-   Accident history
-   Ownership history

A useful valuation therefore needs more than a fixed lookup price.

The platform combines vehicle information with available market
observations and returns an estimated value, market range, confidence
score and supporting factors.

## What Was Built

### Automotive Domain

The system models makes, models, variants, vehicles, cities, conditions,
listings, market prices, valuations, data sources and imports, with
additional entities for user and future platform capabilities.

### REST API

The API exposes reusable functionality under `/api/v1/...`.

The Web application consumes the API, leaving the business layer
reusable for a future mobile client.

### Market Data Pipeline

``` text
Import
  ↓
Validation
  ↓
Duplicate Detection
  ↓
Normalization
  ↓
Vehicle Mapping
  ↓
Market Projection
  ↓
Market Intelligence
  ↓
Valuation
```

### Data Quality

The platform identifies data problems such as missing prices, missing
locations, invalid years, suspicious prices and incomplete vehicle
mapping.

### Market Intelligence

The current development market layer contains approximately:

``` text
9,911 market observations
5 makes
10 models
11 cities
41 model years
```

The Market Intelligence UI provides market summaries, price trends and
distributions by make, model, city and year.

These figures describe the current development dataset and are not
intended to represent the complete Pakistan market.

### Valuation

The current valuation service:

1.  Retrieves applicable market observations.
2.  Establishes a representative market baseline.
3.  Applies vehicle-specific adjustments.
4.  Calculates a market range.
5.  Calculates a confidence score.
6.  Assesses an asking price when supplied.

The implementation is rule-based.

### PAMA and OICA

The project includes source-specific data structures and services for
PAMA production/sales data and OICA production data.

These are separated from the used-car market layer because their data
structures and analytical meaning differ.

## Key Architecture Decisions

### API-first client boundary

The Web application communicates through the REST API instead of
accessing the database directly.

This makes the API the reusable application boundary for future clients.

### Modular architecture instead of premature microservices

The solution separates Domain, Application, Data, Infrastructure, API
and Web responsibilities while remaining operationally manageable as a
single solution.

Individual services can be extracted later if scale or organizational
requirements justify it.

### Rule-based valuation before ML

Machine learning requires suitable, sufficiently representative and
appropriately licensed training data.

The project therefore established the data pipeline and rule-based
baseline first.

The ML model can later be evaluated against the same valuation contract.

## Product Screens

### Home

Introduces the platform and directs users to valuation and market
intelligence.

### Vehicle Valuation

Supports quick, standard and detailed estimates.

### Valuation Result

Displays estimated value, market range, confidence and valuation
factors.

### Market Intelligence

Displays market-level price and distribution information.

### Admin Dashboard

Provides market, data-health, PAMA and OICA intelligence.

### Data Import

Provides the operational entry point for importing market data.

## Data Governance

A data source carries licensing/compliance metadata.

Before commercial deployment, every source must be reviewed for:

-   License
-   Commercial-use permission
-   Commercial model-training permission
-   Attribution requirements
-   Terms of access
-   Redistribution restrictions

A development dataset with a non-commercial restriction must not
automatically be used as commercial training data or redistributed with
the product.

## Current Limitations

1.  The valuation engine is rule-based.
2.  Market observations may be asking prices rather than completed
    transactions.
3.  Vehicle mapping is not exhaustive.
4.  Market coverage is limited to processed sources.
5.  Some development datasets have licensing restrictions.
6.  Production deployment needs additional security, monitoring and
    operational controls.
7.  The current dataset is not a complete statistical representation of
    Pakistan's automotive market.

## Future Roadmap

### Machine Learning Valuation

``` text
Clean Market Dataset
        ↓
Feature Engineering
        ↓
Training / Validation
        ↓
ML Model
        ↓
Prediction
        ↓
Confidence / Explainability
```

### Predictive Intelligence

Potential future capabilities:

-   Depreciation prediction
-   Price forecasting
-   Demand prediction
-   Price anomaly detection
-   Vehicle comparison
-   Buy/sell signals
-   Price alerts

### Computer Vision

A future service could derive vehicle-condition signals from uploaded
images.

### Mobile Application

A mobile client can consume the existing REST API.

### Automotive AI Assistant

Future intelligence could combine market data, valuation, vehicle
knowledge and user context.

## Portfolio Value

The project demonstrates:

-   .NET 10
-   ASP.NET Core
-   REST API design
-   EF Core
-   SQL Server
-   Dependency Injection
-   DTO-based API boundaries
-   Business-rule engines
-   Data ingestion
-   Data normalization
-   Data quality
-   External integrations
-   Dashboard development
-   MVC
-   JavaScript
-   Extensible ML-ready architecture

## Interview Talking Points

### Architecture

> I separated Web, API, Application, Domain, Infrastructure and Data
> concerns so that valuation and market-intelligence logic is not
> coupled to the MVC UI.

### Data

> I built an ingestion pipeline that validates, deduplicates, normalizes
> and maps used-car observations before projecting them into the
> market-price layer.

### Valuation

> I started with a rule-based market-data valuation engine because
> reliable, normalized training data is a prerequisite for a meaningful
> ML model.

### Extensibility

> The valuation engine is behind an interface, so a future ML
> implementation can replace or complement the rule-based implementation
> without changing the clients.

### API

> The MVC application communicates through the REST API, so a future
> mobile client can consume the same business capabilities.

### Data Quality

> I treated data quality as part of the architecture because valuation
> quality depends directly on the quality and coverage of the underlying
> observations.

## Suggested CV / Portfolio Description

> Built a Pakistan-focused automotive market intelligence and vehicle
> valuation platform using .NET 10, ASP.NET Core Web API, MVC, EF Core
> and SQL Server. Designed an SOA-oriented architecture with separate
> Domain, Application, Infrastructure and Data layers; implemented
> used-car data ingestion, normalization, duplicate protection, vehicle
> mapping, data-quality analysis, PAMA/OICA integrations, market
> intelligence dashboards and a rule-based valuation engine producing
> market ranges and confidence scores. Designed the valuation
> abstraction to support future machine-learning and predictive pricing
> capabilities.
