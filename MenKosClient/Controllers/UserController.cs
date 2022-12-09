using Microsoft.AspNetCore.Mvc;

namespace MenKosClient.Controllers
{
    public class UserController : Controller
    {
        [Route("[controller]/paymentdeadline/{id}")]
        public IActionResult PaymentDeadline(int id)
        {
            return View(id);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
