using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ADevSolvedIt.Areas.Admin.Pages;

public class CommentsModel : PageModel
{
    public IList<Comment> Comments { get; set; } = new List<Comment>();

    private readonly ApplicationDbContext _context;

    public CommentsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Comments = await _context.Comments
            .Include(c => c.Post)
            .OrderByDescending(p => p.CreatedOn)
            .Take(50)
            .ToListAsync();
    }
}
