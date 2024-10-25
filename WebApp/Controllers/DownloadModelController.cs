using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class DownloadModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
