using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsedCarDatasetRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsedCarDatasetRecords_SourceId",
                table: "UsedCarDatasetRecords");

            migrationBuilder.AddColumn<string>(
                name: "AssemblyType",
                table: "UsedCarDatasetRecords",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "UsedCarDatasetRecords",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "UsedCarDatasetRecords",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "UsedCarDatasetRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceName",
                table: "UsedCarDatasetRecords",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RawLocationName",
                table: "UsedCarDatasetRecords",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "UsedCarDatasetRecords",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_SourceId_ExternalRecordId",
                table: "UsedCarDatasetRecords",
                columns: new[] { "SourceId", "ExternalRecordId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsedCarDatasetRecords_SourceId_ExternalRecordId",
                table: "UsedCarDatasetRecords");

            migrationBuilder.DropColumn(
                name: "AssemblyType",
                table: "UsedCarDatasetRecords");

            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "UsedCarDatasetRecords");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "UsedCarDatasetRecords");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "UsedCarDatasetRecords");

            migrationBuilder.DropColumn(
                name: "ProvinceName",
                table: "UsedCarDatasetRecords");

            migrationBuilder.DropColumn(
                name: "RawLocationName",
                table: "UsedCarDatasetRecords");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "UsedCarDatasetRecords");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCarDatasetRecords_SourceId",
                table: "UsedCarDatasetRecords",
                column: "SourceId");
        }
    }
}
