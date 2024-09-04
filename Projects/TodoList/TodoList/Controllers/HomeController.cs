using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoList.Models;
using UseCases;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoListManager _listManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(TodoListManager listManager, ILogger<HomeController> logger)
        {
            _listManager = listManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var todoItems = _listManager.GetTodoItems();

            return View(new TodoListViewModel()
            {
                Items = todoItems.Select(ti => new Item()
                {
                    Id = ti.Id,
                    Text = ti.Text,
                    IsCompleted = ti.IsCompleted
                })
            });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(Item item)
        {
            _listManager.AddTodoItem(new TodoItem()
            {
                Id = item.Id,
                Text = item.Text,
                IsCompleted = false
            });

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
