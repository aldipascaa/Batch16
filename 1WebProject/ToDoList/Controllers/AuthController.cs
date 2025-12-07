
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ToDoList.Controllers  // <-- IMPORTANT: matches your other controllers
{
    [ApiController]
    [Route("api/[controller]")] // => api/auth
    public class AuthController : ControllerBase // <-- ends with Controller
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config) => _config = config;

        public record LoginRequest(string Username, string Password);
        public record LoginResponse(string AccessToken, DateTime ExpiresAt);

        /// <summary>Login and obtain a JWT bearer token.</summary>
        [AllowAnonymous]
        [HttpPost("login")]             // => POST /api/auth/login
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest dto)
        {
            // Replace with real user validation
            if (dto.Username != "admin" || dto.Password != "password")
                return Unauthorized();

            var issuer   = _config["Jwt:Issuer"]!;
            var audience = _config["Jwt:Audience"]!;
            var secret   = _config["Jwt:Secret"]!;
            var expires  = DateTime.UtcNow.AddMinutes(
                _config.GetValue<int>("Jwt:AccessTokenExpirationMinutes", 60));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, dto.Username),
                new Claim(ClaimTypes.Name, dto.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer, audience, claims,
                expires: expires, signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new LoginResponse(tokenString, expires));
        }
    }
}
