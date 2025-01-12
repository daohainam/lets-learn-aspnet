using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TodoApi;
using ToDoRepository;

namespace IntegrationTests
{
    public class ApiIntegrationTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test1()
        {
            var client = _factory.CreateClient();

            var id = Guid.NewGuid();
            var response = await client.GetAsync($"/api/todoitems/{id:D}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var item = new ToDoItem { Id = id, Name = GenerateRandomString(), IsComplete = false };
            var createResponse = await client.PostAsJsonAsync("/api/todoitems", item);

            createResponse.EnsureSuccessStatusCode();

            var createdItem = await createResponse.Content.ReadFromJsonAsync<ToDoItem>();
            createdItem.Should().NotBeNull();
            createdItem!.Id.Should().Be(id);
            createdItem.Name.Should().Be(item.Name);
            createdItem.IsComplete.Should().Be(item.IsComplete);

            var getResponse = await client.GetAsync($"/api/todoitems/{id:D}");
            getResponse.EnsureSuccessStatusCode();
            var getItem = await getResponse.Content.ReadFromJsonAsync<ToDoItem>();

            getItem.Should().NotBeNull();
            getItem!.Id.Should().Be(id);
            getItem.Name.Should().Be(item.Name);
            getItem.IsComplete.Should().Be(item.IsComplete);

            var deleteResponse = await client.DeleteAsync($"/api/todoitems/{id:D}");
            deleteResponse.EnsureSuccessStatusCode();

            getResponse = await client.GetAsync($"/api/todoitems/{id:D}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static string GenerateRandomString(int length = 50)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}