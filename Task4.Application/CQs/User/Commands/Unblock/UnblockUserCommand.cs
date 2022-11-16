using MediatR;

namespace Task4.Application.CQs.User.Commands.Unblock;

public class UnblockUserCommand : IRequest
{
    public IEnumerable<long> UserIds { get; set; }
}