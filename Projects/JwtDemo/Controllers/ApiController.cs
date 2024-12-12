using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers
{
    [Authorize]
    public class ApiController : Controller
    {

        public IActionResult SayHello(string name)
        {
            return Content($"Hello {name}!");
        }
        [Authorize(Roles = "Role1")]
        public IActionResult SayHello1(string name)
        {
            return Content($"Hello {name}!");
        }
        [Authorize(Roles = "Role1,Role2")]
        public IActionResult SayHello2(string name)
        {
            return Content($"Hello {name}!");
        }
        [Authorize(Roles = "Role1")]
        [Authorize(Roles = "Role2")]
        public IActionResult SayHello3(string name)
        {
            return Content($"Hello {name}!");
        }
        public IActionResult SayHello4(string name)
        {
            return Content($"Hello {name}!");
        }
        [AllowAnonymous]
        public IActionResult HelloAnonymous()
        {
            return Content($"Hello Anonymous!");
        }

    }
}
