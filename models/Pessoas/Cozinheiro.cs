public class Cozinheiro : Funcionario
{
    public string Especialidade { get; private set; } = string.Empty;

    public Cozinheiro() { }

    public Cozinheiro(string especialidade)
    {
        Especialidade = especialidade;
    }

    public void DefinirEspecialidade(string especialidade)
    {
        Especialidade = especialidade;
    }

    public override void ExecutarTarefa()
    {
        System.Console.WriteLine("Preparando Prato.");
    }
    public void AtualizarStatus()
    {
        System.Console.WriteLine("Status do cozinheiro atualizado.");
    }

}