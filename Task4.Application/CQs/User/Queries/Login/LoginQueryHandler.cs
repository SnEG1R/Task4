using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isCorrectPassword)
        {
            request.ModelState.AddModelError("user-valid", 
                "This user does not exist");
            return request.ModelState;
        }

        await _signInManager.SignInAsync(user, false);
        
        return request.ModelState;
    }
}