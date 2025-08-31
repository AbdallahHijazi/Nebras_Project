using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class updateschoolemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolImageUrl",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_GovernorateId",
                table: "Schools",
                column: "GovernorateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Governorates_GovernorateId",
                table: "Schools",
                column: "GovernorateId",
                principalTable: "Governorates",
                principalColumn: "GovernorateId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Governorates_GovernorateId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_GovernorateId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "SchoolImageUrl",
                table: "Schools");
        }
    }
}
