using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "ModelId", "BodyType", "CreatedDate", "IsActive", "MakeId", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Sedan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "Corolla", null },
                    { 2, "Sedan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "Yaris", null },
                    { 3, "SUV", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "Fortuner", null },
                    { 4, "Sedan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, "Civic", null },
                    { 5, "Sedan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, "City", null },
                    { 6, "Hatchback", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, "Alto", null },
                    { 7, "Hatchback", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, "Cultus", null },
                    { 8, "Hatchback", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, "Swift", null },
                    { 9, "SUV", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, "Sportage", null },
                    { 10, "Hatchback", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, "Picanto", null },
                    { 11, "SUV", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5, "Tucson", null },
                    { 12, "Sedan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 5, "Elantra", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Models",
                keyColumn: "ModelId",
                keyValue: 12);
        }
    }
}
