using Api.Application.Controllers;
using Api.Domain.DTOs;
using Domain.DTOs;
using Domain.Services;
using Domain.Stubs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Application.Test.User
{
    public class UsersControllerUnitTests : BaseTest
    {
        private Mock<IUserService> _mockUserService;

        public UsersControllerUnitTests()
        {
            _mockUserService = new Mock<IUserService>();
        }

        [Fact(DisplayName = "Should Call Get Users")]
        public async void ShouldCallGetUsers()
        {
            // Given
            _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(u => u.Get())
                .ReturnsAsync(new List<UserDTO>());

            var controller = new UsersController(_mockUserService.Object);

            // When
            var result = await controller.GetUsers();

            // Then
            Assert.Equal("OkObjectResult", result.GetType().Name);
            _mockUserService.Verify(u => u.Get());
        }

        [Fact(DisplayName = "Should Call Get User by id")]
        public async void ShouldCallGetUserById()
        {
            // Given
            _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(u => u.Get(It.IsAny<Guid>()))
                .ReturnsAsync(new UserCompleteDTO());

            var controller = new UsersController(_mockUserService.Object);

            // When
            var result = await controller.GetUser(Guid.NewGuid());

            // Then
            Assert.Equal("OkObjectResult", result.GetType().Name);
            _mockUserService.Verify(u => u.Get(It.IsAny<Guid>()));
        }

        [Theory(DisplayName = "Should Call Post to insert an user")]
        [InlineData(false, "CreatedResult")]
        [InlineData(true, "BadRequestResult")]
        public async void ShouldCallPostToInsertAnUser(bool isResultNull, string expectedResultType)
        {
            // Given
            UserDTO? expectedResult = isResultNull ? null : new UserDTO() { Id = Guid.NewGuid() };

            var expectedPayload = UserStubs.GetUserCompleteDTO();

            _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(u => u.Post(It.IsAny<UserCompleteDTO>()))
                .Callback<UserCompleteDTO>(payload =>
                {
                    ObjectAsserts<UserCompleteDTO, UserCompleteDTO>(expectedPayload, payload, new List<string>());
                })
                .ReturnsAsync(expectedResult);


            var controller = new UsersController(_mockUserService.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000/");
            controller.Url = url.Object;

            // When
            var result = await controller.Post(expectedPayload);

            // Then
            Assert.Equal(expectedResultType, result.GetType().Name);
            _mockUserService.Verify(u => u.Post(It.IsAny<UserCompleteDTO>()));
        }

        [Theory(DisplayName = "Should Call Put to update an user")]
        [InlineData(false, "OkObjectResult")]
        [InlineData(true, "BadRequestResult")]
        public async void ShouldCallPutToUpdateAnUser(bool isResultNull, string expectedResultType)
        {
            // Given
            var expectedPayload = UserStubs.GetUserCompleteDTO();

            UserDTO? expectedResult = isResultNull ? null : new UserDTO();

            _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(u => u.Put(It.IsAny<UserCompleteDTO>()))
                .Callback<UserCompleteDTO>(payload =>
                {
                    ObjectAsserts<UserCompleteDTO, UserCompleteDTO>(expectedPayload, payload, new List<string>());
                })
                .ReturnsAsync(expectedResult);

            var controller = new UsersController(_mockUserService.Object);

            // When
            var result = await controller.Put(expectedPayload);

            // Then
            Assert.Equal(expectedResultType, result.GetType().Name);
            _mockUserService.Verify(u => u.Put(It.IsAny<UserCompleteDTO>()));
        }

        [Fact(DisplayName = "Should Call Delete to remove an user")]
        public async void ShouldCallDeleteToRemoveAnUser()
        {
            // Given
            _mockUserService = new Mock<IUserService>();

            _mockUserService
                .Setup(u => u.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = new UsersController(_mockUserService.Object);

            // When
            var result = await controller.Delete(Guid.NewGuid());

            // Then
            Assert.Equal("OkObjectResult", result.GetType().Name);
            _mockUserService.Verify(u => u.Delete(It.IsAny<Guid>()));
        }
    }
}