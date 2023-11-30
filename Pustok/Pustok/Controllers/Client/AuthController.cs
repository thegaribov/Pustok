using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.Database.DomainModels;
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

    #region Login

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

        if (user.IsAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
        }

        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync("Cookies", claimsPrincipal);

        return RedirectToAction("index", "home");
    }

    #endregion

    #region Registeer

    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (_pustokDbContext.Users.Any(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "This email aldready taken");
            return View();
        }

        var user = new User
        {
            Name = model.Name,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
        };

        _pustokDbContext.Users.Add(user);
        _pustokDbContext.SaveChanges();

        return RedirectToAction("index", "home");
    }

    #endregion

    #region Logout

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");

        return RedirectToAction(nameof(Login));
    }

    #endregion
}
