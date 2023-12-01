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
[Authorize(Roles = RoleNames.SuperAdmin)]
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
            .Include(u => u.UserRoles)
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
            .Include(u => u.UserRoles)
            .FirstOrDefault(o => o.Id == id);

        if (user == null)
            return NotFound();

        var model = new UserUpdateViewModel
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            SelectedRoles = user.UserRoles.Select(ur => ur.Role).ToArray()
        };

        return View("Views/Admin/User/UserEdit.cshtml", model);
    }

    [HttpPost("{id}/edit")]
    public IActionResult Edit([FromRoute] int id, [FromForm] UserUpdateViewModel model)
    {
        var user = _pustokDbContext.Users
            .Include(u => u.UserRoles)
            .FirstOrDefault(o => o.Id == id);

        if (user == null)
            return NotFound();

        user.Name = model.Name;
        user.LastName = model.LastName;

        #region Role management

        var rolesInDb = user.UserRoles.Select(pc => pc.Role);

        //Remove proces ========================================

        var removableRoles = rolesInDb
            .Where(r => !model.SelectedRoles.Contains(r))
            .ToList();

        user.UserRoles.RemoveAll(ur => removableRoles.Contains(ur.Role));


        //Add proces ========================================

        var addableRole = model.SelectedRoles
            .Where(r => !rolesInDb.Contains(r))
            .ToList();

        var newRoles = addableRole.Select(r => new UserRole
        {
            Role = r,
            UserId = user.Id,
        });

        user.UserRoles.AddRange(newRoles);

        #endregion

        _pustokDbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    #endregion
}

