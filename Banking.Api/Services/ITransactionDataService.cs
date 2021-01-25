using Banking.Api.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Api.Services
{
    public interface ITransactionDataService
    {
        Task<IEnumerable<TransactionSummary>> GetAccountTransactionSummariesFrom(Guid accountId, DateTimeOffset fromDate);
    }
}
