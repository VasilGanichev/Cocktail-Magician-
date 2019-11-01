using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class BarCocktailConfig : IEntityTypeConfiguration<BarCocktail>
    {
        public void Configure(EntityTypeBuilder<BarCocktail> builder)
        {
            builder
                .Property(b => b.BarID)
                .IsRequired();

            builder
                .Property(b => b.CocktailID)
                .IsRequired();

            builder
                .HasOne(b => b.Bar)
                .WithMany(b => b.BarCocktails);
        }
    }
}
