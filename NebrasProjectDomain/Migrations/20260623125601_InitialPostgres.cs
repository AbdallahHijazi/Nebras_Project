using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    GovernorateId = table.Column<Guid>(type: "uuid", nullable: false),
                    NameAr = table.Column<string>(type: "text", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    GovernorateImage = table.Column<string>(type: "text", nullable: false),
                    CityCount = table.Column<int>(type: "integer", nullable: false),
                    SchoolCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.GovernorateId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    NameAr = table.Column<string>(type: "text", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    GovernorateId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolCount = table.Column<int>(type: "integer", nullable: false),
                    CityImage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "GovernorateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<Guid>(type: "uuid", nullable: false),
                    NameAr = table.Column<string>(type: "text", nullable: false),
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EstimatedRenovationCost = table.Column<decimal>(type: "numeric", nullable: false),
                    AddedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApprovedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    GovernorateId = table.Column<Guid>(type: "uuid", nullable: false),
                    HeadTeacherName = table.Column<string>(type: "text", nullable: false),
                    HeadTeacherNumber = table.Column<string>(type: "text", nullable: false),
                    IsRequirementsMet = table.Column<bool>(type: "boolean", nullable: false),
                    Needs = table.Column<List<string>>(type: "text[]", nullable: false),
                    SchoolImageUrl = table.Column<string>(type: "text", nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                    table.ForeignKey(
                        name: "FK_Schools_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Schools_Governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "Governorates",
                        principalColumn: "GovernorateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schools_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Schools_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "SchoolPhotos",
                columns: table => new
                {
                    PhotoId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotoUrl = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsCoverPhoto = table.Column<bool>(type: "boolean", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPhotos", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_SchoolPhotos_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "GovernorateId", "CityCount", "Description", "GovernorateImage", "NameAr", "NameEn", "SchoolCount" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 11, "محافظة سورية تضم العاصمة دمشق وتعد مركز الإدارة الوطنية.", "uploads/governorates/damascus.jpeg", "دمشق", "Damascus", 450 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 28, "محافظة تحيط بمدينة دمشق وتضم عدداً من المدن والبلدات.", "uploads/governorates/rifdimashq.jpg", "ريف دمشق", "Rif Dimashq", 700 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 44, "إحدى أكبر المحافظات السورية من حيث عدد السكان والمساحة.", "uploads/governorates/aleppo.jpeg", "حلب", "Aleppo", 1000 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 25, "محافظة تقع في وسط سوريا وتعد حلقة وصل بين الشمال والجنوب.", "uploads/governorates/homs.jpg", "حمص", "Homs", 650 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 22, "محافظة في وسط سوريا وتضم مدناً وبلدات متعددة.", "uploads/governorates/hama.jpg", "حماة", "Hama", 450 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 22, "محافظة ساحلية تقع على البحر المتوسط شمال غرب سوريا.", "uploads/governorates/latakia.jpg", "اللاذقية", "Latakia", 400 },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 12, "محافظة ساحلية جنوب اللاذقية وتطل على البحر المتوسط.", "uploads/governorates/tartus.jpg", "طرطوس", "Tartus", 250 },
                    { new Guid("88888888-8888-8888-8888-888888888888"), 26, "محافظة تقع شمال غرب سوريا وتضم عدداً من المدن الرئيسية.", "uploads/governorates/idlib.jpeg", "إدلب", "Idlib", 300 },
                    { new Guid("99999999-9999-9999-9999-999999999999"), 20, "محافظة جنوب سوريا وتشكل منفذاً حدودياً مع الأردن.", "uploads/governorates/daraa.jpg", "درعا", "Daraa", 350 },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 14, "محافظة تقع جنوب سوريا وتضم العديد من القرى والبلدات.", "uploads/governorates/suwayda.jpg", "السويداء", "As-Suwayda", 150 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 14, "محافظة شرقية تقع على ضفاف نهر الفرات.", "uploads/governorates/deirezor.jpg", "دير الزور", "Deir ez-Zor", 300 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 12, "محافظة شمال وسط سوريا تقع على نهر الفرات.", "uploads/governorates/raqqa.jpg", "الرقة", "Raqqa", 200 },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 23, "محافظة تقع شمال شرق سوريا وتضم مدناً رئيسية.", "uploads/governorates/hasakah.jpg", "الحسكة", "Al-Hasakah", 350 },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 5, "محافظة صغيرة تقع جنوب غرب سوريا قرب الجولان.", "uploads/governorates/quneitra.jpg", "القنيطرة", "Quneitra", 80 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_GovernorateId",
                table: "Cities",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolPhotos_SchoolId",
                table: "SchoolPhotos",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_CityId",
                table: "Schools",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_GovernorateId",
                table: "Schools",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UserId",
                table: "Schools",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_UserId1",
                table: "Schools",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolPhotos");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Governorates");
        }
    }
}
