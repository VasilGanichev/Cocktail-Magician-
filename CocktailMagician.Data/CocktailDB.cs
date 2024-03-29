﻿using CocktailMagician.Data.Configurations;
using CocktailMagician.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Data
{
    public class CocktailDB : IdentityDbContext<User>
    {
        public CocktailDB(DbContextOptions<CocktailDB> options) : base(options)
        {
        }   
        public DbSet<Bar> Bars { get; set; }
        public DbSet<BarCocktail> BarCocktail { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<BarReview> BarReviews { get; set; }
        public DbSet<CocktailReview> CocktailReviews { get; set; }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BarConfig());
            modelBuilder.ApplyConfiguration(new BarCocktailConfig());
            modelBuilder.ApplyConfiguration(new CocktailConfig());
            modelBuilder.ApplyConfiguration(new CocktailIngredientConfig());
            modelBuilder.ApplyConfiguration(new IngredientConfig());
            modelBuilder.ApplyConfiguration(new BarsReviewsConfig());
            modelBuilder.ApplyConfiguration(new CoctailReviewConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
