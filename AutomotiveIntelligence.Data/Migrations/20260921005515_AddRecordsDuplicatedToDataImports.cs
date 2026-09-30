using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomotiveIntelligence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRecordsDuplicatedToDataImports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "DataImports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ErrorLog",
                table: "DataImports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordsDuplicated",
                table: "DataImports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DataImports_StartedDate",
                table: "DataImports",
                column: "StartedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DataImports_StartedDate",
                table: "DataImports");

            migrationBuilder.DropColumn(
                name: "RecordsDuplicated",
                table: "DataImports");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "DataImports",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ErrorLog",
                table: "DataImports",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
