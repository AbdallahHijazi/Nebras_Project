using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class updateschoolmodels3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Needs",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Needs",
                table: "Schools");
        }
    }
}
