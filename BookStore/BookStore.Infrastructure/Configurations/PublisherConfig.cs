using BookStore.Domain.Models.Publisher;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    internal class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder
                .HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId)
                .IsRequired(false);

            builder
                .Property(p => p.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(p => p.Address)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
