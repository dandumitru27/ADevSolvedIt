using System.ComponentModel.DataAnnotations;

namespace ADevSolvedIt.Data.Entities
{
    public class ViewLog
    {
        public int Id { get; set; }

        public string UserAgent { get; set; }

        public bool IsCrawlerSimple { get; set; }

        public bool isCrawlerAdvanced { get; set; }

        public string PostSlug { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
