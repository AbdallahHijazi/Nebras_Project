using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NebrasProjectDomain.Migrations
{
    /// <inheritdoc />
    public partial class SeedGovernorates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "GovernorateId",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));
        }
    }
}
