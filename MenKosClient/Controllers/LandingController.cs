using Microsoft.AspNetCore.Mvc;

namespace MenKosClient.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RentDetail()
        {
            return View();
        }
    }
}
