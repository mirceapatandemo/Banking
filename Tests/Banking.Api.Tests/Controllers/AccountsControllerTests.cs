using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Banking.Api.Controllers;
using Banking.Api.Dtos;
using Banking.Api.Entities;
using Banking.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Banking.Api.Tests.Controllers
{
    public class AccountsControllerTests
    {
        private readonly AccountsController controller;
        private readonly Mock<IAccountDataService> mockAccountDataService;
        private readonly Mock<IMapper> mockMapper;

        public AccountsControllerTests()
        {
            mockAccountDataService = new Mock<IAccountDataService>();
            mockMapper = new Mock<IMapper>();

            controller = new AccountsController(mockAccountDataService.Object, mockMapper.Object);        
        }

        [Fact]
        public async Task GetAccounts_Returns200OKWithAccounts()
        {
            var expectedAccounts = new List<Account>{new Account()};
            var expectedAccountDtos = new List<AccountDto> { new AccountDto() };
            mockAccountDataService.Setup(a => a.GetAccounts()).ReturnsAsync(expectedAccounts);
            mockMapper.Setup(m => m.Map<IEnumerable<AccountDto>>(expectedAccounts)).Returns(expectedAccountDtos);

            var result = await controller.GetAccounts();

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var accountsDto = Assert.IsType<AccountsDto>(okResult.Value);
            accountsDto.Accounts.Should().Equal(expectedAccountDtos);
        }
    }
}
