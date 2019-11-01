using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailIngredientConfig
    {
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            builder
                .Property(c => c.Quantity)
                .IsRequired();

            builder
                .Property(c => c.IngredienetID)
                .IsRequired();

            builder
                .Property(c => c.CocktailID)
                .IsRequired();
        }
    }
}
