using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Task4.Application.Common.Mappings;
using Task4.Application.CQs.User.Commands.Create;

namespace Task4.MVC.Models;

public class RegistrationVm : IMapWith<CreateUserCommand>
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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegistrationVm, CreateUserCommand>()
            .ForMember(u => u.Name,
                c =>
                    c.MapFrom(u => u.Name))
            .ForMember(u => u.Email,
                c =>
                    c.MapFrom(u => u.Email))
            .ForMember(u => u.Password,
                c =>
                    c.MapFrom(u => u.Password));
    }
}