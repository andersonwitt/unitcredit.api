using Domain.DTOs;
using Domain.Entities;
using Domain.Repository;
using Domain.Stubs;
using Moq;
using Service.Services;

namespace Api.Service.Test.User
{
    public class UserServiceUnitTest : BaseTest
    {
        private Mock<IUserRepository> _userRepository;

        public UserServiceUnitTest()
        {
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact(DisplayName = "Should call select by login")]
        public async void ShouldCallSelectByLogin()
        {
            // Given
            var expectedEntityResult = UserStubs.GetUserEntity();

            var expectedUserDTOResult = Mapper.Map<UserDTO>(expectedEntityResult);
            var payload = new LoginPayloadDTO()
            {
                Password = "123456",
                StudentId = new Random().Next(1000, 9999).ToString()
            };

            _userRepository = new Mock<IUserRepository>();

            _userRepository.Setup(u => u.SelectByLogin(It.IsAny<LoginPayloadDTO>()))
                .Callback<LoginPayloadDTO>(p =>
                {
                    Assert.Equal(payload.Password, p.Password);
                    Assert.Equal(payload.StudentId, p.StudentId);
                })
                .ReturnsAsync(expectedEntityResult);

            var userService = new UserService(_userRepository.Object, Mapper);

            // When
            var result = await userService.GetByLogin(payload);

            // Then
            ObjectAsserts<UserEntity, UserDTO>(expectedEntityResult, result, new List<string>() { "Password" });
        }
    }
}