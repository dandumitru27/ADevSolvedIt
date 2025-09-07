using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ADevSolvedIt.Data.Entities;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [StringLength(30)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    [StringLength(32)]
    public string EmailHash { get; set; }

    [StringLength(200)]
    public string Website { get; set; }
}
