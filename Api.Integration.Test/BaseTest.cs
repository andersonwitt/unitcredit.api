using System.Net.Http.Headers;
using Api.Domain.DTOs;
using Domain.DTOs;
using Domain.Stubs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;


namespace Api.Integration.Test
{
    public abstract class BaseTest
    {
        public HttpClient Client;
        public BaseTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("DB_CONNECTION", "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=unitdb;");
            Environment.SetEnvironmentVariable("Key", "86F7E437FAA5A7FCE15D1DDCB9EAEAEA377667B8");
            Environment.SetEnvironmentVariable("PassKey", "@Km@1sN3xt");
            Environment.SetEnvironmentVariable("Audience", "KmaisNextAud");
            Environment.SetEnvironmentVariable("Issuer", "KmaisIssuerIss");
            Environment.SetEnvironmentVariable("Seconds", "86400");

            var api = new WebApplicationFactory<Program>();

            Client = api.CreateClient();

            SignIn().GetAwaiter().GetResult();
        }

        public async Task SignIn()
        {
            var loginDto = new LoginPayloadDTO()
            {
                StudentId = "123",
                Password = "123",
            };

            var resultLogin = await PostJsonAsync(loginDto, "/api/login", Client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResultDTO>(jsonLogin);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                        loginObject.AccessToken);
        }

        public async Task SignInWithUser(UserCompleteDTO user)
        {
            var loginDto = new LoginPayloadDTO()
            {
                StudentId = user.StudentId,
                Password = user.Password,
            };

            var resultLogin = await PostJsonAsync(loginDto, "/api/login", Client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResultDTO>(jsonLogin);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                                                        loginObject.AccessToken);
        }

        public async Task<UserCompleteDTO> InsertAnUser()
        {
            var payload = UserStubs.GetUserCompleteDTO();
            payload.Balance = 500;

            var result = await PostJsonAsync(payload, "/api/users", Client);
            var resultContent = await result.Content.ReadAsStringAsync();
            var resultObj = JsonConvert.DeserializeObject<UserDTO>(resultContent);


            return payload;
        }
        public async Task<HttpResponseMessage> PostJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataclass), System.Text.Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> PutJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PutAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataclass), System.Text.Encoding.UTF8, "application/json"));
        }
    }
}
