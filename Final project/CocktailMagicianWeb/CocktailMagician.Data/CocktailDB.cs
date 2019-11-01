using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Data
{
    public class CocktailDB : DbContext
    {
        public CocktailDB(DbContextOptions options) : base(options)
        {
        }

        //public CocktailDB(DbContextOptions<CocktailDB> options) : base(options)
        //{
        //}
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
