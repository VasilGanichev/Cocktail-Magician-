using CocktailMagician.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Data.Configurations
{
    public class CoctailReviewConfig : IEntityTypeConfiguration<CocktailReview>
    {
        public void Configure(EntityTypeBuilder<CocktailReview> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(300);
        }
    }
}
