using Microsoft.AspNetCore.Mvc;
using Pustok.Database;

namespace Pustok.Controllers;

//controller 
public class HomeController : Controller
{
    // localhost:2323/home/index
    //action
    //url mapping, route mapping
    public ViewResult Index()
    {
        return View(DbContext._products);
    }

    // localhost:2323/home/contact
    //action
    public ViewResult Contact()
    {
        return View();
    }

    // localhost:2323/home/about
    //action
    public ViewResult About()
    {
        return View();
    }

}
