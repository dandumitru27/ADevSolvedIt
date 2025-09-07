using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using ADevSolvedIt.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ADevSolvedIt.Areas.Admin.Pages
{
    public class PostDeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly ITagService _tagService;

        public PostDeleteModel(ApplicationDbContext context, ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
        }

        public async Task OnGetAsync()
        {
            Post = await _context.Posts.FindAsync(Id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var post = await _context.Posts.FindAsync(Id);

            var tagList = post.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            _tagService.UpdateTagCounts(tagList);

            return RedirectToPage("./Index");
        }
    }
}
