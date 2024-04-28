using Microsoft.EntityFrameworkCore;

namespace API.FileProcessing.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
        public DbSet<product> Products { get; set; }
    }
}
