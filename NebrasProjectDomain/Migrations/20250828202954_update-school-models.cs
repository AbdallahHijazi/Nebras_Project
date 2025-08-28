using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class updateschoolmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Cities_CityId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "NumberOfClassrooms",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "SchoolStatusId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "StudentCapacity",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "YearEstablished",
                table: "Schools");

            migrationBuilder.RenameColumn(
                name: "SchoolTypeId",
                table: "Schools",
                newName: "GovernortesId");

            migrationBuilder.RenameColumn(
                name: "ConditionDescription",
                table: "Schools",
                newName: "HeadTeacherNumber");

            migrationBuilder.RenameColumn(
                name: "AddressDetails",
                table: "Schools",
                newName: "HeadTeacherName");

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Cities_CityId",
                table: "Schools",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Cities_CityId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Schools");

            migrationBuilder.RenameColumn(
                name: "HeadTeacherNumber",
                table: "Schools",
                newName: "ConditionDescription");

            migrationBuilder.RenameColumn(
                name: "HeadTeacherName",
                table: "Schools",
                newName: "AddressDetails");

            migrationBuilder.RenameColumn(
                name: "GovernortesId",
                table: "Schools",
                newName: "SchoolTypeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Schools",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Schools",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfClassrooms",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolStatusId",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StudentCapacity",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearEstablished",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Cities_CityId",
                table: "Schools",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
