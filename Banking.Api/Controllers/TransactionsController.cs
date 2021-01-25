using System;
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
    public class TransactionsController : Controller
    {
        private readonly IAccountDataService accountDataService;
        private readonly ITransactionDataService transactionDataService;
        private readonly IMapper mapper;

        public TransactionsController(ITransactionDataService transactionDataService, IAccountDataService accountDataService, IMapper mapper)
        {
            this.transactionDataService = transactionDataService;
            this.accountDataService = accountDataService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionSummaryDto>> GetTransactionSummariesFromLastMonth(Guid resourceId)
        {
            if(!await accountDataService.ExistsAccount(resourceId))
            {
                return BadRequest($"Account with Id:{resourceId} does not exist");
            }

            var transactionSummaries = await transactionDataService.GetAccountTransactionSummariesFrom(resourceId, BeginLastMonthDate);
            var transactionSummaryDtos = mapper.Map<IEnumerable<TransactionSummaryDto>>(transactionSummaries);

            return Ok(transactionSummaryDtos);
        }

        private static DateTime BeginLastMonthDate =>
            new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
    }
}
