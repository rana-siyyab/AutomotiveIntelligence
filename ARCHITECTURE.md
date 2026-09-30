# Automotive Intelligence --- Architecture

## Architectural Goal

Automotive Intelligence uses an SOA-oriented modular architecture.

The main goals are:

-   Keep business rules independent from UI technology.
-   Prevent Web and future Mobile clients from accessing SQL Server
    directly.
-   Expose reusable capabilities through REST APIs.
-   Keep the Domain layer independent of infrastructure.
-   Isolate external data-source connectors.
-   Keep valuation behind an abstraction so it can evolve from rules to
    ML.
-   Make data ingestion traceable and repeatable.

## High-Level Architecture

``` text
┌───────────────────────────────┐
│       Web / MVC Client        │
│   Razor + JavaScript + CSS    │
└───────────────┬───────────────┘
                │ HTTP / HTTPS
                ▼
┌───────────────────────────────┐
│          REST API             │
│       /api/v1/...             │
└───────────────┬───────────────┘
                │
        ┌───────┴────────┐
        ▼                ▼
┌───────────────┐ ┌────────────────────┐
│ Application   │ │ Infrastructure     │
│ Services      │ │ Services/Connectors│
└───────┬───────┘ └─────────┬──────────┘
        │                    │
        ▼                    ▼
┌───────────────┐     ┌───────────────┐
│ Domain        │     │ Data / EF Core │
│ Entities      │     │ Repositories   │
└───────────────┘     └───────┬────────┘
                              ▼
                       ┌─────────────┐
                       │ SQL Server  │
                       └─────────────┘
```

## Projects

### AutomotiveIntelligence.Domain

Contains core entities and business concepts. It should remain
independent of ASP.NET Core, MVC and persistence implementation details.

### AutomotiveIntelligence.Application

Contains DTOs, interfaces and application services.

Examples include:

``` text
IValuationService
IMarketDataImportService
IDataQualityService
IDataSourceService
```

### AutomotiveIntelligence.Data

Contains:

-   DbContext
-   EF Core configurations
-   Repositories
-   Migrations
-   Seed configurations

### AutomotiveIntelligence.Infrastructure

Contains implementation concerns such as:

-   Import services
-   External connectors
-   Data normalization
-   Market projection
-   Vehicle mapping
-   Source-specific processing

### AutomotiveIntelligence.Api

Exposes application capabilities through REST controllers.

### AutomotiveIntelligence.Web

Provides the MVC user interface and communicates with the API.

### AutomotiveIntelligence.Shared

Contains reusable cross-project contracts/utilities.

### AutomotiveIntelligence.Tests

Reserved for automated testing of domain and application behavior.

## Dependency Rules

``` text
Web
 ↓
API
 ↓
Application
 ↓
Domain

Infrastructure
 ↓
Application + Domain + Data
```

The Web layer does not directly access SQL Server for business
operations.

## API Boundary

The API uses versioned routes:

``` text
/api/v1/...
```

DTOs are used at the public API boundary rather than exposing EF
entities directly.

``` text
HTTP Request
    ↓
Request DTO
    ↓
Application Service
    ↓
Domain / Repository
    ↓
Result DTO
    ↓
HTTP Response
```

## Valuation Architecture

``` text
IValuationService
       │
       └── RuleBasedValuationService
```

The intended future design is:

``` text
IValuationService
       ├── RuleBasedValuationService
       └── MachineLearningValuationService
```

A hybrid implementation could combine market rules, ML prediction and
confidence/data-quality signals.

## Data Pipeline

``` text
Raw Dataset
    ↓
Import
    ├── Validation
    ├── Duplicate Detection
    └── Import Audit
    ↓
Normalization
    ├── Location
    ├── Make/Model
    └── Vehicle Name
    ↓
Vehicle Mapping
    ├── Make
    ├── Model
    └── Variant
    ↓
Market Projection
    ↓
MarketPrices
    ├── Market Intelligence
    └── Valuation
```

## External Source Architecture

``` text
IExternalDataSourceConnector
        ├── PamaDataSourceConnector
        └── OicaDataSourceConnector
```

A connector resolver selects the appropriate implementation for a
registered source.

This keeps source-specific retrieval logic out of controllers and core
business services.

## PAMA / OICA Separation

``` text
PAMA
 └── PamaProduction
      └── PAMA Intelligence

OICA
 └── OicaProduction
      └── OICA Intelligence

Used Cars
 └── MarketPrice
      ├── Market Intelligence
      └── Valuation
```

This avoids forcing unrelated source schemas into a single generic
production table.

## Data Quality

Data quality is treated as part of the data pipeline.

Examples include:

``` text
Missing Price
Missing Location
Invalid Year
Suspicious Price
Suspicious Engine Capacity
Unmapped Make
Unmapped Model
Unmapped Variant
```

## Web Architecture

``` text
Razor View
   ↓
MVC Controller
   ↓
API Client
   ↓
REST API
```

Page-specific JavaScript is kept separate from Razor views.

Examples:

``` text
valuation.js
market-intelligence.js
admin-dashboard.js
```

## Future Mobile Architecture

``` text
┌───────────────┐
│ Web Client    │
└───────┬───────┘
        │
        ▼
     REST API
        ▲
        │
┌───────┴───────┐
│ Mobile Client │
└───────────────┘
```

The future mobile client consumes the same API and does not access SQL
Server directly.

## Data Compliance

Data sources include metadata for:

``` text
License
CommercialUseAllowed
CommercialTrainingAllowed
AttributionRequired
TermsUrl
AcquiredDate
```

This allows the project to distinguish development/research data from
data that can legitimately be used in a commercial production system.

## Architectural Evolution

``` text
Automotive Data
      ↓
Data Processing
      ↓
Quality + Mapping
      ↓
Market Data
      ↓
┌──────────┬──────────┬──────────┐
Rules      ML       Forecasting
└──────────┴──────────┴──────────┘
             ↓
      Valuation Service
             ↓
          REST API
         ↙       ↘
       Web       Mobile
```

The current implementation establishes the foundations without
prematurely introducing ML infrastructure.
