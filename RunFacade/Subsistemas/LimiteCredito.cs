namespace RunFacade.Subsistemas
{
    public class LimiteCredito
    {
        public bool PossuiLimite(Entities.Cliente cliente, decimal valor)
        {
            Console.WriteLine($"[ANALISE] Verificando limite para o valor de {valor:C}...");

            // Regra simples: para este exemplo, o limite máximo é 100.000
            return valor <= 100000;
        }
    }
}
