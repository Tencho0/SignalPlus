using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalPlus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSignalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Signals",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Signals",
                type: "decimal(9,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Signals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Signals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)");
        }
    }
}
