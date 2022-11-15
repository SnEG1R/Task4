using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Task4.Application.Common.Mappings;

namespace Task4.Application.CQs.User.Commands.Create;

public class CreateUserCommand : IRequest<ModelStateDictionary>, IMapWith<Domain.User>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ModelStateDictionary ModelState { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCommand, Domain.User>()
            .ForMember(u => u.UserName,
                c =>
                    c.MapFrom(u => u.Name))
            .ForMember(u => u.Email,
                c =>
                    c.MapFrom(u => u.Email));
    }
}