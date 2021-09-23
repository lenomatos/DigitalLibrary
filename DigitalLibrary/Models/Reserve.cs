using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DigitalLibrary.Models
{
    public class Reserve: Entity
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public bool Returned { get; set; }

        /* EF Relations */
        public User User { get; set; }
        public Book Book { get; set; }
    }

    public class ReserveMapping : IEntityTypeConfiguration<Reserve>
    {
        public void Configure(EntityTypeBuilder<Reserve> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Returned)
               .IsRequired()
               .HasColumnType("int");

            builder.ToTable("Reserves");
        }
    }
}
