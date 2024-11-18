namespace MiddlewareDemo.Middleware
{
    public static class ClientInfoMiddlewareExtensions
    {
        public static IApplicationBuilder UseClientInfo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientInfoMiddleware>();
        }

        public static IHostApplicationBuilder AddClientInfo(this IHostApplicationBuilder builder) {

            builder.Services.AddSingleton<IClientInfoRepository, ClientInfoRepository.ClientInfoRepository>();

            return builder;
        }
    }
}
