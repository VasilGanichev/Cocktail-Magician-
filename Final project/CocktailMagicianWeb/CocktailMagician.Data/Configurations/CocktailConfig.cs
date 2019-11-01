using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailConfig
    {
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder
                .HasKey(c => c.ID);

            builder
                .Property(c => c.Name)
                .IsRequired();

            builder
                .HasMany(c => c.Bars)
                .WithOne(b => b.Cocktail)
                .HasForeignKey(b => b.CocktailID)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Ingredients)
                .WithOne(i => i.Cocktail)
                .HasForeignKey(i => i.CocktailID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
