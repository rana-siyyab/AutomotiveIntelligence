using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMarketPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MarketPrices",
                columns: new[] { "MarketPriceId", "CityId", "ConditionId", "CreatedDate", "Mileage", "ObservationDate", "ObservedPrice", "SourceId", "VariantId", "Year" },
                values: new object[,]
                {
                    { 1L, 1, 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7200000m, null, 1, 2022 },
                    { 2L, 1, 2, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 52000, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7000000m, null, 1, 2022 },
                    { 3L, 1, 1, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 30000, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7500000m, null, 1, 2022 },
                    { 4L, 1, 3, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 70000, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6700000m, null, 1, 2022 },
                    { 5L, 1, 2, new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 60000, new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6900000m, null, 1, 2022 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 5L);
        }
    }
}
