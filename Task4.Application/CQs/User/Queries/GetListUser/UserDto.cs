using AutoMapper;
using MediatR;
using Task4.Application.Common.Mappings;

namespace Task4.Application.CQs.User.Queries.GetListUser;

public class UserDto : IMapWith<Domain.User>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateRegistration { get; set; }
    public DateTime DateLastLogin { get; set; }
    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.User, UserDto>()
            .ForMember(u => u.Id,
                c =>
                    c.MapFrom(u => u.Id))
            .ForMember(u => u.Name,
                c =>
                    c.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                c =>
                    c.MapFrom(u => u.Email))
            .ForMember(u => u.DateRegistration,
                c =>
                    c.MapFrom(u => u.DateRegistration))
            .ForMember(u => u.DateLastLogin,
                c =>
                    c.MapFrom(u => u.DateLastLogin))
            .ForMember(u => u.Status,
                c =>
                    c.MapFrom(u => u.Status));
    }
}