using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task4.Application.Common.Constants;
using Task4.Application.CQs.User.Commands.Block;
using Task4.Application.CQs.User.Queries.GetListUser;

namespace Task4.MVC.Controllers;

[Authorize]
public class MainController : Controller
{
    private readonly IMediator _mediator;

    public MainController(IMediator mediator)
    {
        _mediator = mediator;
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
}