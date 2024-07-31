using BookStore.Domain.Models.Author;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    internal class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .Property(e => e.FirstName)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(e => e.LastName)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(e => e.Bio)
                .HasMaxLength(512)
                .IsRequired(false);

            builder
                .HasMany(e => e.BookAuthors)
                .WithOne(e => e.Author)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
