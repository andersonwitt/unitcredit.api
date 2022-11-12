using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Api.Domain.Stubs;
using Api.Service.Services;
using Moq;

namespace Api.Service.Test.Transaction
{
    public class TransactionServiceUnitTests : BaseTest
    {
        private Mock<ITransactionRepository> _transactionRepository;

        public TransactionServiceUnitTests()
        {
            _transactionRepository = new Mock<ITransactionRepository>();
        }

        [Fact(DisplayName = "Should call get by Id")]
        public async void ShouldCallGetById()
        {
            // Given
            var expectedEntityResult = TransactionStubs.GetTransactionEntity();

            _transactionRepository = new Mock<ITransactionRepository>();

            _transactionRepository.Setup(u => u.SelectAsync(It.IsAny<Guid>()))
                .Callback<Guid>(payload =>
                {
                    Assert.Equal(expectedEntityResult.Id, payload);
                })
                .ReturnsAsync(expectedEntityResult);

            var transactionService = new TransactionService(_transactionRepository.Object, Mapper);

            // When
            var result = await transactionService.Get(expectedEntityResult.Id);

            // Then
            _transactionRepository.Verify(u => u.SelectAsync(It.IsAny<Guid>()));
            Assert.NotNull(result);
            ObjectAsserts<TransactionEntity, TransactionDTO>(expectedEntityResult, result, new List<string>() { "To", "From" });
        }

        [Fact(DisplayName = "Should call get all")]
        public async void ShouldCallGetAll()
        {
            // Given
            var expectedEntityResult = TransactionStubs.GetTransactionEntity();

            _transactionRepository = new Mock<ITransactionRepository>();

            _transactionRepository.Setup(u => u.SelectAsync())
                .ReturnsAsync(new List<TransactionEntity>(){
                    expectedEntityResult,
                });

            var transactionService = new TransactionService(_transactionRepository.Object, Mapper);

            // When
            var result = await transactionService.Get();

            // Then
            _transactionRepository.Verify(u => u.SelectAsync());
            Assert.Single(result);
            ObjectAsserts<TransactionEntity, TransactionDTO>(expectedEntityResult, result.First(), new List<string>() { "To", "From" });
        }

        [Fact(DisplayName = "Should call get all by user id")]
        public async void ShouldCallGetAllByUserId()
        {
            // Given
            var expectedEntityResult = TransactionStubs.GetTransactionEntity();
            var expectedId = Guid.NewGuid();

            _transactionRepository = new Mock<ITransactionRepository>();

            _transactionRepository.Setup(u => u.SelectByUserId(It.IsAny<Guid>()))
                .Callback<Guid>(payload =>
                {
                    Assert.Equal(expectedId, payload);
                })
                .ReturnsAsync(new List<TransactionEntity>(){
                    expectedEntityResult,
                });

            var transactionService = new TransactionService(_transactionRepository.Object, Mapper);

            // When
            var result = await transactionService.GetByUserId(expectedId);

            // Then
            _transactionRepository.Verify(u => u.SelectByUserId(It.IsAny<Guid>()));
            Assert.Single(result);
            ObjectAsserts<TransactionEntity, TransactionDTO>(expectedEntityResult, result.First(), new List<string>() { "To", "From" });
        }

        [Fact(DisplayName = "Should call insert to add new user passing the right params")]
        public async void ShouldCallInsertToAddNewUserPassingTheRightParams()
        {
            // Given
            var expectedDTO = TransactionStubs.GetTransactionDTO();
            var entityExpectedResult = Mapper.Map<TransactionEntity>(expectedDTO);

            _transactionRepository = new Mock<ITransactionRepository>();

            _transactionRepository.Setup(u => u.InsertAsync(It.IsAny<TransactionEntity>()))
                .Callback<TransactionEntity>(payload =>
                {
                    ObjectAsserts<TransactionEntity, TransactionDTO>(payload, expectedDTO, new List<string>() { "From", "To" });
                })
                .ReturnsAsync(entityExpectedResult);

            // When
            var service = new TransactionService(_transactionRepository.Object, Mapper);
            var result = await service.Post(expectedDTO);

            // Then
            _transactionRepository.Verify(u => u.InsertAsync(It.IsAny<TransactionEntity>()));

            ObjectAsserts<TransactionDTO, TransactionDTO>(expectedDTO, result, new List<string>() { "From", "To" });
        }

        [Fact(DisplayName = "Should call put to update an user passing the right params")]
        public async void ShouldCallPutToUpdateAnUserPassingTheRightParams()
        {
            // Given
            var expectedDTO = TransactionStubs.GetTransactionDTO();
            var entityExpectedResult = Mapper.Map<TransactionEntity>(expectedDTO);

            _transactionRepository = new Mock<ITransactionRepository>();

            _transactionRepository.Setup(u => u.UpdateAsync(It.IsAny<TransactionEntity>()))
                .Callback<TransactionEntity>(payload =>
                {
                    ObjectAsserts<TransactionEntity, TransactionDTO>(payload, expectedDTO, new List<string>() { "To", "From" });
                })
                .ReturnsAsync(entityExpectedResult);

            // When
            var service = new TransactionService(_transactionRepository.Object, Mapper);
            var result = await service.Put(expectedDTO);

            // Then
            _transactionRepository.Verify(u => u.UpdateAsync(It.IsAny<TransactionEntity>()));

            ObjectAsserts<TransactionDTO, TransactionDTO>(expectedDTO, result, new List<string>() { "To", "From" });
        }

        [Fact(DisplayName = "Should call delete to remove an user passing the right params")]
        public async void ShouldCallDeleteToremoveAnUserPassingTheRightParams()
        {
            // Given
            var expectedDTO = TransactionStubs.GetTransactionDTO();
            var entityExpectedResult = Mapper.Map<TransactionEntity>(expectedDTO);

            _transactionRepository = new Mock<ITransactionRepository>();

            _transactionRepository.Setup(u => u.DeleteAsync(It.IsAny<Guid>()))
                .Callback<Guid>(payload =>
                {
                    Assert.Equal(expectedDTO.Id, payload);
                })
                .ReturnsAsync(true);

            // When
            var service = new TransactionService(_transactionRepository.Object, Mapper);
            var result = await service.Delete(expectedDTO.Id);

            // Then
            _transactionRepository.Verify(u => u.DeleteAsync(It.IsAny<Guid>()));

            Assert.True(result);
        }
    }
}