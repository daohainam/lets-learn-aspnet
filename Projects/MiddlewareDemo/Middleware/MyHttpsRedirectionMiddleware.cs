using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;

namespace MyHttpsRedirection
{
    public class MyHttpsRedirectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public MyHttpsRedirectionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.IsHttps)
            {
                await _next(context);
            }
            else
            {
                var httpsPort = _configuration["HTTPS_PORT"];

                if (string.IsNullOrEmpty(httpsPort))
                {
                    httpsPort = "443";
                }

                var httpsUrl = UriHelper.BuildAbsolute("https",
                    httpsPort == "443" ? new HostString(context.Request.Host.Host) : new HostString(context.Request.Host.Host, int.Parse(httpsPort)), 
                    context.Request.PathBase, 
                    context.Request.Path, 
                    context.Request.QueryString);

                // context.Response.Redirect(httpsUrl);

                context.Response.StatusCode = StatusCodes.Status307TemporaryRedirect;
                context.Response.Headers.TryAdd("Location", httpsUrl);
            }
        }

    }
}
