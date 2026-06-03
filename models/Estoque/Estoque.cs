class Estoque
{
    private readonly List<Ingrediente> ingredientes = new();

    public IReadOnlyList<Ingrediente> Ingredientes => ingredientes;

    public void AdicionarIngrediente(Ingrediente ingrediente)
    {
        ingredientes.Add(ingrediente);
    }
}