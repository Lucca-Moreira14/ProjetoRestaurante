public class Ingrediente
{
    public string Nome { get; private set; } = string.Empty;

    public double Quantidade { get; private set; }

    public string UnidadeMedida { get; private set; } = string.Empty;

    public double QuantidadeMinima { get; private set; }

    public Ingrediente() { }

    public Ingrediente(string nome, double quantidade, string unidadeMedida, double quantidadeMinima)
    {
        Nome = nome;
        Quantidade = quantidade;
        UnidadeMedida = unidadeMedida;
        QuantidadeMinima = quantidadeMinima;
    }

    public void AtualizarNome(string nome)
    {
        Nome = nome;
    }

    public void AtualizarUnidadeMedida(string unidadeMedida)
    {
        UnidadeMedida = unidadeMedida;
    }

    public void AtualizarQuantidadeMinima(double quantidadeMinima)
    {
        QuantidadeMinima = quantidadeMinima;
    }

    public void AtualizarQuantidade(double quantidade)
    {
        Quantidade = quantidade;
    }

    public bool VerificarDisponibilidade()
    {
        return Quantidade > QuantidadeMinima;
    }
}