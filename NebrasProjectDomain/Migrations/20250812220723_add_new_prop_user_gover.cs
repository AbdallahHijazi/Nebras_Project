using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class add_new_prop_user_gover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Governorates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GovernorateImage",
                table: "Governorates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Governorates");

            migrationBuilder.DropColumn(
                name: "GovernorateImage",
                table: "Governorates");
        }
    }
}
