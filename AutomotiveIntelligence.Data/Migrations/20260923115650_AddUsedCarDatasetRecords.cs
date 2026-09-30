using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsedCarDatasetRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedCarDatasetRecords",
                columns: table => new
                {
                    UsedCarDatasetRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    ImportId = table.Column<long>(type: "bigint", nullable: true),
                    ExternalRecordId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RawVehicleName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MakeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ModelName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VariantName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ManufacturingYear = table.Column<int>(type: "int", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mileage = table.Column<int>(type: "int", nullable: true),
                    EngineCapacity = table.Column<int>(type: "int", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AskingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ListingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    RawDataJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedCarDatasetRecords", x => x.UsedCarDatasetRecordId);
                    table.ForeignKey(
                        name: "FK_UsedCarDatasetRecords_DataImports_ImportId",
                        column: x => x.ImportId,
                        principalTable: "DataImports",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsedCarDatasetRecords_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_AskingPrice",
                table: "UsedCarDatasetRecords",
                column: "AskingPrice");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_CityName",
                table: "UsedCarDatasetRecords",
                column: "CityName");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_ImportId",
                table: "UsedCarDatasetRecords",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_MakeName",
                table: "UsedCarDatasetRecords",
                column: "MakeName");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_ManufacturingYear",
                table: "UsedCarDatasetRecords",
                column: "ManufacturingYear");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_ModelName",
                table: "UsedCarDatasetRecords",
                column: "ModelName");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_SourceId",
                table: "UsedCarDatasetRecords",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedCarDatasetRecords");
        }
    }
}
