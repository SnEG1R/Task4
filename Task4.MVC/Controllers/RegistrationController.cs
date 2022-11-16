using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task4.Application.CQs.User.Commands.Create;
using Task4.MVC.Models;

namespace Task4.MVC.Controllers;

public class RegistrationController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RegistrationController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(RegistrationVm model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var command = _mapper.Map<CreateUserCommand>(model);
        command.ModelState = ModelState;

        var modelState = await _mediator.Send(command);

        return !modelState.IsValid
            ? View(model)
            : RedirectToAction("Index", "Main");
    }
}