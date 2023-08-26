using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BankApp.Domain.Models;

namespace BankApp.Data.DataModels
{
    public partial class BankAppDBContext : DbContext
    {
        public BankAppDBContext()
        {
        }

        public BankAppDBContext(DbContextOptions<BankAppDBContext> options)
            : base(options)
        {
        }

       // public DbSet<Customer> Customers => Set<Customer>();
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<BankAccount> BankAccounts { get; set; } = null!;
        public virtual DbSet<Transactions> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionsConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
