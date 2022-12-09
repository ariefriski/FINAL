using Microsoft.AspNetCore.Mvc;

namespace MenKosClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
