namespace MiddlewareDemo.Controllers
{
    public class ClientInfoController : Controller
    {
        private readonly IClientInfoRepository _clientInfoRepository;

        public ClientInfoController(IClientInfoRepository clientInfoRepository) {
            _clientInfoRepository = clientInfoRepository;
        }


        [Route("/clientinfo")]
        [HttpGet]
        public IActionResult GetClientInfo()
        {
            return Ok(HttpContext.Features.Get<ClientInfo>());
        }
    }
}
