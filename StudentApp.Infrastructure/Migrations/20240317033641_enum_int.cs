using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class enum_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AddressType",
                table: "StudentAddresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddColumn<int>(
                name: "AddressType1",
                table: "StudentAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType1",
                table: "StudentAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "AddressType",
                table: "StudentAddresses",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
