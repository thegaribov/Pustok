using System.Collections.Generic;

namespace Pustok.Areas.Client.ViewModels;

public class UserConnectionViewModel
{
    public int UserId { get; set; }
    public List<string> ConnectionIds { get; set; }
}
