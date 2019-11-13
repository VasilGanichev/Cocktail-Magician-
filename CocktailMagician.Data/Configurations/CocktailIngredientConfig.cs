using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailIngredientConfig : IEntityTypeConfiguration<CocktailIngredient>
    {
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            builder
                .Property(c => c.Quantity)
                .IsRequired();

            builder
                .Property(c => c.IngredientID)
                .IsRequired();

            builder
                .Property(c => c.CocktailID)
                .IsRequired();
        }
    }
}
