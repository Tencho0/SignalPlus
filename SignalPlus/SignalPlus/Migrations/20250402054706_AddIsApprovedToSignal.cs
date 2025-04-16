using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalPlus.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovedToSignal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Signals",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Signals");
        }
    }
}
