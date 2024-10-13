
namespace MySession.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var session = HttpContext.GetSession(); 

        session.SetString("Name", "Tom");
        await session.CommitAsync();

        return View();
    }

    public async Task<IActionResult> PrivacyAsync()
    {
        var session = HttpContext.GetSession();
        await session.LoadAsync();
        var name = session.GetString("Name");

        return View("Privacy", name);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
