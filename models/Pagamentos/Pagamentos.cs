public abstract class Pagamento
{
    public double Valor { get; protected set; }
    public string Status { get; protected set; } = string.Empty;
    public abstract void ProcessarPagamento();
}