class PedidoDelivery : Pedido
{
    public string EnderecoEntrega { get; private set; } = string.Empty;

    public double TaxaEntrega { get; private set; }

    public PedidoDelivery() { }

    public PedidoDelivery(string enderecoEntrega, double taxaEntrega)
    {
        EnderecoEntrega = enderecoEntrega;
        TaxaEntrega = taxaEntrega;
    }

    public void DefinirEnderecoEntrega(string enderecoEntrega)
    {
        EnderecoEntrega = enderecoEntrega;
    }

    public void DefinirTaxaEntrega(double taxaEntrega)
    {
        TaxaEntrega = taxaEntrega;
    }

    public override double CalcularTaxa()
    {
        return TaxaEntrega;
    }
}
