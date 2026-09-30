using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMarketPriceSourceRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MarketPrices_SourceId",
                table: "MarketPrices");

            migrationBuilder.AddColumn<long>(
                name: "SourceRecordId",
                table: "MarketPrices",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 1L,
                column: "SourceRecordId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 2L,
                column: "SourceRecordId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 3L,
                column: "SourceRecordId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 4L,
                column: "SourceRecordId",
                value: null);

            migrationBuilder.UpdateData(
                table: "MarketPrices",
                keyColumn: "MarketPriceId",
                keyValue: 5L,
                column: "SourceRecordId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrices_SourceId_SourceRecordId",
                table: "MarketPrices",
                columns: new[] { "SourceId", "SourceRecordId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MarketPrices_SourceId_SourceRecordId",
                table: "MarketPrices");

            migrationBuilder.DropColumn(
                name: "SourceRecordId",
                table: "MarketPrices");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrices_SourceId",
                table: "MarketPrices",
                column: "SourceId");
        }
    }
}
