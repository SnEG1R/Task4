using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task4.Application.CQs.User.Queries.Login;
using Task4.MVC.Models;

namespace Task4.MVC.Controllers;

public class LoginController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public LoginController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Index(LoginVm model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var query = _mapper.Map<LoginQuery>(model);
        query.ModelState = ModelState;

        var modelState = await _mediator.Send(query);

        return !modelState.IsValid
            ? View(model)
            : RedirectToAction("Index", "Main");
    }
}