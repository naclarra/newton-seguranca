using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewtonSegurancaAPI.Data;

namespace NewtonSegurancaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProdutosController(AppDbContext db)
    {
        _db = db;
    }

    // VULNERABILIDADE 01: Injeção SQL REAL
    [HttpGet("buscar")]
    public IActionResult Buscar(string nome)
    {
        // VULNERÁVEL: Concatenação direta de entrada do usuário em SQL
        var query = $"SELECT * FROM Produtos WHERE Nome LIKE '%{nome}%'";
        var resultado = _db.Produtos.FromSqlRaw(query).ToList();
        return Ok(resultado);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_db.Produtos.ToList());
    }
}
