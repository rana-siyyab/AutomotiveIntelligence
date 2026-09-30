using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Data.Context;

public class AutomotiveIntelligenceDbContext : DbContext
{
    public AutomotiveIntelligenceDbContext(
        DbContextOptions<AutomotiveIntelligenceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries => Set<Country>();

    public DbSet<Province> Provinces => Set<Province>();

    public DbSet<City> Cities => Set<City>();

    public DbSet<Make> Makes => Set<Make>();

    public DbSet<Model> Models => Set<Model>();

    public DbSet<Variant> Variants => Set<Variant>();

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public DbSet<VehicleSpecifications> VehicleSpecifications => Set<VehicleSpecifications>();

    public DbSet<VehicleCondition> VehicleConditions => Set<VehicleCondition>();

    public DbSet<Listing> Listings => Set<Listing>();

    public DbSet<MarketPrice> MarketPrices => Set<MarketPrice>();
    public DbSet<PamaProduction> PamaProductions { get; set; }

    public DbSet<MarketSnapshot> MarketSnapshots => Set<MarketSnapshot>();

    public DbSet<DataSource> DataSources => Set<DataSource>();

    public DbSet<DataImport> DataImports => Set<DataImport>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<Valuation> Valuations => Set<Valuation>();

    public DbSet<ValuationFactor> ValuationFactors => Set<ValuationFactor>();

    public DbSet<Favorite> Favorites => Set<Favorite>();

    public DbSet<Comparison> Comparisons => Set<Comparison>();

    public DbSet<ComparisonVehicle> ComparisonVehicles => Set<ComparisonVehicle>();

    public DbSet<PriceAlert> PriceAlerts => Set<PriceAlert>();

    public DbSet<Notification> Notifications => Set<Notification>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AutomotiveIntelligenceDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<OicaProduction> OicaProductions
    => Set<OicaProduction>();
    public DbSet<UsedCarDatasetRecord>
    UsedCarDatasetRecords
    => Set<UsedCarDatasetRecord>();
    public DbSet<UsedCarDatasetNormalization>
    UsedCarDatasetNormalizations
    => Set<UsedCarDatasetNormalization>();
    public DbSet<UsedCarDatasetVehicleMapping>
    UsedCarDatasetVehicleMappings
    => Set<UsedCarDatasetVehicleMapping>();
}