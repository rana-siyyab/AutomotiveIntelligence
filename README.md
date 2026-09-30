# Automotive Intelligence

> Pakistan-focused automotive market intelligence and vehicle valuation
> platform built with .NET 10, ASP.NET Core, EF Core, SQL Server, REST
> APIs, and ASP.NET Core MVC.

## Overview

Automotive Intelligence is a portfolio-grade automotive intelligence
platform initially focused on the Pakistan used-car market.

It combines structured vehicle data, used-car market observations, data
normalization, data-quality processing, market intelligence, external
automotive data sources, and a rule-based valuation engine.

The architecture is designed so the valuation implementation can later
evolve toward machine learning without replacing the client applications
or core domain model.

## Main Capabilities

### Vehicle Valuation

The valuation workflow supports:

-   Quick estimates using Make + Model
-   Standard estimates using Make + Model + Year
-   Detailed estimates using additional vehicle information
-   Estimated market value
-   Minimum and maximum market range
-   Confidence score
-   Asking-price deal assessment
-   Recommended buying price
-   Recommended negotiation price
-   Valuation factors

More complete vehicle information is used to produce a more specific
result.

### Market Intelligence

The current development market layer provides:

-   Market-record count
-   Average observed price
-   Latest average observed price
-   Minimum and maximum observed prices
-   Price trends
-   Market distribution by make
-   Market distribution by model
-   Market distribution by city
-   Market distribution by model year

The current development dataset contains approximately 9,911 projected
market-price observations across 5 makes, 10 models and 11 cities.

### Data Pipeline

The used-car processing pipeline includes:

1.  Source registration
2.  Import
3.  Validation
4.  Duplicate detection
5.  Normalization
6.  Vehicle mapping
7.  Market-price projection
8.  Data-quality analysis
9.  Market intelligence

### External Automotive Data

The architecture includes source-specific pipelines for:

-   PAMA production/sales data
-   OICA production data

## Technology Stack

-   .NET 10
-   C#
-   ASP.NET Core Web API
-   ASP.NET Core MVC
-   Entity Framework Core
-   SQL Server
-   REST
-   OpenAPI
-   Razor
-   JavaScript
-   Chart.js
-   Dependency Injection

## Solution Structure

``` text
AutomotiveIntelligence.slnx
├── AutomotiveIntelligence.Web
├── AutomotiveIntelligence.Api
├── AutomotiveIntelligence.Domain
├── AutomotiveIntelligence.Application
├── AutomotiveIntelligence.Data
├── AutomotiveIntelligence.Infrastructure
├── AutomotiveIntelligence.Shared
└── AutomotiveIntelligence.Tests
```

## Dependency Direction

``` text
Web
 ↓ HTTP
API
 ↓
Application
 ↓
Domain

Infrastructure
 ↓
Application + Domain + Data

Data
 ↓
SQL Server
```

The Web application does not directly access SQL Server for normal
business operations.

## Important Domain Concepts

The current domain includes entities for:

-   Make
-   Model
-   Variant
-   Vehicle
-   VehicleSpecifications
-   Country
-   Province
-   City
-   VehicleCondition
-   Listing
-   MarketPrice
-   DataSource
-   DataImport
-   MarketSnapshot
-   User
-   Role
-   Valuation
-   ValuationFactor
-   Favorite
-   Comparison
-   ComparisonVehicle
-   PriceAlert
-   Notification

Additional data-pipeline entities support normalization, vehicle mapping
and production datasets.

## Valuation Engine

The active valuation implementation is rule-based and is exposed through
`IValuationService`.

The service establishes a market baseline from applicable observations
and applies factors including:

-   Mileage
-   Vehicle condition
-   Accident history
-   Number of owners
-   Information completeness

It produces a market range and confidence score. When an asking price is
supplied, it also provides a rule-based deal assessment.


## API Surface

### Vehicle Lookup

``` text
GET /api/v1/vehicle-lookup/makes
GET /api/v1/vehicle-lookup/models/{makeId}
GET /api/v1/vehicle-lookup/variants/{modelId}
GET /api/v1/vehicle-lookup/countries
GET /api/v1/vehicle-lookup/provinces/{countryId}
GET /api/v1/vehicle-lookup/cities/{provinceId}
GET /api/v1/vehicle-lookup/conditions
```

### Valuation

``` text
POST /api/v1/valuation/calculate
GET  /api/v1/valuation/{valuationId}
GET  /api/v1/valuation/recent
```

### Market Intelligence

``` text
GET /api/v1/market-intelligence/overview
GET /api/v1/market-intelligence/by-make
GET /api/v1/market-intelligence/by-model
GET /api/v1/market-intelligence/by-city
GET /api/v1/market-intelligence/by-year
GET /api/v1/market-intelligence/price-trends?days=30
```

### Dashboard

``` text
GET /api/v1/dashboard/market-overview
GET /api/v1/dashboard/price-trends
GET /api/v1/dashboard/popular-models
```

### Data Sources

``` text
GET  /api/v1/data-sources
GET  /api/v1/data-sources/{sourceId}
POST /api/v1/data-sources
PUT  /api/v1/data-sources/{sourceId}/status
```

## Data Quality

The pipeline records processed, inserted, rejected and duplicate rows,
plus import status and errors.

Quality analysis can identify:

-   Missing prices
-   Missing locations
-   Invalid years
-   Suspicious prices
-   Suspicious engine capacities
-   Normalization coverage
-   Vehicle mapping coverage
-   Valuation readiness

## Data Governance

The `DataSource` model includes compliance metadata such as:

``` text
License
CommercialUseAllowed
CommercialTrainingAllowed
AttributionRequired
TermsUrl
AcquiredDate
```

Development datasets must not automatically be treated as commercially
usable datasets. Source licenses, terms and commercial-training rights
must be verified before commercial deployment.

## Current Limitations

-   The valuation engine is rule-based.
-   Market observations may represent asking prices rather than
    completed transactions.
-   Vehicle mapping is not exhaustive.
-   Dataset coverage is limited to the currently processed sources.
-   Some development datasets have licensing restrictions.
-   Production deployment would require additional operational security
    and monitoring.
-   The current dataset should not be presented as a complete
    representation of the Pakistan automotive market.

## Future Roadmap

``` text
Market Data
    ↓
Normalization / Quality
    ↓
Market Intelligence
    ↓
Rule-Based Valuation
    ↓
Machine-Learning Valuation
    ↓
Predictive Automotive Intelligence
```

Potential future capabilities include depreciation prediction, price
forecasting, demand prediction, vehicle comparison, price alerts,
image-based condition analysis, mobile clients and an automotive AI
assistant.

## Portfolio Description

> Automotive Intelligence is a Pakistan-focused automotive market
> intelligence and vehicle valuation platform built with .NET 10,
> ASP.NET Core Web API, MVC, EF Core and SQL Server. The platform
> ingests and normalizes automotive market data, performs data-quality
> validation and vehicle mapping, provides market intelligence across
> makes, models, cities and model years, and uses a rule-based valuation
> engine to estimate vehicle market values with ranges, confidence
> scores and supporting factors. The architecture is designed to evolve
> toward machine-learning-based valuation and predictive automotive
> intelligence.
