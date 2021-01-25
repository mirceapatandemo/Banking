using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Banking.Api.Entities;

namespace Banking.Api.Services
{
    public interface IAccountDataService
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<bool> ExistsAccount(Guid accountId);
    }
}
