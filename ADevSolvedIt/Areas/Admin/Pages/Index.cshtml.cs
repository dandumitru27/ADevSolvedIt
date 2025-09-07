using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ADevSolvedIt.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        public IList<Post> Posts { get; set; } = new List<Post>();

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Posts = await _context.Posts
                .OrderByDescending(p => p.LastEditDate)
                .Take(50)
                .ToListAsync();
        }
    }
}
