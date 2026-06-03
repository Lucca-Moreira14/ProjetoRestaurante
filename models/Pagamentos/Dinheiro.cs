public class PagamentoDinheiro : Pagamento
{
    public double ValorRecebido { get; private set; }

    public PagamentoDinheiro() { }

    public PagamentoDinheiro(double valorRecebido)
    {
        ValorRecebido = valorRecebido;
    }

    public override void ProcessarPagamento()
    {
        Console.WriteLine("Pagamento Dinheiro");
    }

    public void RegistrarValorRecebido(double valorRecebido)
    {
        ValorRecebido = valorRecebido;
    }

    public double CalcularTroco()
    {
        return ValorRecebido - Valor;
    }
}