using Microsoft.AspNetCore.Mvc;
using Localizer.Models;

namespace Localizer.Controllers;

public class UsersController : Controller
{
    public IActionResult Index(string? role, string? q)
    {
        IEnumerable<User> users = MockData.Users;
        if (Enum.TryParse<UserRole>(role, true, out var r))
            users = users.Where(u => u.Role == r);
        else
            role = null;

        if (!string.IsNullOrWhiteSpace(q))
            users = users.Where(u => u.Name.Contains(q, StringComparison.OrdinalIgnoreCase) || u.Email.Contains(q, StringComparison.OrdinalIgnoreCase));

        return View(new UsersViewModel
        {
            Users = users.ToList(),
            Role = role,
            Query = q,
            Total = MockData.Users.Count,
            RoleCounts = Enum.GetValues<UserRole>().ToDictionary(x => x, x => MockData.Users.Count(u => u.Role == x)),
        });
    }
}
