using Microsoft.AspNetCore.Mvc;

namespace MenKosClient.Controllers
{
    public class PanjiController : Controller
    {
  

        public IActionResult Transaction()
        {
            return View();
        }

     
        public IActionResult PaymentDeadline()
        {
            return View();
        }


        [Route("[controller]/paymentdeadline/{id}")]
        public IActionResult PaymentDeadline(int id)
        {
            return View("PaymentDeadlineOccupant", id);
        }

    }
}
