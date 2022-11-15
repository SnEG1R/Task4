using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Task4.MVC.Models;

public class RegistrationVm
{
    [Required(ErrorMessage = "The field is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field is required")] 
    [EmailAddress(ErrorMessage = "Email entered incorrectly")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field is required")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again!")]
    public string ConfirmPassword { get; set; }
}