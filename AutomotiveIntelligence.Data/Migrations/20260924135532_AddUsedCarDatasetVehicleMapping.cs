using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsedCarDatasetVehicleMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedCarDatasetVehicleMappings",
                columns: table => new
                {
                    MappingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsedCarDatasetRecordId = table.Column<long>(type: "bigint", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    MakeId = table.Column<int>(type: "int", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    MappingStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MappingMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    MappingNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MappedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedCarDatasetVehicleMappings", x => x.MappingId);
                    table.ForeignKey(
                        name: "FK_UsedCarDatasetVehicleMappings_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "MakeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsedCarDatasetVehicleMappings_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsedCarDatasetVehicleMappings_UsedCarDatasetRecords_UsedCarDatasetRecordId",
                        column: x => x.UsedCarDatasetRecordId,
                        principalTable: "UsedCarDatasetRecords",
                        principalColumn: "UsedCarDatasetRecordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsedCarDatasetVehicleMappings_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetVehicleMappings_ConfidenceScore",
                table: "UsedCarDatasetVehicleMappings",
                column: "ConfidenceScore");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetVehicleMappings_MakeId",
                table: "UsedCarDatasetVehicleMappings",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetVehicleMappings_ModelId",
                table: "UsedCarDatasetVehicleMappings",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetVehicleMappings_SourceId_MappingStatus",
                table: "UsedCarDatasetVehicleMappings",
                columns: new[] { "SourceId", "MappingStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetVehicleMappings_UsedCarDatasetRecordId",
                table: "UsedCarDatasetVehicleMappings",
                column: "UsedCarDatasetRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetVehicleMappings_VariantId",
                table: "UsedCarDatasetVehicleMappings",
                column: "VariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedCarDatasetVehicleMappings");
        }
    }
}
