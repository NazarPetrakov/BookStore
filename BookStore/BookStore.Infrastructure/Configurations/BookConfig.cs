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
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .IsRequired();

            builder
                .Property(b => b.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder
                .Property(b => b.ISBN)
                .HasMaxLength(13)
                .IsRequired();

            builder
                .HasCheckConstraint("CK_Book_ISBN_MinLength", "LEN(ISBN) >= 10");

            builder
                .Property(b => b.PublicationYear)
                .HasColumnType("date")
                .IsRequired();

            builder
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder
                .Property(b => b.Description)
                .HasMaxLength(1024)
                .IsRequired(false);

        }
    }
}
