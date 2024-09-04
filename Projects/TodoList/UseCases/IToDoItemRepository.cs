using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public interface IToDoItemRepository
    {
        void Add(TodoItem item);
        void Delete(int id);
        TodoItem? GetById(int id);
        IEnumerable<TodoItem> GetItems();
        void Update(TodoItem item);
    }
}
