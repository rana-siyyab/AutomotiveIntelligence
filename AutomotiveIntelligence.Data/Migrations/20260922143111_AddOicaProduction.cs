using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOicaProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OicaProductions",
                columns: table => new
                {
                    OicaProductionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    ImportId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    ProductionUnits = table.Column<int>(type: "int", nullable: false),
                    ObservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OicaProductions", x => x.OicaProductionId);
                    table.ForeignKey(
                        name: "FK_OicaProductions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OicaProductions_DataImports_ImportId",
                        column: x => x.ImportId,
                        principalTable: "DataImports",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OicaProductions_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OicaProductions_CountryId",
                table: "OicaProductions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_OicaProductions_CountryName_Year",
                table: "OicaProductions",
                columns: new[] { "CountryName", "Year" });

            migrationBuilder.CreateIndex(
                name: "IX_OicaProductions_ImportId",
                table: "OicaProductions",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_OicaProductions_ObservationDate",
                table: "OicaProductions",
                column: "ObservationDate");

            migrationBuilder.CreateIndex(
                name: "IX_OicaProductions_SourceId_CountryName_VehicleType_Year",
                table: "OicaProductions",
                columns: new[] { "SourceId", "CountryName", "VehicleType", "Year" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OicaProductions");
        }
    }
}
