using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Banking.Api.Dtos;
using Banking.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountsController : Controller
    {
        private readonly IAccountDataService accountDataService;
        private readonly IMapper mapper;

        public AccountsController(IAccountDataService accountDataService, IMapper mapper)
        {
            this.accountDataService= accountDataService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AccountsDto>> GetAccounts()
        {
            var accounts = await accountDataService.GetAccounts();
            var accountsDto = new AccountsDto
            {
                Accounts = mapper.Map<IEnumerable<AccountDto>>(accounts)
            };
            
            return Ok(accountsDto);            
        }
    } 
}
