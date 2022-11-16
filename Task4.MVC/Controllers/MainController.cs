using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4.Application.CQs.User.Commands.Block;
using Task4.Application.CQs.User.Commands.Delete;
using Task4.Application.CQs.User.Commands.Unblock;
using Task4.Application.CQs.User.Queries.GetListUser;
using Task4.Domain;

namespace Task4.MVC.Controllers;

[Authorize]
public class MainController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly IMediator _mediator;

    public MainController(IMediator mediator, SignInManager<User> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var query = new GetListUserQuery();
        var users = await _mediator.Send(query);
        
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Block([FromBody] IEnumerable<long> ids)
    {
        var command = new BlockUserCommand()
        {
            Ids = ids,
            ClaimsPrincipal = User
        };
        await _mediator.Send(command);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Unblock([FromBody] IEnumerable<long> ids)
    {
        var command = new UnblockUserCommand() { UserIds = ids };
        await _mediator.Send(command);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromBody] IEnumerable<long> ids)
    {
        var command = new DeleteUserCommand() { UserIds = ids };
        await _mediator.Send(command);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        
        return RedirectToAction(nameof(Index));
    }
}