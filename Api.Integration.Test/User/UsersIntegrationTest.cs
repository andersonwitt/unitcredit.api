using Api.Domain.DTOs;
using Domain.DTOs;
using Domain.Stubs;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.User
{
    public class UsersIntegrationTest : BaseTest
    {
        [Fact(DisplayName = "Should test users flow")]
        public async void ShouldTestUsersFlow()
        {
            var user = await ShouldCallPostToInsertAnUser();

            await ShouldCallGetUsers();

            await ShouldCallGetUserById(user.Id);

            await ShouldCallUpdateUser(user);

            await ShouldCallDeleteUser(user.Id);
        }

        public async Task ShouldCallGetUsers()
        {
            var result = await Client.GetAsync($"/api/users");
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<List<UserDTO>>(resultContent);

            Assert.NotEmpty(resultObj);
        }

        public async Task ShouldCallGetUserById(Guid userId)
        {
            var result = await Client.GetAsync($"/api/users/{userId}");
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<UserDTO>(resultContent);

            Assert.NotNull(resultObj);
        }


        public async Task<UserDTO> ShouldCallPostToInsertAnUser()
        {
            var payload = UserStubs.GetUserCompleteDTO();

            var result = await PostJsonAsync(payload, "/api/users", Client);
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<UserDTO>(resultContent);

            Assert.NotNull(resultObj);

            return resultObj;
        }

        public async Task ShouldCallDeleteUser(Guid userId)
        {
            var result = await Client.DeleteAsync($"/api/users/{userId}");
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<bool>(resultContent);

            Assert.True(resultObj);
        }

        public async Task ShouldCallUpdateUser(UserDTO user)
        {
            var userPayload = UserStubs.GetUserCompleteDTO();
            userPayload.Id = user.Id;
            userPayload.Name = "an Updated Name";
            userPayload.Email = "updated@updated.com";

            var result = await PutJsonAsync(userPayload, "/api/users", Client);
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<UserDTO>(resultContent);

            Assert.NotNull(resultObj);
            Assert.NotEqual(resultObj.Name, user.Name);
            Assert.NotEqual(resultObj.Email, user.Email);
            Assert.Equal(userPayload.Name, resultObj.Name);
            Assert.Equal(userPayload.Email, resultObj.Email);
        }
    }
}