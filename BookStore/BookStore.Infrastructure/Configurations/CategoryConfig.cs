using BookStore.Domain.Models.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .HasMany(e => e.BookCategories)
                .WithOne(e => e.Category)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
