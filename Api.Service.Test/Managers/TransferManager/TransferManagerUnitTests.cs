using Api.Domain.DTO;
using Api.Domain.DTOs;
using Api.Domain.Enums;
using Api.Domain.Services;
using Api.Service.Managers;
using Domain.DTOs;
using Domain.Services;
using Moq;

namespace Api.Service.Test.Managers
{
    public class TransferManagerUnitTests : BaseTest
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<ITransactionService> _transactionServiceMock;

        public TransferManagerUnitTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _transactionServiceMock = new Mock<ITransactionService>();
        }

        [Theory(DisplayName = "Should call transfer to user")]
        [InlineData((int)EnumUserType.COMMERCE, 135, "", true, 1, 1)]
        [InlineData((int)EnumUserType.STUDENT, 135, "", true, 1, 1)]
        [InlineData((int)EnumUserType.TEACHER, 135, "", true, 1, 1)]
        [InlineData((int)EnumUserType.ADMIN, 135, "", true, 1, 0)]
        [InlineData((int)EnumUserType.COMMERCE, 20, "Saldo insuficiente!", false, 0, 0)]
        [InlineData((int)EnumUserType.COMMERCE, 20, "Saldo insuficiente!", false, 0, 0)]
        public async void ShouldCallTransferToUser(
            int userType,
            decimal balance,
            string expectedMessage,
            bool expectedIsValid,
            int expectedTransactionPostCall,
            int expectedPutUserCall)
        {
            //Given
            var expectedGetUser1 = new UserCompleteDTO
            {
                Id = Guid.NewGuid(),
                Balance = balance,
                Type = (EnumUserType)userType
            };

            var expectedGetUser2 = new UserCompleteDTO
            {
                Id = Guid.NewGuid(),
                Balance = balance
            };
            var expectedPayload = new TransferToUserPayloadDTO
            {
                Total = 50m,
                FromId = expectedGetUser1.Id,
                ToId = expectedGetUser2.Id,
                TransactionType = EnumTransactionType.Credit
            };

            _userServiceMock = new Mock<IUserService>();
            _transactionServiceMock = new Mock<ITransactionService>();

            _userServiceMock
                .Setup(u => u.Get(expectedPayload.FromId))
                .ReturnsAsync(expectedGetUser1);

            _userServiceMock
                .Setup(u => u.Get(expectedPayload.ToId))
                .ReturnsAsync(expectedGetUser2);

            _transactionServiceMock
                .Setup(t => t.Post(It.IsAny<TransactionDTO>()))
                .Callback<TransactionDTO>(payload =>
                {
                    Assert.Equal(expectedPayload.FromId, payload.FromId);
                    Assert.Equal(expectedPayload.ToId, payload.ToId);
                    Assert.Equal(expectedPayload.Total, payload.Total);
                    Assert.Equal(expectedPayload.TransactionType, payload.Type);
                })
                .ReturnsAsync(new TransactionDTO());

            _userServiceMock
                .Setup(u => u.Put(It.IsAny<UserCompleteDTO>()))
                .Callback<UserCompleteDTO>(payload =>
                {
                    if (payload.Id == expectedPayload.FromId)
                    {
                        Assert.Equal((balance - expectedPayload.Total), payload.Balance);
                    }
                    else
                    {
                        Assert.Equal((balance + expectedPayload.Total), payload.Balance);
                    }
                })
                .ReturnsAsync(new UserDTO());

            var manager = new TransferManager(_transactionServiceMock.Object, _userServiceMock.Object);

            //When
            BaseResultDTO result = await manager.TransferToUser(expectedPayload);

            //Then
            _userServiceMock.Verify(u => u.Get(expectedPayload.FromId));
            _userServiceMock.Verify(u => u.Get(expectedPayload.ToId));

            _transactionServiceMock.Verify(t => t.Post(It.IsAny<TransactionDTO>()), Times.Exactly(expectedTransactionPostCall));
            _userServiceMock.Verify(u => u.Put(It.IsAny<UserCompleteDTO>()), Times.Exactly(expectedPutUserCall));

            Assert.Equal(expectedIsValid, result.IsValid);
            Assert.Equal(expectedMessage, result.Message);
        }
    }
}