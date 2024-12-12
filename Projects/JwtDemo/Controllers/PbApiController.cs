using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers
{
    public class PbApiController : Controller
    {
        [Authorize(Policy = "Policy1")]
        public IActionResult SayHello(string name)
        {
            return Content($"Hello {name}!");
        }
    }
}
