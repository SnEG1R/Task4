using System.Security.Claims;
using MediatR;

namespace Task4.Application.CQs.User.Commands.Block;

public class BlockUserCommand : IRequest
{
    public IEnumerable<long> UserIds { get; set; }
    
    public ClaimsPrincipal ClaimsPrincipal { get; set; }
}