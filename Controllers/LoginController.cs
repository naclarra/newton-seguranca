using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewtonSegurancaAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewtonSegurancaAPI.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _db;

    public LoginController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _db.Usuarios.FirstOrDefault(u => u.Email == request.Email && u.SenhaHash == request.Password);
        
        if (user == null) return Unauthorized();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("S3cr3t_K3y_That_Is_L0ng_En0ugh_F0r_HMAC_SHA256");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { Token = tokenHandler.WriteToken(token) });
    }
}

public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
