using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController<PaymentRepository, Payment>
    {
        private PaymentRepository paymentRepository;
        public PaymentController(PaymentRepository paymentRepository) : base(paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateStatusPayment(int id)
        {
            var result = paymentRepository.UpdateStatusPayment(id);

            return result switch
            {
                1 => Ok(new
                {
                    Message = "Update Payment Status Success!",
                    StatusCode = 200
                }),
                2 => BadRequest(new
                {
                    Message = "Update Payment Failed",
                    StatusCode = 400
                })
            };
        }

        [HttpGet("getpayment")]
        public IActionResult GetPayment()
        {
            var data = paymentRepository.GetPayment();
            return Ok( new { Data = data });
        }
    }
}
