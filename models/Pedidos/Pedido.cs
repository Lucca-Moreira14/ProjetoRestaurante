public abstract class Pedido
{
    private readonly List<Prato> pratos = new();

    public int NumeroPedido { get; protected set; }

    public DateTime DataHora { get; protected set; } = DateTime.Now;

    public string Status { get; protected set; } = "Pendente";

    public double ValorTotal { get; protected set; }

    public Cliente? Cliente { get; protected set; } // ? quer dizer que o cliente pode conter null

    public IReadOnlyList<Prato> Pratos => pratos;

    public void DefinirCliente(Cliente? cliente)
    {
        Cliente = cliente;
    }

    public void DefinirNumeroPedido(int numeroPedido)
    {
        NumeroPedido = numeroPedido;
    }

    public void AdicionarItem(Prato prato)
    {
        pratos.Add(prato);
    }

    public void RemoverItem(Prato prato)
    {
        pratos.Remove(prato);
    }

    public void CalcularTotal()
    {
        ValorTotal = pratos.Sum(p => p.Preco);
    }

    public void FinalizarPedido()
    {
        Status = "Finalizado";
    }

    public abstract double CalcularTaxa();
}