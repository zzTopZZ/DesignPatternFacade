namespace RunFacade.Subsistemas
{
    public class Cadin
    {
        public static bool EstaNoCadin(Entities.Cliente cliente)
        {
            Console.WriteLine($"[CONSULTA] Verificando {cliente.Nome} no CADIN...");
            return false; // Retorna falso para simular que o cliente está "limpo"
        }
    }
}
