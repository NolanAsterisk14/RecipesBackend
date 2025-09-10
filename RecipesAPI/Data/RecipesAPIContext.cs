using Microsoft.EntityFrameworkCore;
using RecipesAPI.Models;

namespace RecipesAPI.Data
{
    public class RecipesAPIContext : DbContext
    {
        public RecipesAPIContext(DbContextOptions<RecipesAPIContext> options) : base(options) { }
        public DbSet<Recipe> Recipes { get; set; }
    }
}