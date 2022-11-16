using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Task4.Application.Interfaces;

namespace Task4.Application.CQs.User.Queries.GetListUser;

public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListUserVm>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetListUserQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetListUserVm> Handle(GetListUserQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetListUserVm() { Users = users };
    }
}