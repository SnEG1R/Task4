using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Task4.Domain;

[Table("user")]
public class User : IdentityUser
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateRegistration { get; set; }
    public DateTime DateLastLogin { get; set; }
    public string Status { get; set; }
}