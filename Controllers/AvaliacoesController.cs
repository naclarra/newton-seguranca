using Microsoft.AspNetCore.Mvc;
using NewtonSegurancaAPI.Data;
using NewtonSegurancaAPI.Models;

namespace NewtonSegurancaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvaliacoesController : ControllerBase
{
    private readonly AppDbContext _db;

    public AvaliacoesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{produtoId}")]
    public IActionResult GetByProduto(int produtoId)
    {
        var avaliacoes = _db.Avaliacoes.Where(a => a.ProdutoId == produtoId).ToList();
        return Ok(avaliacoes);
    }

    // VULNERABILIDADE: XSS Armazenado
    [HttpPost]
    public IActionResult Criar([FromBody] Avaliacao avaliacao)
    {
        // VULNERÁVEL: Salva o comentário sem nenhuma sanitização
        _db.Avaliacoes.Add(avaliacao);
        _db.SaveChanges();
        return Ok(avaliacao);
    }
}
