using Banking.Api.DbContexts;
using Banking.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Api.Services
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly BankingContext context;

        public TransactionDataService(BankingContext context) =>
            this.context = context;

        public async Task<IEnumerable<TransactionSummary>> GetAccountTransactionSummariesFrom(Guid accountId, DateTimeOffset fromDate)
        {
            var transactions = await context.Transactions
                .Include(t => t.Category)
                .Include(t => t.Account)
                .Where(t => t.AccountId == accountId && 
                            t.TransactionDate >= fromDate)
                .ToListAsync();

            var summaries = transactions
                .GroupBy(t => t.Category)
                .Select(g => new TransactionSummary
                {
                    CategoryName = g.Key.Name,
                    TotalAmount = g.Sum(item => item.Amount),
                    Currency = g.First().Account.Currency
                }).ToList();

            return summaries;
        }
    }
}
