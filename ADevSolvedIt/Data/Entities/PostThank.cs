using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ADevSolvedIt.Data.Entities
{
    [Index(nameof(PostId), nameof(IpHashed), IsUnique = true)]
    public class PostThank
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [StringLength(100)]
        public string IpHashed { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }


        public virtual Post Post { get; set; }
    }
}
