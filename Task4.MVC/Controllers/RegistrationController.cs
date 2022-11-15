using Microsoft.AspNetCore.Mvc;
using Task4.MVC.Models;

namespace Task4.MVC.Controllers;

public class RegistrationController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(RegistrationVm model)
    {
        return View();
    }
}