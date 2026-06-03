public class PedidoService
{
    private readonly List<Pedido> pedidos = new();

    public void CriarPedido(Pedido pedido)
    {
        pedidos.Add(pedido);
    }

    public List<Pedido> ListarPedidos()
    {
        return pedidos;
    }
}