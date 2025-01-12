using ToDoRepository;
using FluentAssertions;

namespace RepositoryTests
{
    public class MongoDbTodoRepositoryTests
    {
        [Fact]
        public async Task CreateTodoItemAsync()
        {
            // Arrange
            var repository = new MongoDbToDoRepository(
                new MongoDbToDoRepositoryOptions
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "TodoApi"
                }
                );
            var item = new ToDoItem { Id = Guid.NewGuid(), Name = "Test Item", IsComplete = false };
            // Act
            var result = await repository.CreateTodoItemAsync(item);
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
            result.Name.Should().Be(item.Name);
            result.IsComplete.Should().Be(item.IsComplete);

            var createdItem = await repository.GetTodoItemAsync(result.Id);
            createdItem.Should().NotBeNull();
            createdItem!.Id.Should().Be(result.Id);
            createdItem.Name.Should().Be(result.Name);
            createdItem.IsComplete.Should().Be(result.IsComplete);
        }
    }
}