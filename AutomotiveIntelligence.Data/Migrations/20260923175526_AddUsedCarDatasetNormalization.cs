using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsedCarDatasetNormalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedCarDatasetNormalizations",
                columns: table => new
                {
                    NormalizationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsedCarDatasetRecordId = table.Column<long>(type: "bigint", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    NormalizationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedMakeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NormalizedModelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NormalizedVariantName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NormalizedLocationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LocationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    YearStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PriceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBasicValuationReady = table.Column<bool>(type: "bit", nullable: false),
                    IsDetailedValuationReady = table.Column<bool>(type: "bit", nullable: false),
                    NormalizationNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedCarDatasetNormalizations", x => x.NormalizationId);
                    table.ForeignKey(
                        name: "FK_UsedCarDatasetNormalizations_UsedCarDatasetRecords_UsedCarDatasetRecordId",
                        column: x => x.UsedCarDatasetRecordId,
                        principalTable: "UsedCarDatasetRecords",
                        principalColumn: "UsedCarDatasetRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetNormalizations_NormalizedLocationName",
                table: "UsedCarDatasetNormalizations",
                column: "NormalizedLocationName");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetNormalizations_NormalizedMakeName",
                table: "UsedCarDatasetNormalizations",
                column: "NormalizedMakeName");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetNormalizations_NormalizedModelName",
                table: "UsedCarDatasetNormalizations",
                column: "NormalizedModelName");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetNormalizations_PriceStatus",
                table: "UsedCarDatasetNormalizations",
                column: "PriceStatus");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetNormalizations_SourceId_NormalizationStatus",
                table: "UsedCarDatasetNormalizations",
                columns: new[] { "SourceId", "NormalizationStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetNormalizations_UsedCarDatasetRecordId",
                table: "UsedCarDatasetNormalizations",
                column: "UsedCarDatasetRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetNormalizations_YearStatus",
                table: "UsedCarDatasetNormalizations",
                column: "YearStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedCarDatasetNormalizations");
        }
    }
}
