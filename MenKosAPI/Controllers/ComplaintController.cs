using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : BaseController<ComplaintRepository, Complaint>
    {
        private ComplaintRepository complaintRepository;
        public ComplaintController(ComplaintRepository complaintRepository) : base(complaintRepository)
        {
            this.complaintRepository = complaintRepository;
        }
    }
}
