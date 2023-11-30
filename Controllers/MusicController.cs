using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class MusicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
