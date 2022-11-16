using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Task4.Application.Common.Constants;
using Task4.Application.CQs.User.Queries.GetUser;
using Task4.Domain;
using Task4.MVC.Controllers;

namespace Task4.MVC.Filters;

public class UserValidationAttribute : Attribute, IAsyncActionFilter
{
    private readonly IMediator _mediator;
    private readonly SignInManager<User> _signInManager;

    public UserValidationAttribute(SignInManager<User> signInManager,
        IMediator mediator)
    {
        _signInManager = signInManager;
        _mediator = mediator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var controller = (Controller)context.Controller;
        
        var claimsPrincipal = context.HttpContext.User;

        var currentUserId = Convert.ToInt64(claimsPrincipal
            .FindFirstValue(ClaimTypes.NameIdentifier));

        var query = new GetUserQuery() { UserId = currentUserId };
        var user = await _mediator.Send(query);

        if (user == null || user.Status == UserStatuses.Block)
        {
            await _signInManager.SignOutAsync();

            context.Result = controller.RedirectToAction("Index", "Login");
            return;
        }

        await next();
    }
}