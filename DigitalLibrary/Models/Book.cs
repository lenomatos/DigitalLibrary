using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DigitalLibrary.Models
{
    public class Book : Entity
    {
        public string Name { get; set; }
        public Guid BookGenreId { get; set; }
        public Guid AuthorId { get; set; }
        public string Synopsis { get; set; }

        /* EF Relations */
        public Author Author { get; set; }
        public BookGenre BookGenre { get; set; }
    }

    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.Property(b => b.Synopsis)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.ToTable("Books");
        }
    }
}
