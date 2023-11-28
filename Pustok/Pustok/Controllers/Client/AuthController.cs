using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.ViewModels.Auth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pustok.Controllers.Client;

public class AuthController : Controller
{
    private readonly PustokDbContext _pustokDbContext;

    public AuthController(PustokDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
   
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = _pustokDbContext.Users
            .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
        if (user == null)
        {
            ModelState.AddModelError("Email", "Password or email is wrong");

            return View();
        }

        var claims = new List<Claim>()
        {
            new Claim("Id", user.Id.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync("Cookies", claimsPrincipal);

        return RedirectToAction("index", "home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");

        return RedirectToAction(nameof(Login));
    }
}
