using Microsoft.EntityFrameworkCore;
using WithImageWeb.Models;

namespace WithImageWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ImageModel> Images { get; set; }
    }
}
