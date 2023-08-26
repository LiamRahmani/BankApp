using BankApp.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Data.DataModels
{
    public class TransactionsConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.ToTable("Transactions");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Amount);

            builder.Property(e => e.TransactionDate);

            // Define relationship
            builder.HasOne(e => e.BankAccount)
                .WithMany(b => b.Transactions)
                .HasForeignKey(e => e.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
