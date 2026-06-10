public class Prato //representa um item do cardapio
{
    private readonly List<Ingrediente> ingredientes = new();

    public string Nome { get; private set; } = string.Empty;

    public string Descricao { get; private set; } = string.Empty;

    public double Preco { get; private set; }

    public IReadOnlyList<Ingrediente> Ingredientes => ingredientes;

    public Prato() { }

    public Prato(string nome, double preco)
    {
        Nome = nome;
        Preco = preco;
    }

    public void AtualizarNome(string nome)
    {
        Nome = nome;
    }

    public void AtualizarDescricao(string descricao)
    {
        Descricao = descricao;
    }

    public void AtualizarPreco(double preco)
    {
        Preco = preco;
    }

    public void AdicionarIngrediente(Ingrediente ingrediente) //Adiciona ingrediente a composicao do prato 
    {
        ingredientes.Add(ingrediente);
    }
}