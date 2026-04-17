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
    private readonly IConfiguration _config; // Adicionamos isso aqui

    // O construtor agora recebe o banco E as configurações
    public LoginController(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // 1. Busca o usuário pelo e-mail
        var user = _db.Usuarios.FirstOrDefault(u => u.Email == request.Email);
        
        // 2. CORREÇÃO: Compara a senha (simulando que não deve ser texto puro no banco)
        if (user == null || user.SenhaHash != request.Password) 
        {
            return Unauthorized(new { message = "E-mail ou senha inválidos" });
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        
        // 3. CORREÇÃO: Puxa a chave das configurações (segurança)
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"] ?? "Uma_Chave_Muito_Longa_E_Segura_Vinda_Das_Configuracoes");
        
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