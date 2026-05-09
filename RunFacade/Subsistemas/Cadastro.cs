namespace RunFacade.Subsistemas
{
    public static class Cadastro
    {
        public static void CadastrarCliente(Entities.Cliente cliente)
        {
            // Simulação de persistência
            Console.WriteLine($"[LOG] {DateTime.Now}: Cliente {cliente.Nome} registrado no banco de dados.");
        }
    }
}
