using Api.Domain.DTOs;
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
            ObjectAsserts<UserEntity, UserDTO>(expectedEntityResult, result, new List<string>() { "Password", "Balance" });
        }

        [Fact(DisplayName = "Should call get all")]
        public async void ShouldCallGetAll()
        {
            // Given
            var expectedEntityResult = UserStubs.GetUserEntity();

            var expectedUserDTOResult = Mapper.Map<UserDTO>(expectedEntityResult);

            _userRepository = new Mock<IUserRepository>();

            _userRepository.Setup(u => u.SelectAsync())
                .ReturnsAsync(new List<UserEntity>(){
                    expectedEntityResult,
                });

            var userService = new UserService(_userRepository.Object, Mapper);

            // When
            var result = await userService.Get();

            // Then
            _userRepository.Verify(u => u.SelectAsync());
            Assert.Single(result);
            ObjectAsserts<UserEntity, UserDTO>(expectedEntityResult, result.First(), new List<string>() { "Password", "Balance" });
        }

        [Fact(DisplayName = "Should call get by Id")]
        public async void ShouldCallGetById()
        {
            // Given
            var expectedEntityResult = UserStubs.GetUserEntity();

            var expectedUserDTOResult = Mapper.Map<UserDTO>(expectedEntityResult);

            _userRepository = new Mock<IUserRepository>();

            _userRepository.Setup(u => u.SelectAsync(It.IsAny<Guid>()))
                .Callback<Guid>(payload =>
                {
                    Assert.Equal(expectedEntityResult.Id, payload);
                })
                .ReturnsAsync(expectedEntityResult);

            var userService = new UserService(_userRepository.Object, Mapper);

            // When
            var result = await userService.Get(expectedEntityResult.Id);

            // Then
            _userRepository.Verify(u => u.SelectAsync(It.IsAny<Guid>()));
            Assert.NotNull(result);
            ObjectAsserts<UserEntity, UserDTO>(expectedEntityResult, result, new List<string>() { "Password", "Balance" });
        }

        [Fact(DisplayName = "Should call insert to add new user passing the right params")]
        public async void ShouldCallInsertToAddNewUserPassingTheRightParams()
        {
            // Given
            var expectedDTO = UserStubs.GetUserCompleteDTO();
            var entityExpectedResult = Mapper.Map<UserEntity>(expectedDTO);

            _userRepository = new Mock<IUserRepository>();

            _userRepository.Setup(u => u.InsertAsync(It.IsAny<UserEntity>()))
                .Callback<UserEntity>(payload =>
                {
                    ObjectAsserts<UserEntity, UserCompleteDTO>(payload, expectedDTO, new List<string>() { });
                })
                .ReturnsAsync(entityExpectedResult);

            // When
            var service = new UserService(_userRepository.Object, Mapper);
            var result = await service.Post(expectedDTO);

            // Then
            _userRepository.Verify(u => u.InsertAsync(It.IsAny<UserEntity>()));

            ObjectAsserts<UserCompleteDTO, UserDTO>(expectedDTO, result, new List<string>() { "Password", "Balance" });
        }

        [Fact(DisplayName = "Should call put to update an user passing the right params")]
        public async void ShouldCallPutToUpdateAnUserPassingTheRightParams()
        {
            // Given
            var expectedDTO = UserStubs.GetUserCompleteDTO();
            var entityExpectedResult = Mapper.Map<UserEntity>(expectedDTO);

            _userRepository = new Mock<IUserRepository>();

            _userRepository.Setup(u => u.UpdateAsync(It.IsAny<UserEntity>()))
                .Callback<UserEntity>(payload =>
                {
                    ObjectAsserts<UserEntity, UserCompleteDTO>(payload, expectedDTO, new List<string>() { });
                })
                .ReturnsAsync(entityExpectedResult);

            // When
            var service = new UserService(_userRepository.Object, Mapper);
            var result = await service.Put(expectedDTO);

            // Then
            _userRepository.Verify(u => u.UpdateAsync(It.IsAny<UserEntity>()));

            ObjectAsserts<UserCompleteDTO, UserDTO>(expectedDTO, result, new List<string>() { "Password", "Balance" });
        }

        [Fact(DisplayName = "Should call delete to remove an user passing the right params")]
        public async void ShouldCallDeleteToremoveAnUserPassingTheRightParams()
        {
            // Given
            var expectedDTO = UserStubs.GetUserCompleteDTO();
            var entityExpectedResult = Mapper.Map<UserEntity>(expectedDTO);

            _userRepository = new Mock<IUserRepository>();

            _userRepository.Setup(u => u.DeleteAsync(It.IsAny<Guid>()))
                .Callback<Guid>(payload =>
                {
                    Assert.Equal(expectedDTO.Id, payload);
                })
                .ReturnsAsync(true);

            // When
            var service = new UserService(_userRepository.Object, Mapper);
            var result = await service.Delete(expectedDTO.Id);

            // Then
            _userRepository.Verify(u => u.DeleteAsync(It.IsAny<Guid>()));

            Assert.True(result);
        }
    }
}