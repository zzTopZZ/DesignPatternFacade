namespace RunFacade.Subsistemas
{
    public class Serasa
    {
        public bool PossuiPendencias(Entities.Cliente cliente)
        {
            Console.WriteLine($"[CONSULTA] Verificando {cliente.Nome} no SERASA...");
            return false; // Simulação: cliente sem dívidas
        }
    }
}
