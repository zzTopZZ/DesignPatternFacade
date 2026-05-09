﻿using RunFacade.Entities;
using RunFacade.Subsistemas;

namespace RunFacade.Facades;

public interface IMeuFacade
{
    bool ConcederEmprestimo(Cliente cliente, decimal valor);
}

public class MeuFacade : IMeuFacade
{
    public bool ConcederEmprestimo(Cliente cliente, decimal valor)
    {
        Cadastro.CadastrarCliente(cliente);
        if (Cadin.EstaNoCadin(cliente)) return false;
        if (Serasa.PossuiPendencias(cliente)) return false;

        return LimiteCredito.PossuiLimite(cliente, valor);
    }
}