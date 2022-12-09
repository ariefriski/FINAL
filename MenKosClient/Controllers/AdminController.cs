using Microsoft.AspNetCore.Mvc;

namespace MenKosClient.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult index()
        {
            return View();
        }

        public IActionResult Transaction()
        {
            return View();
        }


        public IActionResult PaymentDeadline()
        {
            return View();
        }

        public IActionResult ComplaintList()
        {
            return View();
        }

        public IActionResult AddKamar()
        {
            return View();
        }

        public IActionResult EditKamar()
        {
            return View();
        }
        public IActionResult DeletedKamar()
        {
            return View();
        }
        public IActionResult DetailKamar()
        {
            return View();
        }
        public IActionResult PageLanding()
        {
            return View();
        }

    }
}
