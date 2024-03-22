using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class enum_int1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType1",
                table: "StudentAddresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressType1",
                table: "StudentAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
