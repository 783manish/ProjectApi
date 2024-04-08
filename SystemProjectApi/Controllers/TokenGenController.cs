using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SystemProjectApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SystemProjectApi.Controllers
{
    [Route("api/[controller]")]
    public class TokenGenController : Controller
    {
        private readonly IConfiguration _configuration;

        public TokenGenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private Users AuthenticateUser(Users user)
        {
            Users _user = null;
            if (user.UserName == "s2u" && user.Password == "s2rsluser")
            {
                _user = new Users { UserName = "manish" };
            }

            return _user;
        }

        private string GenerateToken(Users users)
        {
            var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult response = Unauthorized();
            var usr = AuthenticateUser(user);
            if (usr != null)
            {
                var token = GenerateToken(usr);
                response = Ok(new { token = token });
            }

            return response;
        }
    }
}

