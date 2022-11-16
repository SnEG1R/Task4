using System.Security.Claims;
using MediatR;

namespace Task4.Application.CQs.User.Commands.Delete;

public class DeleteUserCommand : IRequest
{
    public IEnumerable<long> UserIds { get; set; }
    
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
}