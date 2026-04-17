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


    [HttpGet("buscar")]
    public IActionResult Buscar(string nome)
    {
        if (string.IsNullOrEmpty(nome))
        {
            return Ok(_db.Produtos.ToList());
        }

       
        var resultado = _db.Produtos
            .Where(p => p.Nome.Contains(nome))
            .ToList();

        return Ok(resultado);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_db.Produtos.ToList());
    }
}