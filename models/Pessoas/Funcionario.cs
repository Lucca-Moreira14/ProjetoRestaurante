abstract class Funcionario : Pessoa //classe abstrada, base para garcon, cozinheiro e gerente
{
    public int idFuncionario { get; protected set; }
    public double salario { get; protected set; }
    public string cargo { get; protected set; } = string.Empty;

    public abstract void ExecutarTarefa();

}