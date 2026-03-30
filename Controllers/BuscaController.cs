using Microsoft.AspNetCore.Mvc;

namespace NewtonSegurancaAPI.Controllers;

[Route("busca")]
public class BuscaController : Controller
{
    // VULNERABILIDADE 02: XSS Refletido
    [HttpGet]
    public IActionResult Index(string q)
    {
        ViewBag.Termo = q;
        return View();
    }
}
