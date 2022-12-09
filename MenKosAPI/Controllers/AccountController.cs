//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.ComponentModel.DataAnnotations;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq.Expressions;
//using System.Security.Claims;
//using System.Text;
//using WebApi.Context;
//using WebApi.Models;
//using WebApi.Repositories.Data;
//using WebApi.Utils;
//using WebApi.ViewModels;
//using WebAppMVC.Models;

//namespace WebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AccountController : ControllerBase
//    {

       
   
//        private readonly AccountRepository _repository;

//        public IConfiguration _configuration;


//        public AccountController(AccountRepository accountRepository, IConfiguration configuration)
//        {
//            _repository = accountRepository;
//            _configuration = configuration;
//        }

 
//        [HttpPost("Login")]
//        public IActionResult Login(RequestLogin requestLogin)
//        {


//            var user = _repository.GetUserDetails(requestLogin.Email);

//            if (user != null)
//            {
//                if (Hashing.ValidatePassword(requestLogin.Password, user.Password))
//                {

//                    //coba generate token
//                    var claims = new[] {
//                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
//                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
//                        new Claim("fullName", user.Employee.FullName),
//                        new Claim("email", user.Employee.Email),
//                        new Claim("role", user.Role.Name)
//                    };

//                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//                    var token = new JwtSecurityToken(
//                        _configuration["Jwt:Issuer"],
//                        _configuration["Jwt:Audience"],
//                        claims,
//                        expires: DateTime.UtcNow.AddMinutes(10),
//                        signingCredentials: signIn);

//                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));


//                    return Ok(
//                        new
//                        {
//                            message = "berhasil login!",
//                            statusCode = 200,
//                            data = new
//                            {
//                                id = user.Id,
//                                fullname = user.Employee.FullName,
//                                email = user.Employee.Email,
//                                role = user.Role.Name,
//                                token = new JwtSecurityTokenHandler().WriteToken(token)

//                            }
//                        });

//                }


//            }
            

//            return BadRequest(new { message = "Login gagal!", statusCode = 400 });

//        }

//        [HttpPost("Register")]
//        public IActionResult Register(RequestRegister requestRegister)
//        {

//            var employee = _repository.GetEmployee(requestRegister.Email);

//            if (employee == null)
//            {
//               var result = _repository.CreateEmployee(requestRegister.Fullname, requestRegister.Email, requestRegister.BirthDate, requestRegister.Gender, requestRegister.City);
//                if (result > 0)
//                {
//                    var employeeId = _repository.GetEmployee(requestRegister.Email).Id;
//                    var userResult = _repository.CreateUser(Hashing.HashPassword(requestRegister.Password), employeeId);
                 
//                    if (userResult > 0)
//                    {
//                        return Ok(new { message = "register berhasil!", statusCode = 200 });
//                    }

//                }
//            }
//            return BadRequest(new { message = "register gagal!", statusCode = 400 });

           
//        }

//        [Authorize]
//        [HttpPut("Change_Password")]
//        public IActionResult ChangePassword(RequestChangePassword requestChangePassword)
//        {
//            var user = _repository.GetUserEmployee(u => u.Employee.Email.Equals(requestChangePassword.Email));

//            if (user != null) {
//                if (Hashing.ValidatePassword(requestChangePassword.OldPassword, user.Password))
//                {
//                    user.Password = Hashing.HashPassword(requestChangePassword.NewPassword);
//                    var result = _repository.UpdatePassword(user);
//                    if (result > 0)
//                    {
//                        return Ok(new { message = "Ubah Password berhasil!", statusCode = 200 });
//                    }
//                }

//            }

    
//            return BadRequest(new { message = "Ubah Password gagal!", statusCode = 400 });
//        }

//        [AllowAnonymous]
//        [HttpPut("Forgot_Password")]
//        public IActionResult ForgotPassword(RequestForgotPassword requestForgotPassword)
//        {
//            var user = _repository.GetUserEmployee(u => u.Employee.Email == requestForgotPassword.Email && u.Employee.FullName == requestForgotPassword.FullName);

//            if (user != null)
//            {
//                user.Password = Hashing.HashPassword(requestForgotPassword.NewPassword);
//                var result = _repository.UpdatePassword(user);
//                if (result > 0)
//                {
//                    return Ok(new { message = "Ubah Password berhasil!", statusCode = 200 });
//                }
//            }
//            return BadRequest(new { message = "Ubah Password gagal!", statusCode = 400 });
//        }

        

//    }
//}
