using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalLibrary.Models
{
    public class User : Entity
    {
        public string Email { get; set; }
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
