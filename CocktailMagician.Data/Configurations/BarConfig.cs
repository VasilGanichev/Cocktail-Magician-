using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocktailMagician.Data.Configurations
{
    public class BarConfig : IEntityTypeConfiguration<Bar>
    {
        public void Configure(EntityTypeBuilder<Bar> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired();

            builder.Property(b => b.Address)
                .IsRequired();

            builder.Property(b => b.PhoneNumber)
                .IsRequired();

            builder.Property(b => b.Picture);

            builder.Property(b => b.IsHidden)
                .IsRequired();

            builder.HasMany(b => b.BarReviews)
                .WithOne(r => r.Bar)
                .HasForeignKey(r => r.BarId);
        }
    }
}
