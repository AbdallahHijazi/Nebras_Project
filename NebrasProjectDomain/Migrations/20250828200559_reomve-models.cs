using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class reomvemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_SchoolStatuses_SchoolStatusId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_SchoolTypes_SchoolTypeId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SchoolRequiredRenovations");

            migrationBuilder.DropTable(
                name: "SchoolStatuses");

            migrationBuilder.DropTable(
                name: "SchoolTypes");

            migrationBuilder.DropTable(
                name: "RenovationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Schools_SchoolStatusId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_SchoolTypeId",
                table: "Schools");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RenovationTypes",
                columns: table => new
                {
                    RenovationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationTypes", x => x.RenovationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolStatuses",
                columns: table => new
                {
                    SchoolStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolStatuses", x => x.SchoolStatusId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolTypes",
                columns: table => new
                {
                    SchoolTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTypes", x => x.SchoolTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolRequiredRenovations",
                columns: table => new
                {
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RenovationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolRequiredRenovations", x => x.SchoolId);
                    table.ForeignKey(
                        name: "FK_SchoolRequiredRenovations_RenovationTypes_RenovationTypeId",
                        column: x => x.RenovationTypeId,
                        principalTable: "RenovationTypes",
                        principalColumn: "RenovationTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolRequiredRenovations_Schools_SchoolId1",
                        column: x => x.SchoolId1,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_SchoolStatusId",
                table: "Schools",
                column: "SchoolStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_SchoolTypeId",
                table: "Schools",
                column: "SchoolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolRequiredRenovations_RenovationTypeId",
                table: "SchoolRequiredRenovations",
                column: "RenovationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolRequiredRenovations_SchoolId1",
                table: "SchoolRequiredRenovations",
                column: "SchoolId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_SchoolStatuses_SchoolStatusId",
                table: "Schools",
                column: "SchoolStatusId",
                principalTable: "SchoolStatuses",
                principalColumn: "SchoolStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_SchoolTypes_SchoolTypeId",
                table: "Schools",
                column: "SchoolTypeId",
                principalTable: "SchoolTypes",
                principalColumn: "SchoolTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
