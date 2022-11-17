using MediatR;

namespace Task4.Application.CQs.User.Queries.GetUser;

public class GetUserQuery : IRequest<Domain.User?>
{
    public long UserId { get; set; }
}