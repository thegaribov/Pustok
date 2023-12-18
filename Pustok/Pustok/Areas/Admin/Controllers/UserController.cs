using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Admin.ViewModels.User;
using Pustok.Contracts;
using Pustok.Database;
using Pustok.Database.DomainModels;
using System.Linq;

namespace Pustok.Areas.Admin.Controllers;

[Route("admin/users")]
[Authorize(Roles = RoleNames.SuperAdmin)]
[Area("Admin")]
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

        return View(users);
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

        return View(model);
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

