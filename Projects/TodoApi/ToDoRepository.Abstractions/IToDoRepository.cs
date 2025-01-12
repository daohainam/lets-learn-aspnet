namespace ToDoRepository
{
    public interface IToDoRepository
    {
        Task<ToDoItem> CreateTodoItemAsync(ToDoItem item);
        Task<int> DeleteTodoItemAsync(Guid id);
        Task<ToDoItem?> GetTodoItemAsync(Guid id);
        Task<IEnumerable<ToDoItem>> GetTodoItemsAsync();
    }
}
