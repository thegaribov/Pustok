using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using System.Linq;

namespace Pustok.Controllers.Client;

//controller 
public class HomeController : Controller
{
    private readonly PustokDbContext _dbContext;

    public HomeController(PustokDbContext pustokDbContext)
    {
        _dbContext = pustokDbContext;
    }

    // localhost:2323/home/index
    //action
    //url mapping, route mapping
    public ViewResult Index()
    {
        return View(_dbContext.Products.ToList());
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
