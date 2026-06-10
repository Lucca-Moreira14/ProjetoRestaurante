public class Cardapio
{
    private readonly List<Prato> listaPratos = new();

    public IReadOnlyList<Prato> ListaPratos => listaPratos;

    public void AdicionarPrato(Prato prato)
    {
        listaPratos.Add(prato);
    }
}