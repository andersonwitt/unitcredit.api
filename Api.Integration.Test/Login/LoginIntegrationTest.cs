using Domain.DTOs;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Login
{
    public class LoginIntegrationTest : BaseTest
    {
        [Fact(DisplayName = "Should call sign in to authenticate user")]
        public async Task ShouldCallSignInToAuthenticateUser()
        {
            var payload = new LoginPayloadDTO()
            {
                Password = "123",
                StudentId = "123",
            };

            var result = await PostJsonAsync(payload, "/api/login", Client);
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<LoginResultDTO>(resultContent);

            Assert.False(resultObj.IsAuthenticated);
        }
    }
}