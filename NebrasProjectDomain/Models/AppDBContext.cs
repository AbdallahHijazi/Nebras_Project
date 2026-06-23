using Microsoft.EntityFrameworkCore;
using NebrasProjectModels.Models.Citys;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectModels.Models.Photos;
using NebrasProjectModels.Models.Schools;
using NebrasProjectModels.Models.Users;

namespace NebrasProjectDomain.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() { }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolPhoto> SchoolPhotos { get; set; }
        public DbSet<User> Users { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NebrasDB;Username=postgres;Password=postgres");
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Governorate>().HasData(
                new Governorate
                {
                    GovernorateId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    NameAr = "دمشق",
                    NameEn = "Damascus",
                    Description = "محافظة سورية تضم العاصمة دمشق وتعد مركز الإدارة الوطنية.",
                    GovernorateImage = "uploads/governorates/damascus.jpeg",
                    CityCount = 11,
                    SchoolCount = 450
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    NameAr = "ريف دمشق",
                    NameEn = "Rif Dimashq",
                    Description = "محافظة تحيط بمدينة دمشق وتضم عدداً من المدن والبلدات.",
                    GovernorateImage = "uploads/governorates/rifdimashq.jpg",
                    CityCount = 28,
                    SchoolCount = 700
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    NameAr = "حلب",
                    NameEn = "Aleppo",
                    Description = "إحدى أكبر المحافظات السورية من حيث عدد السكان والمساحة.",
                    GovernorateImage = "uploads/governorates/aleppo.jpeg",
                    CityCount = 44,
                    SchoolCount = 1000
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    NameAr = "حمص",
                    NameEn = "Homs",
                    Description = "محافظة تقع في وسط سوريا وتعد حلقة وصل بين الشمال والجنوب.",
                    GovernorateImage = "uploads/governorates/homs.jpg",
                    CityCount = 25,
                    SchoolCount = 650
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    NameAr = "حماة",
                    NameEn = "Hama",
                    Description = "محافظة في وسط سوريا وتضم مدناً وبلدات متعددة.",
                    GovernorateImage = "uploads/governorates/hama.jpg",
                    CityCount = 22,
                    SchoolCount = 450
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    NameAr = "اللاذقية",
                    NameEn = "Latakia",
                    Description = "محافظة ساحلية تقع على البحر المتوسط شمال غرب سوريا.",
                    GovernorateImage = "uploads/governorates/latakia.jpg",
                    CityCount = 22,
                    SchoolCount = 400
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    NameAr = "طرطوس",
                    NameEn = "Tartus",
                    Description = "محافظة ساحلية جنوب اللاذقية وتطل على البحر المتوسط.",
                    GovernorateImage = "uploads/governorates/tartus.jpg",
                    CityCount = 12,
                    SchoolCount = 250
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    NameAr = "إدلب",
                    NameEn = "Idlib",
                    Description = "محافظة تقع شمال غرب سوريا وتضم عدداً من المدن الرئيسية.",
                    GovernorateImage = "uploads/governorates/idlib.jpeg",
                    CityCount = 26,
                    SchoolCount = 300
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    NameAr = "درعا",
                    NameEn = "Daraa",
                    Description = "محافظة جنوب سوريا وتشكل منفذاً حدودياً مع الأردن.",
                    GovernorateImage = "uploads/governorates/daraa.jpg",
                    CityCount = 20,
                    SchoolCount = 350
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    NameAr = "السويداء",
                    NameEn = "As-Suwayda",
                    Description = "محافظة تقع جنوب سوريا وتضم العديد من القرى والبلدات.",
                    GovernorateImage = "uploads/governorates/suwayda.jpg",
                    CityCount = 14,
                    SchoolCount = 150
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    NameAr = "دير الزور",
                    NameEn = "Deir ez-Zor",
                    Description = "محافظة شرقية تقع على ضفاف نهر الفرات.",
                    GovernorateImage = "uploads/governorates/deirezor.jpg",
                    CityCount = 14,
                    SchoolCount = 300
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    NameAr = "الرقة",
                    NameEn = "Raqqa",
                    Description = "محافظة شمال وسط سوريا تقع على نهر الفرات.",
                    GovernorateImage = "uploads/governorates/raqqa.jpg",
                    CityCount = 12,
                    SchoolCount = 200
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    NameAr = "الحسكة",
                    NameEn = "Al-Hasakah",
                    Description = "محافظة تقع شمال شرق سوريا وتضم مدناً رئيسية.",
                    GovernorateImage = "uploads/governorates/hasakah.jpg",
                    CityCount = 23,
                    SchoolCount = 350
                },
                new Governorate
                {
                    GovernorateId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    NameAr = "القنيطرة",
                    NameEn = "Quneitra",
                    Description = "محافظة صغيرة تقع جنوب غرب سوريا قرب الجولان.",
                    GovernorateImage = "uploads/governorates/quneitra.jpg",
                    CityCount = 5,
                    SchoolCount = 80
                }
            );
        }
    }
}
