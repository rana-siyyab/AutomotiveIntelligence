using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPamaProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PamaProductions",
                columns: table => new
                {
                    PamaProductionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    ImportId = table.Column<long>(type: "bigint", nullable: true),
                    MakeId = table.Column<int>(type: "int", nullable: true),
                    ManufacturerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ProductionUnits = table.Column<int>(type: "int", nullable: false),
                    SalesUnits = table.Column<int>(type: "int", nullable: false),
                    ObservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PamaProductions", x => x.PamaProductionId);
                    table.ForeignKey(
                        name: "FK_PamaProductions_DataImports_ImportId",
                        column: x => x.ImportId,
                        principalTable: "DataImports",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PamaProductions_DataSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "DataSources",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PamaProductions_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "MakeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PamaProductions_ImportId",
                table: "PamaProductions",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_PamaProductions_MakeId_Year_Month",
                table: "PamaProductions",
                columns: new[] { "MakeId", "Year", "Month" });

            migrationBuilder.CreateIndex(
                name: "IX_PamaProductions_ObservationDate",
                table: "PamaProductions",
                column: "ObservationDate");

            migrationBuilder.CreateIndex(
                name: "IX_PamaProductions_SourceId_Year_Month",
                table: "PamaProductions",
                columns: new[] { "SourceId", "Year", "Month" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PamaProductions");
        }
    }
}
