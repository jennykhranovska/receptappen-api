using Microsoft.EntityFrameworkCore;
using receptappen_api.Models;

namespace receptappen_api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
    }
}
