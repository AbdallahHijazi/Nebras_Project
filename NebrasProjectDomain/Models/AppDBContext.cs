using Microsoft.EntityFrameworkCore;
using NebrasProjectModels.Models.Citys;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectModels.Models.Photos;
using NebrasProjectModels.Models.RenovationTypes;
using NebrasProjectModels.Models.Roles;
using NebrasProjectModels.Models.SchoolRequiredRenovations;
using NebrasProjectModels.Models.Schools;
using NebrasProjectModels.Models.SchoolStatues;
using NebrasProjectModels.Models.SchoolTypes;
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
        public DbSet<SchoolType> SchoolTypes { get; set; }
        public DbSet<SchoolStatus> SchoolStatuses { get; set; }
        public DbSet<RenovationType> RenovationTypes { get; set; }
        public DbSet<SchoolRequiredRenovation> SchoolRequiredRenovations { get; set; }
        public DbSet<SchoolPhoto> SchoolPhotos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NebrasDB");
        }
    }
}
