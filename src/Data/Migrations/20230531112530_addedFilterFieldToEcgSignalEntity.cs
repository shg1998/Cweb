using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addedFilterFieldToEcgSignalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EcgSignals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<long>(type: "bigint", nullable: false),
                    CentralId = table.Column<byte>(type: "tinyint", nullable: false),
                    BedId = table.Column<byte>(type: "tinyint", nullable: false),
                    Lead = table.Column<byte>(type: "tinyint", nullable: false),
                    Filter = table.Column<byte>(type: "tinyint", nullable: false),
                    SignalData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcgSignals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EcgSignals");
        }
    }
}
