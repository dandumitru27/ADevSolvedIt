using System.ComponentModel.DataAnnotations;

namespace ADevSolvedIt.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int UsageCount { get; set; }
    }
}
