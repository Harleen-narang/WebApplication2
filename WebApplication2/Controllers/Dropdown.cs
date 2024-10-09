using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class Dropdown : Controller
    {
        public IActionResult Drop()
        {
            return View();
        }
    }
}
