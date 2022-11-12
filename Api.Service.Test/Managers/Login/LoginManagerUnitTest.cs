using Domain.DTOs;
using Domain.Services;
using Domain.Stubs;
using Moq;
using Service.Managers;

namespace Api.Service.Test.Login
{
    public class LoginManagerUnitTest
    {
        private Mock<IUserService> _userService;

        public LoginManagerUnitTest()
        {
            _userService = new Mock<IUserService>();
        }

        [Theory(DisplayName = "Should validate if user has correct login information")]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void ShouldValidateIfUserHasCorrectLoginInformation(bool hasUser, bool expectedIsValid)
        {
            // Given
            UserDTO userDTO = hasUser ? UserStubs.GetUserDTO() : null;

            var payload = new LoginPayloadDTO()
            {
                Password = "123456",
                StudentId = "123456",
            };

            _userService = new Mock<IUserService>();
            _userService.Setup(u => u.GetByLogin(It.IsAny<LoginPayloadDTO>()))
                .Callback<LoginPayloadDTO>(p =>
                {
                    Assert.Equal(payload.Password, p.Password);
                    Assert.Equal(payload.StudentId, p.StudentId);
                })
                .ReturnsAsync(userDTO);

            var loginManager = new LoginManager(_userService.Object);

            // When
            var result = await loginManager.SignIn(payload);

            // Then
            Assert.Equal(expectedIsValid, result.IsAuthenticated);
        }
    }
}