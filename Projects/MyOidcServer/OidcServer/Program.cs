using OidcServer.Repositories;

namespace OidcServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            builder.Services.AddSingleton<ICodeItemRepository, CodeItemRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapGet("/.well-known/openid-configuration",
                () => Results.File(Path.Combine(builder.Environment.ContentRootPath, "OidcDiscovery", "openid-configuration.json"), contentType: "application/json")
                );

            app.MapGet("/.well-known/jwks.json",
                () => Results.File(Path.Combine(builder.Environment.ContentRootPath, "OidcDiscovery", "jwks.json"), contentType: "application/json")
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
