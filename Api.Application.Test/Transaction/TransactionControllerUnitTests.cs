using Api.Application.Controllers;
using Api.Domain.DTOs;
using Api.Domain.Services;
using Api.Domain.Stubs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.Transaction
{
    public class TransactionControllerUnitTests : BaseTest
    {
        private Mock<ITransactionService> _mockTransactionService;

        public TransactionControllerUnitTests()
        {
            _mockTransactionService = new Mock<ITransactionService>();
        }

        [Fact(DisplayName = "Should Call Get Transactions")]
        public async void ShouldCallGetTransactions()
        {
            // Given
            _mockTransactionService = new Mock<ITransactionService>();

            _mockTransactionService
                .Setup(u => u.Get())
                .ReturnsAsync(new List<TransactionDTO>());

            var controller = new TransactionsController(_mockTransactionService.Object);

            // When
            var result = await controller.GetAll();

            // Then
            Assert.Equal("OkObjectResult", result.GetType().Name);
            _mockTransactionService.Verify(u => u.Get());
        }

        [Fact(DisplayName = "Should Call Get User by id")]
        public async void ShouldCallGetUserById()
        {
            // Given
            _mockTransactionService = new Mock<ITransactionService>();

            _mockTransactionService
                .Setup(u => u.Get(It.IsAny<Guid>()))
                .ReturnsAsync(new TransactionDTO());

            var controller = new TransactionsController(_mockTransactionService.Object);

            // When
            var result = await controller.Get(Guid.NewGuid());

            // Then
            Assert.Equal("OkObjectResult", result.GetType().Name);
            _mockTransactionService.Verify(u => u.Get(It.IsAny<Guid>()));
        }

        [Theory(DisplayName = "Should Call Post to insert a transaction")]
        [InlineData(false, "CreatedResult")]
        [InlineData(true, "BadRequestResult")]
        public async void ShouldCallPostToInsertATransaction(bool isResultNull, string expectedResultType)
        {
            // Given
            TransactionDTO? expectedResult = isResultNull ? null : new TransactionDTO() { Id = Guid.NewGuid() };

            var expectedPayload = TransactionStubs.GetTransactionDTO();

            _mockTransactionService = new Mock<ITransactionService>();

            _mockTransactionService
                .Setup(u => u.Post(It.IsAny<TransactionDTO>()))
                .Callback<TransactionDTO>(payload =>
                {
                    ObjectAsserts<TransactionDTO, TransactionDTO>(expectedPayload, payload, new List<string>());
                })
                .ReturnsAsync(expectedResult);


            var controller = new TransactionsController(_mockTransactionService.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000/");
            controller.Url = url.Object;

            // When
            var result = await controller.Post(expectedPayload);

            // Then
            Assert.Equal(expectedResultType, result.GetType().Name);
            _mockTransactionService.Verify(u => u.Post(It.IsAny<TransactionDTO>()));
        }

        [Theory(DisplayName = "Should Call Put to update a transaction")]
        [InlineData(false, "OkObjectResult")]
        [InlineData(true, "BadRequestResult")]
        public async void ShouldCallPutToUpdateATransaction(bool isResultNull, string expectedResultType)
        {
            // Given
            var expectedPayload = TransactionStubs.GetTransactionDTO();

            TransactionDTO? expectedResult = isResultNull ? null : new TransactionDTO();

            _mockTransactionService = new Mock<ITransactionService>();

            _mockTransactionService
                .Setup(u => u.Put(It.IsAny<TransactionDTO>()))
                .Callback<TransactionDTO>(payload =>
                {
                    ObjectAsserts<TransactionDTO, TransactionDTO>(expectedPayload, payload, new List<string>());
                })
                .ReturnsAsync(expectedResult);

            var controller = new TransactionsController(_mockTransactionService.Object);

            // When
            var result = await controller.Put(expectedPayload);

            // Then
            Assert.Equal(expectedResultType, result.GetType().Name);
            _mockTransactionService.Verify(u => u.Put(It.IsAny<TransactionDTO>()));
        }

        [Fact(DisplayName = "Should Call Delete to remove a transaction")]
        public async void ShouldCallDeleteToRemoveATransaction()
        {
            // Given
            _mockTransactionService = new Mock<ITransactionService>();

            _mockTransactionService
                .Setup(u => u.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = new TransactionsController(_mockTransactionService.Object);

            // When
            var result = await controller.Delete(Guid.NewGuid());

            // Then
            Assert.Equal("OkObjectResult", result.GetType().Name);
            _mockTransactionService.Verify(u => u.Delete(It.IsAny<Guid>()));
        }
    }
}