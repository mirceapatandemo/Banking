using Banking.Api.DbContexts;
using Banking.Api.Entities;
using Banking.Api.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Banking.Api.Tests.Services
{
    public class TransactionDataServiceTests
    {
        private readonly BankingContext context;
        private readonly TransactionDataService transactionDataService;

        public TransactionDataServiceTests()
        {
            context = BankingContextFactory.CreateInMemory("bankingdb2");
            InitializeDb(context);

            transactionDataService = new TransactionDataService(context);
        }

        [Fact]
        public async Task GetAccountTransactionSummariesFrom_ReturnsEmpty_IfAccountDoesNotExist()
        {
            var transactionSummaries = await transactionDataService.GetAccountTransactionSummariesFrom(Guid.Parse("999ffbb8-9f11-4ec6-a1e1-df48aefc82ef"), new DateTime(2021, 01, 01));

            transactionSummaries.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAccountTransactionSummariesFrom_ReturnsEmpty_IfNoTransactionsFromSpecifiedDate()
        {
            var transactionSummaries = await transactionDataService.GetAccountTransactionSummariesFrom(Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"), new DateTime(2021, 07, 01));

            transactionSummaries.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAccountTransactionSummariesFrom_ReturnsSummaries_OnlyForAccountAndFromSpecifiedDate()
        {
            var transactionSummaries = await transactionDataService.GetAccountTransactionSummariesFrom(Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"), new DateTime(2021,01,01));

            transactionSummaries.Should().BeEquivalentTo(new List<TransactionSummary>
            {
                new TransactionSummary{CategoryName = "Food", TotalAmount = 120m, Currency = "EUR"},
                new TransactionSummary{CategoryName = "Entertainment", TotalAmount = 50m, Currency = "EUR"}
            });
        }

        private void InitializeDb(BankingContext context)
        {
            if (context.Accounts.Any())
                return;

            var accounts = new List<Account>
            {
                new Account {AccountId = Guid.Parse("450ffbb8-9f11-4ec6-a1e1-df48aefc82ef"), Currency = "EUR"},
                new Account {AccountId = Guid.Parse("cbb0bc3e-a583-4e73-a2e1-d3387f84cf87"), Currency = "EUR"}
            };
            InitializeDbWith(accounts, context);

            var categories = new List<Category>
            {
                new Category {CategoryId = 1, Name = "Food"},
                new Category {CategoryId = 2, Name = "Entertainment"},
                new Category {CategoryId = 3, Name = "Clothing"},
                new Category {CategoryId = 4, Name = "Travel"},
                new Category {CategoryId = 5, Name = "Medical expenses"}
            };
            InitializeDbWith(categories, context);

            var transactions = new List<Transaction>
            {
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
                }
            };
            InitializeDbWith(transactions, context);
        }

        private void InitializeDbWith<T>(IEnumerable<T> entities, BankingContext context)
            where T:class
        {
            context.AddRange(entities);
            context.SaveChanges();
        }
    }
}
