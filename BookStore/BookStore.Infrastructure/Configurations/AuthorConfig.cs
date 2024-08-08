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
                .Property(a => a.FirstName)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(a => a.LastName)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(a => a.Bio)
                .HasMaxLength(512)
                .IsRequired(false);
        }
    }
}
