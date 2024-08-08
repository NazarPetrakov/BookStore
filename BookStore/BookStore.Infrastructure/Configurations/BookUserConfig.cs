using BookStore.Domain.Models.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    public class BookUserConfig : IEntityTypeConfiguration<BookUser>
    {
        public void Configure(EntityTypeBuilder<BookUser> builder)
        {
                builder
                    .HasKey(bu => new { bu.BookId, bu.UserId });
                builder
                    .HasOne(bu => bu.Book)
                    .WithMany(b => b.BookUsers)
                    .HasForeignKey(bu => bu.BookId);
                builder
                    .HasOne(bu => bu.User)
                    .WithMany(u => u.BookUsers)
                    .HasForeignKey(bu => bu.UserId);
        }
    }
}
