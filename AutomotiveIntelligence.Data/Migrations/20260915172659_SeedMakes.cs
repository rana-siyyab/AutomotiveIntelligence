using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Makes",
                columns: new[] { "MakeId", "Country", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Japan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Toyota", null },
                    { 2, "Japan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Honda", null },
                    { 3, "Japan", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Suzuki", null },
                    { 4, "South Korea", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "KIA", null },
                    { 5, "South Korea", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Hyundai", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "MakeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "MakeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "MakeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "MakeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Makes",
                keyColumn: "MakeId",
                keyValue: 5);
        }
    }
}
