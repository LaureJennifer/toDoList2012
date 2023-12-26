using Microsoft.AspNetCore.Mvc;

namespace toDoList2012.Data
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
