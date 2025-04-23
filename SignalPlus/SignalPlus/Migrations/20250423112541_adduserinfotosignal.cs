using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalPlus.Migrations
{
    /// <inheritdoc />
    public partial class adduserinfotosignal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNameVisible",
                table: "Signals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SenderEmail",
                table: "Signals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Signals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderPhone",
                table: "Signals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNameVisible",
                table: "Signals");

            migrationBuilder.DropColumn(
                name: "SenderEmail",
                table: "Signals");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Signals");

            migrationBuilder.DropColumn(
                name: "SenderPhone",
                table: "Signals");
        }
    }
}
