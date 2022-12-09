//using MenKosAPI.Context;
//using MenKosAPI.Handler;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.JsonWebTokens;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq.Expressions;
//using System.Security.Claims;
//using System.Text;
//using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

//namespace MenKosAPI.Repositories.Data
//{
//    public class AccountRepository
//    {
//        private readonly MyContext _myContext;
//        public IConfiguration _configuration;

//        public AccountRepository(MyContext myContext, IConfiguration configuration)
//        {
//            _myContext = myContext;
//            _configuration = configuration;
//        }

//        public int Login(string email, string password)
//        {
//            var user = _myContext.Users.Include(u => u.Occupant).Include(u => u.Role).SingleOrDefault(u => u.Email == email);
//            if (user != null)
//            {
//                if (Hashing.ValidatePassword(password, user.Password))
//                {
//                    var claims = new[]
//                    {
//                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
//                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
//                        new Claim("name", user.Occupant.Name),
//                        new Claim("email", user.Email),
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


//                    return Ok(
//                      new
//                      {
//                          message = "Login Success!",
//                          statusCode = 200,
//                          data = new
//                          {
//                              occupantid = user.Occupant.Id,
//                              name = user.Occupant.Name,
//                              email = user.Email,
//                              role = user.Role.Name,
//                              token = new JwtSecurityTokenHandler().WriteToken(token)

//                          }
//                      });

//                }
//                return 3; //bad request password salah
//            }
//            return 2; //bad request user tidak ditemukan


//        }


      



//        }






//    }


