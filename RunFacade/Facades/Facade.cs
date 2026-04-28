﻿using RunFacade.Entities;
using RunFacade.Subsistemas;

namespace RunFacade.Facades;

public interface IMeuFacade
{
    bool ConcederEmprestimo(Cliente cliente, decimal valor);
}

public class MeuFacade : IMeuFacade
{
    private readonly Cadastro _cadastro;
    private readonly Cadin _cadin;
    private readonly Serasa _serasa;
    private readonly LimiteCredito _limite;

    // O .NET injeta automaticamente essas classes aqui
    public MeuFacade(Cadastro cadastro, Cadin cadin, Serasa serasa, LimiteCredito limite)
    {
        _cadastro = cadastro;
        _cadin = cadin;
        _serasa = serasa;
        _limite = limite;
    }

    public bool ConcederEmprestimo(Cliente cliente, decimal valor)
    {
        _cadastro.CadastrarCliente(cliente);
        if (_cadin.EstaNoCadin(cliente)) return false;
        if (_serasa.PossuiPendencias(cliente)) return false;

        return _limite.PossuiLimite(cliente, valor);
    }
}