public class CardapioService
{
    private readonly List<Prato> pratos = new();

    public void AdicionarPrato(Prato prato)
    {
        pratos.Add(prato);
    }

    public List<Prato> ListarPratos()
    {
        return pratos;
    }
}