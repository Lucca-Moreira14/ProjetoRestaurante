public class Mesa
{
    public int Numero { get; private set; }
    public int Capacidade { get; private set; }
    public string Status { get; private set; } = string.Empty;

    public Mesa() { }

    public Mesa(int numero, int capacidade)
    {
        Numero = numero;
        Capacidade = capacidade;
        Status = "Livre";
    }

    public void DefinirNumero(int numero)
    {
        Numero = numero;
    }

    public void DefinirCapacidade(int capacidade)
    {
        Capacidade = capacidade;
    }

    public void OcuparMesa()
    {
        Status = "Ocupada";
    }

    public void LiberarMesa()
    {
        Status = "Livre";
    }
}