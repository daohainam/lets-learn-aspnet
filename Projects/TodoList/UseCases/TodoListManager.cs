using Entities;

namespace UseCases
{
    public class TodoListManager(IToDoItemRepository repository)
    {
        private readonly IToDoItemRepository repository = repository;

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return repository.GetItems();
        }

        public void AddTodoItem(TodoItem item)
        {
            repository.Add(item);
        }

        public void MarkComplete(int id)
        {
            var item = repository.GetById(id);
            if (item != null)
            {
                item.IsCompleted = true;
                repository.Update(item);
            }
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
