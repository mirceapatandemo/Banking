using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Banking.Api.Controllers;
using Banking.Api.Dtos;
using Banking.Api.Entities;
using Banking.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Banking.Api.Tests.Controllers
{
    public class TransactionsControllerTests
    {
        private readonly TransactionsController controller;
        private readonly Mock<IAccountDataService> mockAccountDataService;
        private readonly Mock<ITransactionDataService> mockTransactionDataService;
        private readonly Mock<IMapper> mockMapper;

        public TransactionsControllerTests()
        {
            mockAccountDataService = new Mock<IAccountDataService>();
            mockTransactionDataService = new Mock<ITransactionDataService>();
            mockMapper = new Mock<IMapper>();

            controller = new TransactionsController(
                mockTransactionDataService.Object, mockAccountDataService.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetTransactionSummariesFromLastMonth_Returns400BdRequest_IfAccountDoesNotExist()
        {
            // Arrange
            mockAccountDataService.Setup(a => a.ExistsAccount(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act
            var result = await controller.GetTransactionSummariesFromLastMonth(Guid.NewGuid());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetTransactionSummariesFromLastMonth_Returns200OKWithExpectedSummaries()
        {
            // Arrange
            var resourceId = Guid.NewGuid();
            var expectedTransactionSummaries = new List<TransactionSummary>();
            var expectedTransactionSummaryDtos = new List<TransactionSummaryDto>();
            mockAccountDataService.Setup(a => a.ExistsAccount(It.IsAny<Guid>())).ReturnsAsync(true);
            mockTransactionDataService.Setup(t => t.GetAccountTransactionSummariesFrom(resourceId, BeginLastMonthDate))
                .ReturnsAsync(expectedTransactionSummaries);
            mockMapper.Setup(m => m.Map<IEnumerable<TransactionSummaryDto>>(expectedTransactionSummaries))
                .Returns(expectedTransactionSummaryDtos);

            // Act
            var result = await controller.GetTransactionSummariesFromLastMonth(resourceId);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var transactionSummaryDtos = Assert.IsAssignableFrom<IEnumerable<TransactionSummaryDto>>(okResult.Value);
            transactionSummaryDtos.Should().Equal(expectedTransactionSummaryDtos);
        }

        private static DateTime BeginLastMonthDate =>
            new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
    }
}
