using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ToDoRepository;

namespace TodoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton(new MongoDbToDoRepositoryOptions
            {
                ConnectionString = builder.Configuration.GetConnectionString("TodoApiDatabase")!,
                DatabaseName = builder.Configuration["TodoApiDatabaseName"]!
            });
            builder.Services.AddTransient<IToDoRepository, MongoDbToDoRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
