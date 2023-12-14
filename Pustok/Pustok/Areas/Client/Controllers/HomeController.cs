using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using System.Linq;

namespace Pustok.Areas.Client.Controllers;

//controller 
[Route("home")]
[Area("Client")]
public class HomeController : Controller
{
    private readonly PustokDbContext _dbContext;

    public HomeController(PustokDbContext pustokDbContext)
    {
        _dbContext = pustokDbContext;
    }

    [HttpGet("~/")]
    [HttpGet("index")]
    public ViewResult Index()
    {
        return View(_dbContext.Products.ToList());
    }

    [HttpGet("contact")]
    public ViewResult Contact()
    {
        return View();
    }

    [HttpGet("about")]
    public ViewResult About()
    {
        return View();
    }
}
