using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}