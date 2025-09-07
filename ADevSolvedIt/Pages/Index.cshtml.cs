using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ADevSolvedIt.Pages
{
    public class IndexModel : PageModel
    {
        public IList<PostDto> Posts { get; set; }
        public IList<Tag> Tags { get; set; }

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Posts = await _context.Posts
                .OrderByDescending(p => p.LastEditDate)
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    Tags = p.Tags,
                    LastEditDate = p.LastEditDate,
                    UserEmailHash = p.User.EmailHash,
                    UserName = p.User.Name,
                    ThanksCount = p.ThanksCount,
                    Views = p.Views,
                    CommentCount = p.Comments.Count()
                })
                .Take(40)
                .ToListAsync();

            Tags = await _context.Tags
                .OrderByDescending(t => t.UsageCount)
                .ThenBy(t => t.Name)
                .Take(30)
                .ToListAsync();
        }
    }
}
