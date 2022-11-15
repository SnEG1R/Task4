using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Task4.Application.Common.Mappings;
using Task4.Application.CQs.User.Queries.Login;

namespace Task4.MVC.Models;

public class LoginVm : IMapWith<LoginQuery>
{
    [Required(ErrorMessage = "The field is required")]
    [EmailAddress(ErrorMessage = "Email entered incorrectly")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The field is required")]
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginVm, LoginQuery>()
            .ForMember(l => l.Email,
                c =>
                    c.MapFrom(l => l.Email))
            .ForMember(l => l.Password,
                c =>
                    c.MapFrom(l => l.Password));
    }
}