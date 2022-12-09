using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupantController : BaseController<OccupantRepository, Occupant>
    {
        private OccupantRepository occupantRepository;
        public OccupantController(OccupantRepository occupantRepository) : base(occupantRepository)
        {
            this.occupantRepository = occupantRepository;
        }
    }
}
