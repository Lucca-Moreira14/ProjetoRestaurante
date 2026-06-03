public class PagamentoPix : Pagamento
{
    public string ChavePix { get; private set; } = string.Empty;

    public PagamentoPix() { }

    public PagamentoPix(string chavePix)
    {
        ChavePix = chavePix;
    }

    public override void ProcessarPagamento()
    {
        Console.WriteLine("Pagamento PIX");
    }
}