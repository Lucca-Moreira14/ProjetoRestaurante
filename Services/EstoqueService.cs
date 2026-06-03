public class EstoqueService
{
    private readonly List<Ingrediente> ingredientes = new();

    public void AdicionarIngrediente(Ingrediente ingrediente)
    {
        ingredientes.Add(ingrediente);
    }

    public void ListarIngredientes()
    {
        foreach (var item in ingredientes)
        {
            Console.WriteLine(item.Nome);
        }
    }
}