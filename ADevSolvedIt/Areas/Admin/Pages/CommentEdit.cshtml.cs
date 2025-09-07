using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using ADevSolvedIt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ADevSolvedIt.Areas.Admin.Pages;

public class CommentEditModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public Comment Comment { get; set; }

    private readonly ApplicationDbContext _context;

    public CommentEditModel(ApplicationDbContext context)
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

        comment.TextMarkdown = Comment.TextMarkdown;
        comment.TextHtml = MarkdownService.ToSafeHtml(Comment.TextMarkdown);

        comment.Name = Comment.Name;
        comment.Email = Comment.Email;
        comment.EmailHash = EmailService.GetEmailHash(Comment.Email);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Comments");
    }
}
