using BookStore.Domain.Models.Book;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    internal class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasMany(e => e.Reviews)
                .WithOne(e => e.Book)
                .HasForeignKey(e => e.BookId)
                .IsRequired();

            builder
                .Property(e => e.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder
                .Property(e => e.ISBN)
                .HasMaxLength(13)
                .IsRequired();

            builder
                .HasCheckConstraint("CK_Book_ISBN_MinLength", "LEN(ISBN) >= 10");

            builder
                .Property(t => t.PublicationYear)
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
                .Property(t => t.Description)
                .HasMaxLength(1024)
                .IsRequired(false);
        }
    }
}
