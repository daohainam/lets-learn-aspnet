using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace MiddlewareDemo.Middleware
{
    public class ClientInfoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ClientInfoMiddleware> _logger;

        public ClientInfoMiddleware(RequestDelegate next, ILogger<ClientInfoMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            _logger.LogInformation("ClientInfoMiddleware is loaded");
        }

        public async Task InvokeAsync(HttpContext context, IClientInfoRepository clientInfoRepository)
        {
            _logger.LogInformation("ClientInfoMiddleware is invoked");

            var apiKey = context.Request.Headers["API-Key"].FirstOrDefault();
            if (!string.IsNullOrEmpty(apiKey))
            {
                var clientInfo = clientInfoRepository.GetClientInfo(apiKey);
                if (clientInfo != null)
                {
                    _logger.LogInformation("ClientInfo found: {client}", clientInfo.Name);
                    context.Features.Set(clientInfo);

                    await _next(context);

                    return;
                }
            }

            _logger.LogWarning("Unauthorized!");
            context.Response.StatusCode = 401;
        }
    }
}
