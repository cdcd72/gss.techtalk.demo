using JwtAuth.Demo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APITest.Demo
{
    public class TestBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        public TestBase()
        {
            Server = new TestServer(GetWebHostBuilder());
            Client = Server.CreateClient();
        }

        #region Protected Method

        protected async Task<HttpResponseMessage> GetAsync(string url, string token = null)
        {
            if (token != null)
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.GetAsync(url);

            return response;
        }

        protected async Task<T> GetResultAsync<T>(string url, string token = null)
        {
            var response = await GetAsync(url, token);

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, object dto, string token = null)
        {
            if (token != null)
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.PostAsync(url,
                new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));

            return response;
        }

        protected async Task<T> PostResultAsync<T>(string url, object dto, string token = null)
        {
            var response = await PostAsync(url, dto, token);

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        #endregion

        private IWebHostBuilder GetWebHostBuilder()
        {
            // 在此處因為是在模擬載入 Configuration，所以才只會載入 appsettings.json & appsettings.Test.json
            // 實際上這段在 .NET Core 背後就處理掉了，而且會依據執行環境的 ASPNETCORE_ENVIRONMENT 環境變數切換並載入組態設定...
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Test.json")
                .Build();

            var builder = new WebHostBuilder()
                .UseConfiguration(configBuilder)
                .UseEnvironment("Test") // 實際執行環境改為 Test
                .UseStartup<Startup>();

            return builder;
        }
    }
}
