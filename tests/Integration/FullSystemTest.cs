using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Integration
{
    public class FullSystemTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public FullSystemTest(WebApplicationFactory<Program> factory)
        {
            // Tworzymy klienta HTTP do testów integracyjnych
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost") // wymuszenie HTTP zamiast HTTPS
            });
        }

        [Fact]
        public async Task FrontendBackendDatabase_ShouldBeConnected()
        {
            var response = await _client.GetAsync("/health/full");
            response.EnsureSuccessStatusCode(); // sprawdza status 200 OK

            var doc = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonDocument>();
            Assert.NotNull(doc);
            var root = doc!.RootElement;

            Assert.Equal("ok", root.GetProperty("status").GetString());
            Assert.Equal(1, root.GetProperty("db").GetInt32());
        }
    }
}
