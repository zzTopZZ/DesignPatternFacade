using Microsoft.AspNetCore.Mvc;
using RunFacade.DTOs.Request;
using RunFacade.DTOs.Response;
using RunFacade.Entities;
﻿using RunFacade.Facades;

namespace RunFacade.Controllers;

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
    [ProducesResponseType(typeof(AnaliseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Analisar([FromBody] SolicitacaoRequest request)
    {
        var cliente = new Cliente(request.NomeCliente);
        bool aprovado = _facade.ConcederEmprestimo(cliente, request.Valor);

        if (aprovado)
        {
            var resposta = new AnaliseResponse("Aprovado", $"Crédito concedido para {cliente.Nome}");
            return Ok(resposta);
        }

        return BadRequest(new { status = "Negado", mensagem = "Restrições..." });
    }
}

