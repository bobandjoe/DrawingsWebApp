
using DrawingsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawingsWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Drawing> Drawings { get; set; }
    }
}
