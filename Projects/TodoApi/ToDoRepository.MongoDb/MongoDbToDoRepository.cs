using MongoDB.Driver;

namespace ToDoRepository
{
    public class MongoDbToDoRepository : IToDoRepository
    {
        private readonly IMongoCollection<ToDoItem> collection;

        public MongoDbToDoRepository(MongoDbToDoRepositoryOptions options)
        {
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);
            collection = database.GetCollection<ToDoItem>("TodoItems");
        }

        public async Task<ToDoItem> CreateTodoItemAsync(ToDoItem item)
        {
            await collection.InsertOneAsync(item);
            return item;
        }

        public async Task<int> DeleteTodoItemAsync(Guid id)
        {
            var result = await collection.DeleteOneAsync(item => item.Id == id);

            return (int)result.DeletedCount;
        }

        public async Task<ToDoItem?> GetTodoItemAsync(Guid id)
        {
            return await collection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetTodoItemsAsync()
        {
            return await collection.Find(item => true).ToListAsync();
        }
    }
}
