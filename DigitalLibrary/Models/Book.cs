using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Models
{
    public class Book : Entity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid BookGenreId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
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
