using Pustok.Contracts;
using System.Collections.Generic;

namespace Pustok.Areas.Admin.ViewModels.User;

public class UserUpdateViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role[] SelectedRoles { get; set; }
}