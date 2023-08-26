using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankApp.Domain.Models;

namespace BankApp.Data.DataModels
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.Property(e => e.CustomerId).HasColumnName("CustomerId");

            builder.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.Givenname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.TelephoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

            builder.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);
        }
    }
}
