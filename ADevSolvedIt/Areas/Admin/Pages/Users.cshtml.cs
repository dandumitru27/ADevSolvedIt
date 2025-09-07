using ADevSolvedIt.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ADevSolvedIt.Areas.Admin.Pages;

public class UsersModel : PageModel
{
    public IList<ApplicationUser> Users{ get; set; } = new List<ApplicationUser>();

    private readonly UserManager<ApplicationUser> _userManager;

    public UsersModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task OnGetAsync()
    {
        Users = await _userManager.Users
            .OrderByDescending(p => p.CreatedOn)
            .Take(50)
            .ToListAsync();
    }
}
