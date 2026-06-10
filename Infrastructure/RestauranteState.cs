public sealed class RestauranteState
{
    public List<Cliente> Clientes { get; } = new();

    public List<Funcionario> Funcionarios { get; } = new();

    public List<Mesa> Mesas { get; } = new();

    public RestauranteState()
    {
        Mesas.Add(new Mesa(1, 4));
        Mesas.Add(new Mesa(2, 2));
        Mesas.Add(new Mesa(3, 6));
        Mesas.Add(new Mesa(4, 4));
    }

    public Cliente ObterOuCriarCliente(int idCliente)
    {
        var cliente = Clientes.FirstOrDefault(c => c.IdCliente == idCliente);
        if (cliente != null)
        {
            return cliente;
        }

        cliente = new Cliente(idCliente);
        Clientes.Add(cliente);
        return cliente;
    }
}
