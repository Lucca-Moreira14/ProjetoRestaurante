class Garcom : Funcionario
{
    public string Setor { get; private set; } = string.Empty;

    public Garcom() { }

    public Garcom(string setor)
    {
        Setor = setor;
    }

    public void DefinirSetor(string setor)
    {
        Setor = setor;
    }

    public override void ExecutarTarefa()
    {
        System.Console.WriteLine("Atendendo Clientes");
    }
    public void AnotarPedido()
    {

    }
    public void FecharConta()
    {

    }
}