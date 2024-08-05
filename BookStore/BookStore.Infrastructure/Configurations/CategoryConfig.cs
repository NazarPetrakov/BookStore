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
                .Property(c => c.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .HasMany(c => c.BookCategories)
                .WithOne(bc => bc.Category)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
