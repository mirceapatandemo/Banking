using Banking.Api.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Banking.Api.Tests.Services
{
    public class BankingContextFactory
    {
        public static BankingContext CreateInMemory(string database)
        {
            var options = new DbContextOptionsBuilder<BankingContext>()
                .UseInMemoryDatabase(databaseName: database)
                .Options;

            return new BankingContext(options);
        }
    }
}
