namespace RunFacade.Subsistemas
{
    public class Cadastro
    {
        public void CadastrarCliente(Entities.Cliente cliente)
        {
            // Simulação de persistência
            Console.WriteLine($"[LOG] {DateTime.Now}: Cliente {cliente.Nome} registrado no banco de dados.");
        }
    }
}
