using Banking.Api.DbContexts;
using Banking.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Api.Services
{
    public class AccountDataService : IAccountDataService
    {
        private readonly BankingContext context;

        public AccountDataService(BankingContext context)
        {
            this.context = context;
        }

        public async Task<bool> ExistsAccount(Guid accountId) =>
            await context.Accounts.AnyAsync(a => a.AccountId == accountId);
        
        public async Task<IEnumerable<Account>> GetAccounts() =>
            await context.Accounts.ToListAsync();        
    }
}
