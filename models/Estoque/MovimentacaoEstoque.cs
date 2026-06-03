public class MovimentacaoEstoque
{
    public string tipo { get; private set; } = string.Empty;
    public DateTime Data { get; private set; } = DateTime.Now; // DateTime serve para registrar quando a movimentacao ocorreu (Datetime.now por ex)
    public double Quantidade { get; private set; }

    public MovimentacaoEstoque() { }

    public MovimentacaoEstoque(string tipo, DateTime data, double quantidade)
    {
        this.tipo = tipo;
        Data = data;
        Quantidade = quantidade;
    }
}