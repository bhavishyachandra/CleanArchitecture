using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Tests.Integration.Web
{
    public class ApiGuestbookGetByIdShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ApiGuestbookGetByIdShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnGuestbookWithOneItem()
        {
            var response = await _client.GetAsync("/api/guestbook/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GuestBook>(stringResponse);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Return404GivenInvalidId()
        {
            var response = await _client.GetAsync("/api/guestbook/100");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
