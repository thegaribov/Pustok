using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pustok.Extensions;
using Pustok.Hubs;

namespace Pustok;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);

        var app = builder.Build();

        ConfigureMiddlewarePipeline(app);

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllerConfigs();
        builder.Services.AddAuth();
        builder.Services.AddCustomServices();
        builder.Services.AddSignalR();
    }

    private static void ConfigureMiddlewarePipeline(WebApplication app)
    {
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

        app.MapHub<AlertHub>("alerthub");
    }
}