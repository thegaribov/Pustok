using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Services;
using Pustok.Services.Abstract;
using Pustok.Services.Concretes;

namespace Pustok;

public class Program
{
    public static void Main(string[] args)
    {
        //DatabaseService databaseService = new DatabaseService();
        //databaseService.InitializeTables();
        //databaseService.Dispose();

        //string myHtml = "\"<div class=\\\"w3-example\\\">\\r\\n<h3>Example</h3>\\r\\n<div class=\\\"w3-white w3-padding notranslate\\\">\\r\\n<form action=\\\"/action_page.php\\\" target=\\\"_blank\\\">\\r\\n  <label for=\\\"fname\\\">First name:</label><br>\\r\\n  <input type=\\\"text\\\" id=\\\"fname\\\" name=\\\"fname\\\" value=\\\"John\\\"><br>\\r\\n  <label for=\\\"lname\\\">Last name:</label><br>\\r\\n  <input type=\\\"text\\\" id=\\\"lname\\\" name=\\\"lname\\\" value=\\\"Doe\\\"><br><br>\\r\\n  <input type=\\\"submit\\\" value=\\\"Submit\\\">\\r\\n</form> \\r\\n</div>\\r\\n<a class=\\\"w3-btn w3-margin-top w3-margin-bottom\\\" href=\\\"tryit.asp?filename=tryhtml_form_submit\\\" target=\\\"_blank\\\">Try it Yourself »</a>\\r\\n</div>\"";

        //boiler-plate 
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation();

        builder.Services
            .AddScoped<IEmployeeService, EmployeeServiceImp>()
            .AddScoped<IUserService, UserService>()
            .AddSingleton<IFileService, FileService>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IBasketService, BasketService>()
            .AddDbContext<PustokDbContext>(o =>
            {
                o.UseNpgsql(DatabaseConstants.CONNECTION_STRING);
            });


        //Middleware pipeline

        var app = builder.Build();

        app.UseStaticFiles();

        //app.MapGet("/", () => Results.Content(myHtml, "text/html"));
        //app.MapGet("/contact", () => "This is my contact : +994.."); //text/plain
        //app.MapGet("/about", () => Results.Content("<h1>About in World!</h1>", "text/html"));

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

        app.Run();
    }
}