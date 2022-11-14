using System.ComponentModel.DataAnnotations.Schema;

namespace Task4.Domain;

[Table("user")]
public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateRegistration { get; set; }
    public DateTime DateLastLogin { get; set; }
    public string Status { get; set; }
}