using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewtonSegurancaAPI.Data;
using NewtonSegurancaAPI.Models;
using System.Security.Claims;

namespace NewtonSegurancaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsuariosController(AppDbContext db)
    {
        _db = db;
    }


    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetPerfil(int id)
    {
        var usuario = _db.Usuarios.Find(id);
        if (usuario == null) return NotFound();

        // Em vez de retornar o objeto 'usuario' inteiro (com senha e tudo),
        // retornamos apenas o que é seguro.
        return Ok(new 
        {
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.Role
        });
    }

    [Authorize]
    [HttpGet("{userId}/pedidos")]
    public IActionResult MeusPedidos(int userId)
    {

        var identity = User.Identity as ClaimsIdentity;
        var loggedUserId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (loggedUserId != userId.ToString())
        {
            return Forbid(); 
        }

        var pedidos = _db.Pedidos
            .Where(p => p.UsuarioId == userId)
            .ToList();
        
        return Ok(pedidos);
    }

   
    [Authorize(Roles = "Admin")]
    [HttpGet("admin/all")]
    public IActionResult GetAllUsuarios()
    {
        return Ok(_db.Usuarios.ToList());
    }
}