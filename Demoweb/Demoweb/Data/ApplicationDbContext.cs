using Demoweb.Models;
using Microsoft.EntityFrameworkCore;

namespace Demoweb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Demo> Demos { get; set; }
        public DbSet<Demoweb.Models.UserView> UserView { get; set; }
    }
}
