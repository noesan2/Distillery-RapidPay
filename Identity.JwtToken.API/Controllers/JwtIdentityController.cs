using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.JwtToken.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtIdentityController : ControllerBase
    {
        private IConfiguration _configurations;

        public JwtIdentityController(IConfiguration config)
        {
            _configurations = config;
        }

        [HttpPost]
        public IActionResult GenerateJwtToken([FromBody] JwtRequestDto request)
        {
            var handler = new JwtSecurityTokenHandler();
            var secret = Encoding.UTF8.GetBytes(_configurations?.GetValue<string>("JwtSettings:Key"));

            var claims = new List<Claim> {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            , new (JwtRegisteredClaimNames.Sub, request.Email)
            , new (JwtRegisteredClaimNames.Email, request.Email)};

            foreach (var custom in request.Claims)
            {
                claims.Add(new(custom.Key.ToString(), custom.Value.ToString()!));
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _configurations?.GetValue<string>("JwtSettings:Issuer"),
                Audience = _configurations?.GetValue<string>("JwtSettings:Audience"),
                Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(_configurations.GetValue<double>("JwtSettings:TokenLifetime"))),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = handler.CreateToken(descriptor);

            return Ok(handler.WriteToken(jwtToken));

        }
    }
}
