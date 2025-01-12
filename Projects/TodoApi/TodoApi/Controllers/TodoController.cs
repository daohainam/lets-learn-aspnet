using Microsoft.AspNetCore.Mvc;
using ToDoRepository;

namespace TodoApi.Controllers
{
    [Route("/api/todoitems")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IToDoRepository todoRepository;

        public TodoController(IToDoRepository todoRepository) { 
            this.todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(await todoRepository.GetTodoItemsAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItemAsync(Guid id)
        {
            var item = await todoRepository.GetTodoItemAsync(id);
            
            if (item == null) return NotFound();

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTodoItemAsync(ToDoItem item)
        {
            return Ok(await todoRepository.CreateTodoItemAsync(item));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(int id)
        {
            return Ok($"UpdateTodoItem {id}");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            int r = await todoRepository.DeleteTodoItemAsync(id);

            return r == 0 ? NotFound() : Ok();
        }
    }
}
