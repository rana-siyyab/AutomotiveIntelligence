using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "Code", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[] { 1, "PK", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Pakistan", null });

            migrationBuilder.InsertData(
                table: "VehicleConditions",
                columns: new[] { "ConditionId", "CreatedDate", "Description", "IsActive", "Name", "Score", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Very well maintained vehicle with minimal signs of use.", true, "Excellent", 95m, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Well maintained vehicle with normal signs of use.", true, "Good", 85m, null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vehicle with normal wear and some maintenance requirements.", true, "Average", 70m, null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vehicle with noticeable wear and maintenance requirements.", true, "Fair", 55m, null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vehicle requiring significant maintenance or repairs.", true, "Poor", 35m, null }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceId", "CountryId", "CreatedDate", "IsActive", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Punjab", null },
                    { 2, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sindh", null },
                    { 3, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Khyber Pakhtunkhwa", null },
                    { 4, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Balochistan", null },
                    { 5, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Islamabad Capital Territory", null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "CreatedDate", "IsActive", "Name", "ProvinceId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Lahore", 1, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Rawalpindi", 1, null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Faisalabad", 1, null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Multan", 1, null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Gujranwala", 1, null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Karachi", 2, null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Hyderabad", 2, null },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Peshawar", 3, null },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Abbottabad", 3, null },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Quetta", 4, null },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Islamabad", 5, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "VehicleConditions",
                keyColumn: "ConditionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleConditions",
                keyColumn: "ConditionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VehicleConditions",
                keyColumn: "ConditionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VehicleConditions",
                keyColumn: "ConditionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VehicleConditions",
                keyColumn: "ConditionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 1);
        }
    }
}
