using ADevSolvedIt.Data;
using ADevSolvedIt.Data.Entities;
using ADevSolvedIt.Interfaces;

namespace ADevSolvedIt.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void UpdateTagCounts(List<string> tagNames)
        {
            foreach (var tagName in tagNames)
            {
                var tagNameSearchTerm = $",{tagName},";

                var usageCount = _context.Posts.Count(p => p.Tags.Contains(tagNameSearchTerm));

                var tag = _context.Tags.FirstOrDefault(t => t.Name == tagName);

                if (tag == null)
                {
                    if (usageCount > 0)
                    {
                        tag = new Tag
                        {
                            Name = tagName,
                            UsageCount = usageCount
                        };
                        _context.Tags.Add(tag);
                    }
                }
                else
                {
                    if (usageCount == 0)
                    {
                        _context.Tags.Remove(tag);
                    }
                    else
                    {
                        tag.UsageCount = usageCount;
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
