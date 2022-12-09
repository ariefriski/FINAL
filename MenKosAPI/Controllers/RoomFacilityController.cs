using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomFacilityController : BaseController<RoomFacilityRepository, RoomFacility>
    {
        private RoomFacilityRepository roomFacilityRepository;
        public RoomFacilityController(RoomFacilityRepository roomFacilityRepository) : base(roomFacilityRepository)
        {
            this.roomFacilityRepository = roomFacilityRepository;
        }
    }
}
