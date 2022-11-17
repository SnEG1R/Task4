using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Task4.Application.Common.Constants;

namespace Task4.Application.CQs.User.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ModelStateDictionary>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public LoginQueryHandler(UserManager<Domain.User> userManager, 
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ModelStateDictionary> Handle(LoginQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            request.ModelState.AddModelError("user-valid", 
                "This user does not exist");
            return request.ModelState;
        }

        if (user.Status == UserStatuses.Block)
        {
            request.ModelState.AddModelError("user-valid", 
                "This user is blocked");
            return request.ModelState;
        }

        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isCorrectPassword)
        {
            request.ModelState.AddModelError("user-valid", 
                "This user does not exist");
            return request.ModelState;
        }

        await _signInManager.SignInAsync(user, true);

        return request.ModelState;
    }
}