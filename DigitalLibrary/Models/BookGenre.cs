using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Models
{
    public class BookGenre : Entity
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }
    }

    public class BookGenderMapping : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.ToTable("BookGenres");
        }
    }
}
