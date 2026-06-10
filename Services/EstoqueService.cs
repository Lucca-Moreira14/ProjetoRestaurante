public class EstoqueService
{
    private readonly List<Ingrediente> ingredientes = new();

    public void AdicionarIngrediente(Ingrediente ingrediente)
    {
        ingredientes.Add(ingrediente);
    }

    public List<Ingrediente> ListarIngredientes()
    {
        return ingredientes;
    }
}