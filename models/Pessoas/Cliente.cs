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

    }
    public void ConsultarPedido()
    {

    }
}