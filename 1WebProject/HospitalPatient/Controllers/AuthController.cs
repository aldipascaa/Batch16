
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalPatient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config) => _config = config;

    public record LoginRequest(string Username, string Password);
    public record LoginResponse(string AccessToken, DateTime ExpiresAt);

    /// <summary>Login and obtain a JWT.</summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest dto)
    {
        // DEMO ONLY: replace with Identity or DB
        var role = dto.Username switch
        {
            "admin" => "Admin",
            "doc"   => "Doctor",
            _       => "Staff"
        };
        if (dto.Password != "Pass@123") return Unauthorized();

        var issuer   = _config["Jwt:Issuer"]!;
        var audience = _config["Jwt:Audience"]!;
        var secret   = _config["Jwt:Secret"]!;
        var expires  = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:AccessTokenExpirationMinutes", 60));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, dto.Username),
            new Claim(ClaimTypes.Name, dto.Username),
            new Claim(ClaimTypes.Role, role)
        };

        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer, audience, claims, expires: expires, signingCredentials: creds);

        return Ok(new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), expires));
    }
}
