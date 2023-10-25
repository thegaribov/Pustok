using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.Database.Repositories;

namespace Pustok.Controllers;

//controller 
public class HomeController : Controller
{
    private readonly ProductRepository _productRepository;

    public HomeController()
    {
        _productRepository = new ProductRepository();
    }

    // localhost:2323/home/index
    //action
    //url mapping, route mapping
    public ViewResult Index()
    {
        return View(_productRepository.GetAll());
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

    protected override void Dispose(bool disposing)
    {
        _productRepository.Dispose();

        base.Dispose(disposing);
    }
}
