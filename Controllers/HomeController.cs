using Microsoft.AspNetCore.Mvc;
using AspMVC.Models;

namespace AspMVC.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }
}
