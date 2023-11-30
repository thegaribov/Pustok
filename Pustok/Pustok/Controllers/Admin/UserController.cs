using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using Pustok.Migrations;
using Pustok.ViewModels.Auth;
using Pustok.ViewModels.Product;
using System.Linq;

namespace Pustok.Controllers;

[Route("admin/users")]
[Authorize(Roles = "admin")]
public class UserController : Controller
{
    private readonly PustokDbContext _pustokDbContext;

    public UserController(PustokDbContext pustokDbContext)
    {
        _pustokDbContext = pustokDbContext;
    }

    #region Users

    [HttpGet]
    public IActionResult Index()
    {
        var users = _pustokDbContext.Users
            .OrderBy(u => u.Name)
            .ToList();

        return View("Views/Admin/User/Users.cshtml", users);
    }

    #endregion

    #region Edit user

    [HttpGet("{id}/edit")]
    public IActionResult Edit(int id)
    {
        var user = _pustokDbContext.Users
            .FirstOrDefault(o => o.Id == id);

        if (user == null)
            return NotFound();

        var model = new UserUpdateViewModel
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            IsAdmin = user.IsAdmin,
            Email = user.Email
        };

        return View("Views/Admin/User/UserEdit.cshtml", model);
    }

    [HttpPost("{id}/edit")]
    public IActionResult Edit([FromRoute] int id, [FromForm] UserUpdateViewModel model)
    {
        var user = _pustokDbContext.Users
            .FirstOrDefault(o => o.Id == id);

        if (user == null)
            return NotFound();

        user.Name = model.Name;
        user.LastName = model.LastName;
        user.IsAdmin = model.IsAdmin;

        _pustokDbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    #endregion
}
