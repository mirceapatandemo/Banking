using Banking.Api.DbContexts;
using Banking.Api.Entities;
using Banking.Api.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Banking.Api.Tests.Services
{
    public class AccountDataServiceTests
    {
        private readonly AccountDataService accountDataService;
        private readonly BankingContext context;

        public AccountDataServiceTests()
        {
            context = BankingContextFactory.CreateInMemory("bankingDb1");
            accountDataService = new AccountDataService(context);
        }

        [Fact]
        public async Task ExistAccount_ReturnsTrue_IfAccountExistInDb()
        {
            var accounts = new List<Account>
            {
                new Account {  AccountId = Guid.Parse("150ffbb8-9f11-4ec6-a1e1-df48aefc82ef") },
                new Account {  AccountId = Guid.Parse("236a5577-a3c2-4a95-af92-0c242d08f92e") }
            };
            await InitializeContextWith(accounts, context);

            var exist = await accountDataService.ExistsAccount(Guid.Parse("150ffbb8-9f11-4ec6-a1e1-df48aefc82ef"));
 
            exist.Should().BeTrue();
        }

        [Fact]
        public async Task ExistAccount_ReturnsFalse_IfAccountDoesNotExistInDb()
        {
            var accounts = new List<Account>
            {
                new Account {  AccountId = Guid.Parse("350ffbb8-9f11-4ec6-a1e1-df48aefc82ef") },
                new Account {  AccountId = Guid.Parse("436a5577-a3c2-4a95-af92-0c242d08f92e") }
            };
            await InitializeContextWith(accounts, context);

            var exist = await accountDataService.ExistsAccount(Guid.Parse("999ffbb8-9f11-4ec6-a1e1-df48aefc82ef"));

            exist.Should().BeFalse();
        }

        [Fact]
        public async Task GetAccounts_ReturnsAllAccountsFromDb()
        {
            var expectedAccounts = new List<Account>
            {
                new Account {  AccountId = Guid.Parse("550ffbb8-9f11-4ec6-a1e1-df48aefc82ef") },
                new Account {  AccountId = Guid.Parse("636a5577-a3c2-4a95-af92-0c242d08f92e") }
            };
            await InitializeContextWith(expectedAccounts, context);

            var accounts = await accountDataService.GetAccounts();

            accounts.Should().Contain(expectedAccounts);
        }

        private async Task InitializeContextWith(IEnumerable<Account> accounts, BankingContext context)
        {
            await context.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
        }
    }
}
