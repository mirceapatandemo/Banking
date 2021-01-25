using Banking.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Banking.Api.DbContexts
{
    public class BankingContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public BankingContext(DbContextOptions<BankingContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"),
                    Product = "Betaalrekening",
                    Iban = "NL69INGB0123456789",
                    Name = "Hr A van Dijk , Mw B Mol-van Dijk",
                    Currency = "EUR"
                },
                new Account
                {
                    AccountId = Guid.Parse("cbb0bc3e-a583-4e73-a2e1-d3387f84cf87"),
                    Product = "Betaalrekening",
                    Iban = "NL77INGB0777777777",
                    Name = "George Joe",
                    Currency = "EUR"
                });

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Food"},
                new Category { CategoryId = 2, Name = "Entertainment" },
                new Category { CategoryId = 3, Name = "Clothing" },
                new Category { CategoryId = 4, Name = "Travel" },
                new Category { CategoryId = 5, Name = "Medical expenses" }
                );

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.TransactionDate);                
            modelBuilder.Entity<Transaction>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Transaction>()
                .HasData(
                    new Transaction
                    {
                        TransactionId = 1,
                        AccountId = Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"),
                        Amount = 1000.00m,
                        CategoryId = 1,
                        TransactionDate = DateTime.Parse("2020-12-20")
                    },
                    new Transaction
                    {
                        TransactionId = 2,
                        AccountId = Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"),
                        Amount = 20.00m,
                        CategoryId = 1,
                        TransactionDate = DateTime.Parse("2021-01-05")
                    },
                    new Transaction
                    {
                        TransactionId = 3,
                        AccountId = Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"),
                        Amount = 100.00m,
                        CategoryId = 1,
                        TransactionDate = DateTime.Parse("2021-01-10")
                    },
                    new Transaction
                    {
                        TransactionId = 4,
                        AccountId = Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"),
                        Amount = 50.00m,
                        CategoryId = 2,
                        TransactionDate = DateTime.Parse("2021-01-15")
                    },
                    new Transaction
                    {
                        TransactionId = 5,
                        AccountId = Guid.Parse("cbb0bc3e-a583-4e73-a2e1-d3387f84cf87"),
                        Amount = 77.00m,
                        CategoryId = 1,
                        TransactionDate = DateTime.Parse("2021-01-17")
                    });
        }
    }
}
