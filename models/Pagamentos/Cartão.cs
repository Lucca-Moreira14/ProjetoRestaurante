public class PagamentoCartao : Pagamento
{
    public string NumeroCartao { get; private set; } = string.Empty;

    public PagamentoCartao() { }

    public PagamentoCartao(string numeroCartao)
    {
        NumeroCartao = numeroCartao;
    }

    public override void ProcessarPagamento()
    {
        Console.WriteLine("Pagamento Cartão");
    }
}