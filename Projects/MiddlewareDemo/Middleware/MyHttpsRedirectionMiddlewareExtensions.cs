using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpsRedirection
{
    public static class MyHttpsRedirectionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyHttpsRedirection(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyHttpsRedirectionMiddleware>();
        }
    }
}
