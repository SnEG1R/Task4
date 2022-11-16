using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Task4.Application.Common.Constants;
using Task4.Application.Interfaces;

namespace Task4.Application.CQs.User.Commands.Block;

public class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, Unit>
{
    private readonly IApplicationContext _context;
    private readonly SignInManager<Domain.User> _signInManager;

    public BlockUserCommandHandler(IApplicationContext context,
        SignInManager<Domain.User> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }

    public async Task<Unit> Handle(BlockUserCommand request,
        CancellationToken cancellationToken)
    {
        var users = _context.Users
            .Where(u => request.Ids
                .Contains(u.Id));

        var currentUserId = Convert.ToInt64(request.ClaimsPrincipal
            .FindFirst(ClaimTypes.NameIdentifier)?.Value);

        foreach (var user in users)
        {
            user.Status = UserStatuses.Block;

            if (user.Id == currentUserId)
            {
                await _signInManager.SignOutAsync();
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}