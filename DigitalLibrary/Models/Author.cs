using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DigitalLibrary.Models
{
    public class Author : Entity
    {
        public string Name { get; set; }

        /* EF Relations */
        public IEnumerable<Book> Books { get; set; }
    }

    public class AuthroMapping : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.HasMany(a => a.Books)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            builder.ToTable("Authors");
        }
    }
}
