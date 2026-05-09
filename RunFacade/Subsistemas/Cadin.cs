namespace RunFacade.Subsistemas
{
    public static class Cadin
    {
        public static bool EstaNoCadin(Entities.Cliente cliente)
        {
            Console.WriteLine($"[CONSULTA] Verificando {cliente.Nome} no CADIN...");
            return false; // Retorna falso para simular que o cliente está "limpo"
        }
    }
}
