namespace RunFacade.DTOs.Request
{
    public record SolicitacaoRequest
    {
        public required string NomeCliente { get; init; }
        public required decimal Valor { get; init; }
    }
}
