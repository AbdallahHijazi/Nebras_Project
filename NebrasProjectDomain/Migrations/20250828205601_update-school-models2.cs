using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class updateschoolmodels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GovernortesId",
                table: "Schools",
                newName: "GovernorateId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequirementsMet",
                table: "Schools",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequirementsMet",
                table: "Schools");

            migrationBuilder.RenameColumn(
                name: "GovernorateId",
                table: "Schools",
                newName: "GovernortesId");
        }
    }
}
