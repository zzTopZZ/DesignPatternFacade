using FacadeApp.Facades;
using Microsoft.AspNetCore.Mvc;
using RunFacade.Entities;

namespace FacadeApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmprestimoController : ControllerBase
{
    private readonly IMeuFacade _facade;

    public EmprestimoController(IMeuFacade facade)
    {
        _facade = facade;
    }

    [HttpPost("analisar")]
    public IActionResult Analisar([FromBody] SolicitacaoRequest request)
    {
        var cliente = new Cliente(request.NomeCliente);
        bool aprovado = _facade.ConcederEmprestimo(cliente, request.Valor);

        if (aprovado)
            return Ok(new { status = "Aprovado", mensagem = $"Crédito concedido para {cliente.Nome}" });

        return BadRequest(new { status = "Negado", mensagem = "Restrições encontradas ou limite insuficiente" });
    }
}

// DTO para receber os dados do JSON
public record SolicitacaoRequest(string NomeCliente, decimal Valor);