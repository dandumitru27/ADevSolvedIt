using System.ComponentModel.DataAnnotations;

namespace ADevSolvedIt.Data.Entities;

public class Comment
{
    public int Id { get; set; }

    [Required]
    public int PostId { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(32)]
    public string EmailHash { get; set; }

    [Required]
    [StringLength(5000)]
    public string TextMarkdown { get; set; }

    [Required]
    public string TextHtml { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }


    public virtual Post Post { get; set; }
}
