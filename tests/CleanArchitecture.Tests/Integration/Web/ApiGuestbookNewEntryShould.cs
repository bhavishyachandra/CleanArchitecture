using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Tests.Integration.Web
{
    public class ApiGuestbookNewEntryShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private HttpClient client;

        public ApiGuestbookNewEntryShould(CustomWebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        public async Task InvalidIdShouldReturn404()
        {
            string invalidId = "100";
            string message = Guid.NewGuid().ToString();
            var newEntry = new { EmailAddress = "test@test.com", Message = message };
            var jsonString = JsonConvert.SerializeObject(newEntry);
            var result = await client.PostAsync($"/api/guestbook/{invalidId}/NewEntry", new StringContent(jsonString, Encoding.UTF8));
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        public async Task ReturnGuestbookWithNewItem()
        {
            string validId = "1";
            string message = Guid.NewGuid().ToString();
            var newEntry = new { EmailAddress = "test@test.com", Message = message };
            var jsonString = JsonConvert.SerializeObject(newEntry);
            var result = await client.PostAsync($"/api/guestbook/{validId}/NewEntry", new StringContent(jsonString, Encoding.UTF8));
            var resultString = await result.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<GuestBookEntry>(resultString);
            Assert.Equal(newEntry.EmailAddress, resultObject.EmailAddress);
            Assert.Equal(newEntry.Message, resultObject.Message);
        }
    }
}
