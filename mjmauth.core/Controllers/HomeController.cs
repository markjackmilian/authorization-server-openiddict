using Microsoft.AspNetCore.Mvc;

namespace mjmauth.core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}