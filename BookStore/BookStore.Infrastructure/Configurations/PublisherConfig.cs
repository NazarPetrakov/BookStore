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
                .HasMany(e => e.Books)
                .WithOne(e => e.Publisher)
                .HasForeignKey(e => e.PublisherId)
                .IsRequired(false);

            builder
                .Property(e => e.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(e => e.Address)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
