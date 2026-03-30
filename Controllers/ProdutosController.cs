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

    // VULNERABILIDADE 01: Injeção SQL (Simulada para In-Memory)
    [HttpGet("buscar")]
    public IActionResult Buscar(string nome)
    {
        // Em um banco real, usaríamos FromSqlRaw.
        // No In-Memory, vamos simular a falha permitindo "payloads" via LINQ (ex: q=' OR 1=1)
        // Nota didática: O In-Memory não processa SQL real, então o aluno aprenderá o CONCEITO.
        
        // Simulação de comportamento vulnerável:
        var produtos = _db.Produtos.ToList();
        
        if (nome.Contains("' OR 1=1")) 
        {
             return Ok(produtos); // Simula o efeito de bypass do SQL Injection
        }

        var resultado = produtos.Where(p => p.Nome == nome).ToList();
        return Ok(resultado);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_db.Produtos.ToList());
    }
}
