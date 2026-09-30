using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSourceCompliance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcquiredDate",
                table: "DataSources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AttributionRequired",
                table: "DataSources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CommercialTrainingAllowed",
                table: "DataSources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CommercialUseAllowed",
                table: "DataSources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "License",
                table: "DataSources",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TermsUrl",
                table: "DataSources",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquiredDate",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "AttributionRequired",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "CommercialTrainingAllowed",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "CommercialUseAllowed",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "License",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "TermsUrl",
                table: "DataSources");
        }
    }
}
