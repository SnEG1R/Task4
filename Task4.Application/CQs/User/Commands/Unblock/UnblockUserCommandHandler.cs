using MediatR;
using Task4.Application.Common.Constants;
using Task4.Application.Interfaces;

namespace Task4.Application.CQs.User.Commands.Unblock;

public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, Unit>
{
    private readonly IApplicationContext _context;

    public UnblockUserCommandHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UnblockUserCommand request, 
        CancellationToken cancellationToken)
    {
        var users = _context.Users
            .Where(u => request.UserIds
                .Contains(u.Id));
        
        foreach (var user in users)
        {
            user.Status = UserStatuses.Unblock;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}