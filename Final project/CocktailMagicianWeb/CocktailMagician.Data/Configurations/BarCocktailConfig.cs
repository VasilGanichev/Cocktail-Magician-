using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class BarCocktailConfig
    {
        public void Configure(EntityTypeBuilder<BarCocktail> builder)
        {
            builder
                .Property(b => b.BarID)
                .IsRequired();

            builder
                .Property(b => b.Bar)
                .IsRequired();

            builder
                .Property(b => b.CocktailID)
                .IsRequired();

            builder
                .Property(b => b.Cocktail)
                .IsRequired();

            builder
                .HasOne(b => b.Bar)
                .WithMany(b => b.BarCocktails);
        }
    }
}
