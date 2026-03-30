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

    // VULNERABILIDADE 04: Exposição de Dados Sensíveis
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetPerfil(int id)
    {
        // VULNERÁVEL: Retorna a entidade completa (com SenhaHash e TokenRecuperacao)
        var usuario = _db.Usuarios.Find(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    // VULNERABILIDADE 03: Quebra de Controle de Acesso (IDOR)
    [Authorize]
    [HttpGet("{userId}/pedidos")]
    public IActionResult MeusPedidos(int userId)
    {
        // VULNERÁVEL: Não verifica se o userId solicitado pertence ao usuário logado
        var pedidos = _db.Pedidos
            .Where(p => p.UsuarioId == userId)
            .ToList();
        
        return Ok(pedidos);
    }

    // VULNERABILIDADE: Missing Function Level Access Control
    [HttpGet("admin/all")]
    // VULNERÁVEL: Falta [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsuarios()
    {
        return Ok(_db.Usuarios.ToList());
    }
}
