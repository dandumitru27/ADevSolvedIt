using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ADevSolvedIt.Areas.Admin.Pages;

public class CommentDeleteModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public Comment Comment { get; set; }

    private readonly ApplicationDbContext _context;

    public CommentDeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Comment = await _context.Comments.FindAsync(Id);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var comment = await _context.Comments.FindAsync(Id);

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Comments");
    }
}
