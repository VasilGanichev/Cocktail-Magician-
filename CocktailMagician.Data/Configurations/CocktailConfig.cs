using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class CocktailConfig : IEntityTypeConfiguration<Cocktail>
    {
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder
                .HasKey(c => c.Id);

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
            builder
                .HasMany(c => c.CocktailReviews)
                .WithOne(r => r.Cocktail)
                .HasForeignKey(b => b.CocktailId);
        }
    }
}
