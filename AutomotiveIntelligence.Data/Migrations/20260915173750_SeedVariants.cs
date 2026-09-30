using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "VariantId", "CreatedDate", "DriveType", "EngineCapacity", "FuelType", "IsActive", "ModelId", "Name", "SeatingCapacity", "Transmission", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1800, "Petrol", true, 1, "Altis X", 5, "Automatic", null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1800, "Petrol", true, 1, "Altis Grande", 5, "Automatic", null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1300, "Petrol", true, 2, "GLI", 5, "Manual", null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1300, "Petrol", true, 2, "ATIV", 5, "Automatic", null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4WD", 2700, "Petrol", true, 3, "2.7 V", 7, "Automatic", null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1500, "Petrol", true, 4, "1.5 Turbo", 5, "CVT", null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1200, "Petrol", true, 5, "1.2", 5, "Manual", null },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 660, "Petrol", true, 6, "VXR", 4, "Manual", null },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 660, "Petrol", true, 6, "VXL AGS", 4, "AMT", null },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 998, "Petrol", true, 7, "VXL", 5, "Manual", null },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1200, "Petrol", true, 8, "GL", 5, "Manual", null },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 2000, "Petrol", true, 9, "FWD", 5, "Automatic", null },
                    { 13, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 1000, "Petrol", true, 10, "1.0", 5, "Automatic", null },
                    { 14, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 2000, "Petrol", true, 11, "FWD", 5, "Automatic", null },
                    { 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FWD", 2000, "Petrol", true, 12, "GL", 5, "Automatic", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Variants",
                keyColumn: "VariantId",
                keyValue: 15);
        }
    }
}
