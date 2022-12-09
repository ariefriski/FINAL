using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomPictureController : BaseController<RoomPictureRepository, RoomPicture>
    {
        private readonly RoomPictureRepository roomPictureRepository;
        public RoomPictureController(RoomPictureRepository roomPictureRepository) : base(roomPictureRepository)
        {
            this.roomPictureRepository = roomPictureRepository;
        }
    }
}
