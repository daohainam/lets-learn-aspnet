using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace MySessionIntegrationTest
{
    public class SessionTests: IClassFixture<WebApplicationFactory<MySession.Program>>
    {
        private readonly HttpClient factory;

        public SessionTests(WebApplicationFactory<MySession.Program> factory)
        {
            this.factory = factory.CreateClient();
        }

        [Fact]
        public async Task Call_TestGetSession_Returns_Ok_Async()
        {
            var response = await factory.GetAsync("/Test/TestGetSession");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Call_Set_And_Get_SessionValueAsync_Returns_Ok_Async()
        {
            string randomValue = Guid.NewGuid().ToString();

            await factory.GetAsync($"/Test/SetSessionValue?key=TEST-KEY&value={randomValue}");

            var response = await factory.GetAsync("/Test/GetSessionValue?key=TEST-KEY");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(randomValue, responseString);
        }
    }
}