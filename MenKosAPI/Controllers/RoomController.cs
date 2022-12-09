using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseController<RoomRepository, Room>
    {
        private RoomRepository roomRepository;
        public RoomController(RoomRepository roomRepository) : base(roomRepository)
        {
            this.roomRepository = roomRepository;
        }


        //Create RoomPrice.
        //Create Room.
        //Create Picture Room.
        //Create Facility Room
    }
}
