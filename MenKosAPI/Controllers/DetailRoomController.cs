using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using MenKosAPI.Requests;
using MenKosAPI.Response;
using MenKosAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailRoomController : ControllerBase
    {
        private DetailRoomRepository detailRoomRepository;
        public DetailRoomController(DetailRoomRepository detailRoomRepository) 
        {
            this.detailRoomRepository = detailRoomRepository;
        }

        [HttpGet]
        public IActionResult GetDetail()
        {
            var listData = detailRoomRepository.Get();

            return Ok(new
            {
                Message = "Success Get Data",
                StatusCode = 200,
                Data = listData
            });
        } 
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var listData = detailRoomRepository.GetById(id);

            return Ok(new
            {
                Message = "Success Get Data",
                StatusCode = 200,
                Data = listData
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromForm]CreateRoomVM createRoomVM)
        {
            if (createRoomVM == null)


            {


                return BadRequest(new PostResponse { Success = false, ErrorCode = "S01", Error = "Invalid post request" });


            }


            if (string.IsNullOrEmpty(Request.GetMultipartBoundary()))


            {


                return BadRequest(new PostResponse { Success = false, ErrorCode = "S02", Error = "Invalid post header" });
            }
            if (createRoomVM.Image != null)


            {
                await detailRoomRepository.SaveImage(createRoomVM);
            }


            var postResponse = await detailRoomRepository.CreateRoom(createRoomVM);


            if (!postResponse.Success)


            {


                return NotFound(postResponse);


            }


            return Ok(postResponse.Post);
        }

        [HttpPut]
        public IActionResult Update(Room room)
        {
            try
            {
                var result = detailRoomRepository.Update(room);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "data gagal diupdate"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "data telah diupdate"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = detailRoomRepository.Delete(id);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "gagal dihapus"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "berhasil dihapus"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
    }
}
