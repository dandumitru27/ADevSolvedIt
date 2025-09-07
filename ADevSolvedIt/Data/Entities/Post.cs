using System.ComponentModel.DataAnnotations;

namespace ADevSolvedIt.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        [StringLength(250)]
        public string Slug { get; set; }

        [Required]
        public string ProblemMarkdown { get; set; }

        [Required]
        public string ProblemHtml { get; set; }

        [Required]
        public string SolutionMarkdown { get; set; }

        [Required]
        public string SolutionHtml { get; set; }

        [Required]
        [StringLength(250)]
        public string Tags { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime LastEditDate { get; set; }

        public int ThanksCount { get; set; }

        public int Views { get; set; }


        public virtual ApplicationUser User { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
