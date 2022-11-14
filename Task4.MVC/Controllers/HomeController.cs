using Microsoft.AspNetCore.Mvc;

namespace Task4.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}   