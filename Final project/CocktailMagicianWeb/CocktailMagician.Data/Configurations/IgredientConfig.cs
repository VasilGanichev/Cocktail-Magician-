﻿using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class IgredientConfig
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder
                .Property(i => i.Name)
                .IsRequired();

            builder
               .Property(i => i.Type)
               .IsRequired();

            builder
                .HasMany(i => i.CocktailIngredients)
                .WithOne(c => c.Ingredient);
        }
    }
}
