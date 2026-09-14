using Microsoft.EntityFrameworkCore;
using receptappen_api.Models;

namespace receptappen_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
    }
}
