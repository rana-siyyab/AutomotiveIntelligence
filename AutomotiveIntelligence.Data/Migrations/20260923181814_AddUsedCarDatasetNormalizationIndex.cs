using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsedCarDatasetNormalizationIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_SourceId_UsedCarDatasetRecordId",
                table: "UsedCarDatasetRecords",
                columns: new[] { "SourceId", "UsedCarDatasetRecordId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsedCarDatasetRecords_SourceId_UsedCarDatasetRecordId",
                table: "UsedCarDatasetRecords");
        }
    }
}
