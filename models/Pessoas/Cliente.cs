public class Cliente : Pessoa
{
    public int IdCliente { get; private set; }
    public int pontosFidelidade { get; private set; }
    private readonly List<Pedido> pedidos = new();

    public IReadOnlyList<Pedido> Pedidos => pedidos; //Lista apenas leitura, para evitar que seja modificada fora da classe Cliente.

    public Cliente() { } // como é um construtor vazio, nao inicializa nada alem da classe.

    public Cliente(int idCliente)
    {
        IdCliente = idCliente;
    }

    public void DefinirIdCliente(int idCliente)
    {
        IdCliente = idCliente;
    }

    public void DefinirPontosFidelidade(int pontosFidelidade)
    {
        this.pontosFidelidade = pontosFidelidade;
    }

    public void AdicionarPedido(Pedido pedido)
    {
        pedidos.Add(pedido);
    }

    public void FazerPedido()
    {
        var pedido = new PedidoPresencial();
        pedido.DefinirNumeroPedido(pedidos.Count + 1);
        pedidos.Add(pedido);
        System.Console.WriteLine($"Pedido #{pedido.NumeroPedido} criado para cliente {IdCliente}");
    }

    public void ConsultarPedido()
    {
        if (pedidos.Count == 0)
        {
            System.Console.WriteLine("Nenhum pedido encontrado para este cliente.");
            return;
        }

        foreach (var p in pedidos)
        {
            System.Console.WriteLine($"Pedido #{p.NumeroPedido} - Status: {p.Status} - Valor: {p.ValorTotal}");
        }
    }
    public Pedido? ObterPedidoAberto() //Retorna o pedido que ainda nao foi finalizado, caso nao exista pedido retorna null
    {   
        return pedidos.FirstOrDefault(p => p.Status == "Pendente");
    }   
}