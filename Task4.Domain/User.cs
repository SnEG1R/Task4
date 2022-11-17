using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.CompilerServices;

namespace Task4.Domain;

[Table("user")]
public class User : IdentityUser<long>
{
    public string Name { get; set; }
    public DateTime DateRegistration { get; set; }
    public DateTime DateLastLogin { get; set; }
    public string Status { get; set; }
}