using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieStoreAPI.Context;
using MovieStoreAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        
        public AuthorizationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login loginModel)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Username == loginModel.Username);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
