using Microsoft.AspNetCore.Mvc;

namespace MenKosClient.Controllers
{
    public class OrderKamarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
