﻿using BookStore.Domain.Models.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    internal class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .Property(r => r.Rating)
                .IsRequired();
            builder
                .HasCheckConstraint("CK_Review_Rating_Range", "Rating BETWEEN 1 AND 10");

            builder
                .Property(r => r.Comment)
                .HasMaxLength(1024)
                .IsRequired(false);

            builder
                .Property(r => r.ReviewDate)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
