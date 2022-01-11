using EntityLayer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class DataContext:IdentityDbContext<UserAdmin>
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext>options):base(options)
        {

        }
        public DbSet<Advert>Adverts { get; set; }
        public DbSet<Brand>Brands { get; set; }
        public DbSet<City>Cities { get; set; }
        public DbSet<Image>Images { get; set; }
        public DbSet<Model>Models { get; set; }
        public DbSet<State>States { get; set; }
        public DbSet<UserAdmin>UserAdmins { get; set; }
    }
}
