using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class changeStructureOfReceivedDataFromCentral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pr",
                table: "Parameters");

            migrationBuilder.RenameColumn(
                name: "Lead",
                table: "EcgSignals",
                newName: "EcgLead");

            migrationBuilder.RenameColumn(
                name: "Filter",
                table: "EcgSignals",
                newName: "EcgFilter");

            migrationBuilder.AlterColumn<int>(
                name: "CentralId",
                table: "EcgSignals",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EcgLead",
                table: "EcgSignals",
                newName: "Lead");

            migrationBuilder.RenameColumn(
                name: "EcgFilter",
                table: "EcgSignals",
                newName: "Filter");

            migrationBuilder.AddColumn<string>(
                name: "Pr",
                table: "Parameters",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CentralId",
                table: "EcgSignals",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);
        }
    }
}
