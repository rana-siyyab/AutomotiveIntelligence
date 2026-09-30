using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SourceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BaseUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.SourceId);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    MakeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.MakeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleConditions",
                columns: table => new
                {
                    ConditionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleConditions", x => x.ConditionId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataImports",
                columns: table => new
                {
                    ImportId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RecordsProcessed = table.Column<int>(type: "int", nullable: false),
                    RecordsInserted = table.Column<int>(type: "int", nullable: false),
                    RecordsRejected = table.Column<int>(type: "int", nullable: false),
                    ImportStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ErrorLog = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataImports", x => x.ImportId);
                    table.ForeignKey(
                        name: "FK_DataImports_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BodyType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_Models_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "MakeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comparisons",
                columns: table => new
                {
                    ComparisonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comparisons", x => x.ComparisonId);
                    table.ForeignKey(
                        name: "FK_Comparisons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NotificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EngineCapacity = table.Column<int>(type: "int", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriveType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_Variants_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketPrices",
                columns: table => new
                {
                    MarketPriceId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: true),
                    Mileage = table.Column<int>(type: "int", nullable: true),
                    ObservedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    ObservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPrices", x => x.MarketPriceId);
                    table.ForeignKey(
                        name: "FK_MarketPrices_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketPrices_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketPrices_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketPrices_VehicleConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "VehicleConditions",
                        principalColumn: "ConditionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketSnapshots",
                columns: table => new
                {
                    SnapshotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AveragePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MedianPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimumPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaximumPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ListingCount = table.Column<int>(type: "int", nullable: false),
                    DemandScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketSnapshots", x => x.SnapshotId);
                    table.ForeignKey(
                        name: "FK_MarketSnapshots_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketSnapshots_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceAlerts",
                columns: table => new
                {
                    PriceAlertId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    TargetPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTriggeredDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceAlerts", x => x.PriceAlertId);
                    table.ForeignKey(
                        name: "FK_PriceAlerts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PriceAlerts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceAlerts_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    ManufacturingYear = table.Column<int>(type: "int", nullable: false),
                    RegistrationYear = table.Column<int>(type: "int", nullable: true),
                    Mileage = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConditionId = table.Column<int>(type: "int", nullable: true),
                    OwnerCount = table.Column<int>(type: "int", nullable: true),
                    HasAccidentHistory = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "VehicleConditions",
                        principalColumn: "ConditionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleSpecifications",
                columns: table => new
                {
                    VehicleSpecificationsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    EngineCapacity = table.Column<int>(type: "int", nullable: true),
                    Horsepower = table.Column<int>(type: "int", nullable: true),
                    Torque = table.Column<int>(type: "int", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriveType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SeatingCapacity = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Width = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Wheelbase = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    GroundClearance = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    FuelTankCapacity = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    BootSpace = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    AirbagCount = table.Column<int>(type: "int", nullable: true),
                    HasABS = table.Column<bool>(type: "bit", nullable: true),
                    HasSunroof = table.Column<bool>(type: "bit", nullable: true),
                    HasTractionControl = table.Column<bool>(type: "bit", nullable: true),
                    HasStabilityControl = table.Column<bool>(type: "bit", nullable: true),
                    HasInfotainment = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSpecifications", x => x.VehicleSpecificationsId);
                    table.ForeignKey(
                        name: "FK_VehicleSpecifications_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonVehicles",
                columns: table => new
                {
                    ComparisonId = table.Column<long>(type: "bigint", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonVehicles", x => new { x.ComparisonId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_ComparisonVehicles_Comparisons_ComparisonId",
                        column: x => x.ComparisonId,
                        principalTable: "Comparisons",
                        principalColumn: "ComparisonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComparisonVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoriteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ListingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    AskingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SellerType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ListingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    SourceReference = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ListingUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ListingStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingId);
                    table.ForeignKey(
                        name: "FK_Listings_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Listings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Valuations",
                columns: table => new
                {
                    ValuationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    ManufacturingYear = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: true),
                    AskingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    EstimatedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimumPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaximumPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ValuationMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valuations", x => x.ValuationId);
                    table.ForeignKey(
                        name: "FK_Valuations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Valuations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Valuations_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Valuations_VehicleConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "VehicleConditions",
                        principalColumn: "ConditionId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Valuations_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ValuationFactors",
                columns: table => new
                {
                    ValuationFactorId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValuationId = table.Column<long>(type: "bigint", nullable: false),
                    FactorType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FactorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdjustmentValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AdjustmentPercentage = table.Column<decimal>(type: "decimal(7,4)", precision: 7, scale: 4, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuationFactors", x => x.ValuationFactorId);
                    table.ForeignKey(
                        name: "FK_ValuationFactors_Valuations_ValuationId",
                        column: x => x.ValuationId,
                        principalTable: "Valuations",
                        principalColumn: "ValuationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId_Name",
                table: "Cities",
                columns: new[] { "ProvinceId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comparisons_UserId",
                table: "Comparisons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonVehicles_ComparisonId_DisplayOrder",
                table: "ComparisonVehicles",
                columns: new[] { "ComparisonId", "DisplayOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonVehicles_VehicleId",
                table: "ComparisonVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                table: "Countries",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataImports_SourceId",
                table: "DataImports",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_Name",
                table: "DataSources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId_VehicleId",
                table: "Favorites",
                columns: new[] { "UserId", "VehicleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_VehicleId",
                table: "Favorites",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_CityId",
                table: "Listings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ListingDate",
                table: "Listings",
                column: "ListingDate");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ListingStatus",
                table: "Listings",
                column: "ListingStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_SourceId",
                table: "Listings",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_VehicleId",
                table: "Listings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Makes_Name",
                table: "Makes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrices_CityId",
                table: "MarketPrices",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrices_ConditionId",
                table: "MarketPrices",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrices_ObservationDate",
                table: "MarketPrices",
                column: "ObservationDate");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrices_SourceId",
                table: "MarketPrices",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrices_VariantId_CityId_Year",
                table: "MarketPrices",
                columns: new[] { "VariantId", "CityId", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_MarketSnapshots_CityId",
                table: "MarketSnapshots",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketSnapshots_VariantId_CityId_SnapshotDate",
                table: "MarketSnapshots",
                columns: new[] { "VariantId", "CityId", "SnapshotDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId_Name",
                table: "Models",
                columns: new[] { "MakeId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedDate",
                table: "Notifications",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_PriceAlerts_CityId",
                table: "PriceAlerts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceAlerts_IsActive",
                table: "PriceAlerts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_PriceAlerts_UserId_VariantId",
                table: "PriceAlerts",
                columns: new[] { "UserId", "VariantId" });

            migrationBuilder.CreateIndex(
                name: "IX_PriceAlerts_VariantId",
                table: "PriceAlerts",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId_Name",
                table: "Provinces",
                columns: new[] { "CountryId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValuationFactors_ValuationId",
                table: "ValuationFactors",
                column: "ValuationId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_CityId",
                table: "Valuations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_ConditionId",
                table: "Valuations",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_CreatedDate",
                table: "Valuations",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_UserId",
                table: "Valuations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_VariantId",
                table: "Valuations",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Valuations_VehicleId",
                table: "Valuations",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ModelId_Name",
                table: "Variants",
                columns: new[] { "ModelId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleConditions_Name",
                table: "VehicleConditions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ConditionId",
                table: "Vehicles",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VariantId",
                table: "Vehicles",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSpecifications_VariantId",
                table: "VehicleSpecifications",
                column: "VariantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComparisonVehicles");

            migrationBuilder.DropTable(
                name: "DataImports");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "MarketPrices");

            migrationBuilder.DropTable(
                name: "MarketSnapshots");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PriceAlerts");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ValuationFactors");

            migrationBuilder.DropTable(
                name: "VehicleSpecifications");

            migrationBuilder.DropTable(
                name: "Comparisons");

            migrationBuilder.DropTable(
                name: "DataSources");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Valuations");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "VehicleConditions");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Makes");
        }
    }
}
