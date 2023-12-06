using System.Collections.Generic;

namespace Pustok.ViewModels;

public class UserConnectionViewModel
{
    public int UserId { get; set; }
    public List<string> ConnectionIds { get; set; }
}
