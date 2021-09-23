using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Models
{
    public class User : Entity
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(32, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Password { get; set; }
        public bool Administrator { get; set; }
    }

    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.Property(b => b.Password)
                .IsRequired()
                .HasColumnType("nvarchar(32)");

            builder.Property(b => b.Administrator)
               .IsRequired()
               .HasColumnType("int");

            builder.ToTable("Users");
        }
    }
}
