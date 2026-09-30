using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddValuationAssessmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DealAssessment",
                table: "Valuations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RecommendedBuyingPrice",
                table: "Valuations",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RecommendedNegotiationPrice",
                table: "Valuations",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealAssessment",
                table: "Valuations");

            migrationBuilder.DropColumn(
                name: "RecommendedBuyingPrice",
                table: "Valuations");

            migrationBuilder.DropColumn(
                name: "RecommendedNegotiationPrice",
                table: "Valuations");
        }
    }
}
