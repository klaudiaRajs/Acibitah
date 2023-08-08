using Microsoft.AspNetCore.Mvc;

namespace Acibitah.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
