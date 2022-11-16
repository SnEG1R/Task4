using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Interfaces;

namespace Task4.Application.CQs.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Domain.User?>
{
    private readonly IApplicationContext _context;

    public GetUserQueryHandler(IApplicationContext context)
    {
        _context = context;
    }

    public async Task<Domain.User?> Handle(GetUserQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        return user;
    }
}