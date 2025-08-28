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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NebrasDB");
        }
    }
}
