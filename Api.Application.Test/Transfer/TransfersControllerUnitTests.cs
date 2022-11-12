using System.Security.Claims;
using Api.Application.Controllers;
using Api.Domain.DTO;
using Api.Domain.DTOs;
using Api.Domain.Enums;
using Api.Domain.Managers;
using Moq;

namespace Api.Application.Test.Transfer
{
    public class TransfersControllerUnitTests
    {
        private Mock<ITransferManager> _mockTransferManager;
        private Mock<IClaimsManager> _mockClaimsManager;

        public TransfersControllerUnitTests()
        {
            _mockTransferManager = new Mock<ITransferManager>();
            _mockClaimsManager = new Mock<IClaimsManager>();
        }

        [Fact(DisplayName = "Should Call Get Transactions")]
        public async void ShouldCallTransfer()
        {
            // Given
            var requestPayload = new TransferToUserRequestDTO()
            {
                ToId = Guid.NewGuid(),
                Total = 150m,
            };
            _mockTransferManager = new Mock<ITransferManager>();

            _mockClaimsManager
                .Setup(c => c.GetUserSession(It.IsAny<ClaimsPrincipal>()))
                .Returns(new UserSessionDTO()
                {
                    UserId = Guid.NewGuid(),
                    UserType = EnumUserType.ADMIN,
                });

            _mockTransferManager
                .Setup(u => u.TransferToUser(It.IsAny<TransferToUserPayloadDTO>()))
                .Callback<TransferToUserPayloadDTO>(payload =>
                {
                    Assert.Equal(EnumTransactionType.Transfer, payload.TransactionType);
                    Assert.Equal(requestPayload.ToId, payload.ToId);
                    Assert.Equal(requestPayload.Total, payload.Total);
                })
                .ReturnsAsync(new BaseResultDTO() { IsValid = true });

            var controller = new TransfersController(_mockClaimsManager.Object, _mockTransferManager.Object);

            // When
            var result = await controller.TransferToUser(requestPayload);

            // Then
            Assert.Equal("OkObjectResult", result.GetType().Name);
            _mockTransferManager.Verify(u => u.TransferToUser(It.IsAny<TransferToUserPayloadDTO>()));
        }
    }
}