using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectModels.Models.Schools;
using NebrasProjectModels.Models.Users;

namespace NebrasProjectDomain.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() { }
        //Repository
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<School> Schools { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NebrasDB");
        }
    }
}
