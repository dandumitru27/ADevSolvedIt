using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using ADevSolvedIt.Interfaces;
using ADevSolvedIt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ADevSolvedIt.Pages;

public class PostEditModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public Post Post { get; set; }

    [BindProperty]
    public string Website { get; set; }

    private readonly ApplicationDbContext _context;
    private readonly ITagService _tagService;
    private readonly ISendGridService _sendGridService;
    private readonly UserManager<ApplicationUser> _userManager;

    public PostEditModel(
        ApplicationDbContext context,
        ITagService tagService,
        ISendGridService sendGridService,
        UserManager<ApplicationUser> userManager
    )
    {
        _context = context;
        _tagService = tagService;
        _sendGridService = sendGridService;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var applicationUser = await _userManager.GetUserAsync(User);

        if (Id != 0)
        {
            Post = await _context.Posts.FindAsync(Id);

            Post.Tags = Post.Tags[1..^1];

            var isAdmin = await _userManager.IsInRoleAsync(applicationUser, "Admin");

            var canEditPost = applicationUser.Id == Post.UserId || isAdmin;
            if (!canEditPost)
            {
                return Forbid();
            }
        }
        else
        {
            Post = new Post();

            Website = applicationUser.Website;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Post post;
        if (Id != 0)
        {
            post = await _context.Posts.FindAsync(Id);
            if (post == null)
            {
                return NotFound();
            }
        }
        else
        {
            post = new Post
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreationDate = DateTime.UtcNow
            };

            _context.Posts.Add(post);

            var user = await _context.Users.FindAsync(post.UserId);
            user.Website = Website;

        }

        var oldTags = post.Tags != null ? GetTagsAsList(post.Tags) : new List<string>();
        var newTags = GetTagsAsList(Post.Tags);

        post.Title = Post.Title;
        post.ProblemMarkdown = Post.ProblemMarkdown;
        post.SolutionMarkdown = Post.SolutionMarkdown;
        post.Tags = $",{string.Join(",", newTags)},";

        post.Slug = SlugGenerator.GenerateSlug(Post.Title);

        post.ProblemHtml = MarkdownService.ToSafeHtml(Post.ProblemMarkdown);
        post.SolutionHtml = MarkdownService.ToSafeHtml(Post.SolutionMarkdown);

        post.LastEditDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var allTags = oldTags.Concat(newTags).Distinct().ToList();

        _tagService.UpdateTagCounts(allTags);

        if (Id == 0)
        {
            var emailSubject = "New Post - A Dev - " + post.Title[..Math.Min(post.Title.Length, 40)];

            await _sendGridService.SendEmailAsync(post.ProblemHtml, emailSubject);
        }

        return Redirect($"/{post.Id}/{post.Slug}");
    }

    private static List<string> GetTagsAsList(string tags)
    {
        return tags
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToList();
    }
}
