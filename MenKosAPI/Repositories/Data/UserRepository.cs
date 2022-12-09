using MenKosAPI.Context;
using MenKosAPI.Handler;
using MenKosAPI.Models;
using MenKosAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MenKosAPI.Repositories.Data
{
    public class UserRepository : GeneralRepositories<User>
    {
        private readonly MyContext _context;
        public IConfiguration _configuration;
        public UserRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            _context = myContext;
            _configuration = configuration;
        }


        public string Login(LoginVM loginVM)
        {
            var data = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Email == loginVM.Email);
            //var vp =;
            if (data != null)
            {
                if (Hashing.ValidatePassword(loginVM.Password, data.Password))
                {

                    var claims = new[]
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email",data.Email),
                        new Claim("Role",data.Role.Name),
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

                    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                         _configuration["Jwt:Issuer"],
                         _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: cred
                        );
                    var Token = new JwtSecurityTokenHandler().WriteToken(token);
                    return Token;
                }
                return null;

            }

            return null;
        }


        public int Register(User user)
        {

            if (_context.Users.Any(x => x.Email == user.Email))
            {
                return 0;
            }

            User userNew = new User()
            {
                Email = user.Email,
                Password = Hashing.HashPassword(user.Password),
                OccupantId = user.OccupantId,
                RoleId = user.RoleId,
            };
            _context.Users.Add(userNew);
            var create = _context.SaveChanges();

            if (create > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
