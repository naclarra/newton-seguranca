using Microsoft.AspNetCore.Mvc;

namespace NewtonSegurancaAPI.Controllers;

[Route("busca")]
public class BuscaController : Controller
{
   // CORREÇÃO: XSS Refletido mitigado com Encode
    [HttpGet]
    public IActionResult Index(string q)
    {
        if (string.IsNullOrEmpty(q)) return View();

        ViewBag.Termo = System.Web.HttpUtility.HtmlEncode(q);
        
        return View();
    }