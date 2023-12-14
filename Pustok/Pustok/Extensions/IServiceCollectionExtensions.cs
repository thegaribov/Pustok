using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Services.Abstract;
using Pustok.Services.Concretes;
using Pustok.Services;
using Microsoft.Extensions.Configuration;

namespace Pustok.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddControllerConfigs(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();
    }

    public static void AddCustomServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
           .AddHttpContextAccessor()
           .AddHttpClient()
           .AddScoped<IEmployeeService, EmployeeServiceImp>()
           .AddScoped<IUserService, UserService>()
           .AddSingleton<IFileService, FileService>()
           .AddScoped<IProductService, ProductService>()
           .AddScoped<IBasketService, BasketService>()
           .AddScoped<IOrderService, OrderService>()
           .AddDbContext<PustokDbContext>(o =>
           {
               var connectionString = configuration.GetConnectionString("Pustok");
               o.UseNpgsql(connectionString);
           });
    }

    public static void AddAuth(this IServiceCollection serviceCollection)
    {
        serviceCollection
           .AddAuthentication("Cookies")
           .AddCookie("Cookies", o =>
           {
               o.LoginPath = "/auth/login";
               o.LogoutPath = "/home/index";
               o.AccessDeniedPath = "/home/index";
           });

        serviceCollection.AddAuthorization();
    }
}
