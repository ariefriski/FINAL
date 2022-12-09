using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomPriceController : BaseController<RoomPriceRepository, RoomPrice>
    {
        private readonly RoomPriceRepository roomPriceRepository;
        public RoomPriceController(RoomPriceRepository roomPriceRepository) : base(roomPriceRepository)
        {
            this.roomPriceRepository = roomPriceRepository;
        }
    }
}
