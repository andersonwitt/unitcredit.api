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

            var api = new WebApplicationFactory<Program>();

            Client = api.CreateClient();
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
