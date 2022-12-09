using MenKosAPI.Base;
using MenKosAPI.Models;
using MenKosAPI.Repositories.Data;
using MenKosAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenKosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserRepository, User>
    {
        private readonly UserRepository userRepository;
        public UserController(UserRepository userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginVM loginVM)
        {
            var token = userRepository.Login(loginVM);
            try
            {
                if (token == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Login gagal",
                    });
                }

                return Ok(new
                {
                    Message = "Sukses",
                    Token = token
                });
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

        [HttpPost("Register")]
        public ActionResult Register([FromBody] User user)
        {
            var data = userRepository.Register(user);

            try
            {
                if (data == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Error Email Sudah Terdaftar!"
                    });
                }
                else
                {
                    // string token = CreateToken(email);
                    return Ok(new
                    {
                        Message = "Register Berhasil",
                        Data = data
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
