using IdentityAndCms.CMS;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndCms.Models
{
    public class CmsDbContext : IdentityDbContext<User>
    {
        public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options)
        { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Person> People { get; set; }
    }
}