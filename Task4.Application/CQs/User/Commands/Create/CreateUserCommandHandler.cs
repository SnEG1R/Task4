using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Task4.Application.CQs.User.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ModelStateDictionary>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(UserManager<Domain.User> userManager, IMapper mapper, 
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task<ModelStateDictionary> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var isUserExist = await _userManager.FindByEmailAsync(request.Email);
        if (isUserExist != null)
        {
            request.ModelState.AddModelError("user-exist", "User already exists");
            return request.ModelState;
        }

        var user = _mapper.Map<Domain.User>(request);
        user.DateRegistration = DateTime.UtcNow;
        user.DateLastLogin = DateTime.UtcNow;
        user.Status = "";

        await _userManager.CreateAsync(user, request.Password);
        await _signInManager.SignInAsync(user, true);

        return request.ModelState;
    }
}