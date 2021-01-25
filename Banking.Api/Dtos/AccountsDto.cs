using System.Collections.Generic;

namespace Banking.Api.Dtos
{
    public class AccountsDto
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}
