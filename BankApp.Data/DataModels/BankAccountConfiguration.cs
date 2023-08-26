using BankApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DataModels
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccount");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Balance);

            // Define relationships
            builder.HasOne(e => e.Customer)
               .WithMany(c => c.BankAccounts)
               .HasForeignKey(e => e.CustomerId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
