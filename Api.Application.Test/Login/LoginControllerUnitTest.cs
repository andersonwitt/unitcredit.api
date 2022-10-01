using Api.Application.Controllers;
using Api.Domain.DTOs;
using Api.Domain.Managers;
using Moq;

namespace Api.Application.Test.Login
{
    public class LoginControllerUnitTest
    {
        private Mock<ILoginManager> _loginManager;

        public LoginControllerUnitTest()
        {
            _loginManager = new Mock<ILoginManager>();
        }

        [Theory(DisplayName = "Should call sign in to authenticate user")]
        [InlineData(true, "OkObjectResult")]
        [InlineData(false, "OkObjectResult")]
        public async void ShouldCallSignInToAuthenticateUser(bool isAuthenticated, string objectResult)
        {
            // Given
            var payload = new LoginPayloadDTO()
            {
                Password = "123456",
                StudentId = "123456",
            };

            _loginManager = new Mock<ILoginManager>();
            _loginManager
                .Setup(l => l.SignIn(It.IsAny<LoginPayloadDTO>()))
                .Callback<LoginPayloadDTO>(p =>
                {
                    Assert.Equal(payload.Password, p.Password);
                    Assert.Equal(payload.StudentId, p.StudentId);
                })
                .ReturnsAsync(new LoginResultDTO()
                {
                    IsAuthenticated = isAuthenticated,
                });

            var loginController = new LoginController(_loginManager.Object);

            // When
            var result = await loginController.SignIn(payload);

            // Then
            Assert.Equal(objectResult, result.GetType().Name);
        }
    }
}