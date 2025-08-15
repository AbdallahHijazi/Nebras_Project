using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class addnewproptogovcity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityCount",
                table: "Governorates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolCount",
                table: "Governorates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CityImage",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SchoolCount",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityCount",
                table: "Governorates");

            migrationBuilder.DropColumn(
                name: "SchoolCount",
                table: "Governorates");

            migrationBuilder.DropColumn(
                name: "CityImage",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "SchoolCount",
                table: "Cities");
        }
    }
}
