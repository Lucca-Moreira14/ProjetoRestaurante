public class PedidoPresencial : Pedido
{
    public int NumeroMesa { get; private set; }

    public PedidoPresencial() { }

    public PedidoPresencial(int numeroMesa)
    {
        NumeroMesa = numeroMesa;
    }

    public void DefinirNumeroMesa(int numeroMesa)
    {
        NumeroMesa = numeroMesa;
    }

    public override double CalcularTaxa()
    {
        return 0;
    }
}