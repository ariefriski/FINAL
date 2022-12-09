using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController<OrderRepository, Order>
    {
        private OrderRepository orderRepository;
        public OrderController(OrderRepository orderRepository) : base(orderRepository)
        {
         this.orderRepository= orderRepository;
        }
    }
}
