using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using ADevSolvedIt.Interfaces;
using ADevSolvedIt.Services;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ADevSolvedIt.Pages;

[ValidateReCaptcha]
public class PostModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Slug { get; set; }


    public Post Post { get; set; }
    
    public string MostSignificantTag { get; set; }
    public List<Post> RelatedPosts { get; set; }

    public List<Comment> Comments { get; set; }

    [BindProperty]
    public Comment NewComment { get; set; }

    public bool HasUserClickedThanks { get; set; }

    public bool ShowEditButton { get; set; }


    private readonly ApplicationDbContext _context;
    private readonly ISendGridService _sendGridService;
    private readonly UserManager<ApplicationUser> _userManager;

    public PostModel(ApplicationDbContext context, ISendGridService sendGridService, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _sendGridService = sendGridService;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await RetrieveDataAsync();

        if (Slug != Post.Slug)
        {
            return RedirectToPagePermanent("Post", routeValues: new { id = Id, slug = Post.Slug });
        }

        await IncrementViewCountAsync();

        return Page();
    }

    private async Task RetrieveDataAsync()
    {
        Post = await _context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == Id);

        Comments = await _context.Comments
            .Where(c => c.PostId == Id)
            .OrderBy(c => c.CreatedOn)
            .Take(30)
            .ToListAsync();

        var tags = Post.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        MostSignificantTag = await _context.Tags
            .Where(t => tags.Contains(t.Name) && t.UsageCount > 1)
            .OrderByDescending(t => t.UsageCount)
            .Select(t => t.Name)
            .FirstOrDefaultAsync();

        if (MostSignificantTag != null)
        {
            RelatedPosts = await _context.Posts
                .Where(p => p.Tags.Contains($",{MostSignificantTag},") && p.Id != Id)
                .OrderByDescending(p => p.ThanksCount)
                .ThenByDescending(p => p.Views)
                .Take(3)
                .ToListAsync();
        }
        else
        {
            RelatedPosts = await _context.Posts
                .OrderByDescending(p => p.LastEditDate)
                .Take(3)
                .ToListAsync();
        }

        string ipHashed = IPService.HashIPWithSalt(HttpContext);

        HasUserClickedThanks = await _context.PostThanks.AnyAsync(pt => pt.PostId == Id && pt.IpHashed == ipHashed);

        var applicationUser = await _userManager.GetUserAsync(User);

        if (applicationUser != null)
        {
            var isAdmin = await _userManager.IsInRoleAsync(applicationUser, "Admin");

            ShowEditButton = applicationUser.Id == Post.UserId || isAdmin;
        }
    }

    private async Task IncrementViewCountAsync()
    {
        var userAgent = Request.Headers.UserAgent.ToString();

        if (!CrawlerDetectionService.IsCrawler(userAgent))
        {
            await _context.Database.ExecuteSqlRawAsync("UPDATE Posts SET Views = Views + 1 WHERE Id = {0}", Id);
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState["Recaptcha"]?.ValidationState == ModelValidationState.Invalid)
        {
            TempData["CaptchaErrorMessage"] = "Your request was flagged as spam by ReCaptcha. Please try again or contact us if this is a mistake.";

            await RetrieveDataAsync();

            return Page();
        }

        NewComment.Id = 0;
        NewComment.PostId = Id;

        if (string.IsNullOrWhiteSpace(NewComment.Email))
        {
            NewComment.Email = "anon" + Guid.NewGuid() + "@dummy.com";
        }

        NewComment.EmailHash = EmailService.GetEmailHash(NewComment.Email);
        NewComment.TextHtml = MarkdownService.ToSafeHtml(NewComment.TextMarkdown);

        NewComment.CreatedOn = DateTime.UtcNow;

        _context.Comments.Add(NewComment);
        await _context.SaveChangesAsync();

        Post = await _context.Posts.FindAsync(Id);

        var emailSubject = "Comment - A Dev - " + Post.Title[..Math.Min(Post.Title.Length, 40)];

        await _sendGridService.SendEmailAsync(NewComment.TextHtml, emailSubject);

        return RedirectToPage(
            "Post",
            pageHandler: null,
            routeValues: new { id = Id, slug = Post.Slug },
            fragment: "comments");
    }

    public async Task<IActionResult> OnPostAddThanks()
    {
        string ipHashed = IPService.HashIPWithSalt(HttpContext);

        var postThank = new PostThank
        {
            PostId = Id,
            IpHashed = ipHashed,
            CreatedOn = DateTime.UtcNow
        };

        _context.PostThanks.Add(postThank);
        await _context.SaveChangesAsync();

        Post = await _context.Posts.FindAsync(Id);
        Post.ThanksCount = await _context.PostThanks.CountAsync(pt => pt.PostId == Id);

        await _context.SaveChangesAsync();

        return new JsonResult(Post.ThanksCount);
    }
}
