class Gerente : Funcionario
{
    public int nivelAcesso { get; private set; }

    public Gerente() { }

    public Gerente(int nivelAcesso)
    {
        this.nivelAcesso = nivelAcesso;
    }

    public void DefinirNivelAcesso(int nivelAcesso)
    {
        this.nivelAcesso = nivelAcesso;
    }

    public override void ExecutarTarefa()
    {
        System.Console.WriteLine("Gerenciando Restaurante.");
    }
    public void GerarRelatorio()
    {
        System.Console.WriteLine("Gerando relatório resumido do restaurante (simulação).\n");
        System.Console.WriteLine("-- Relatório --");
        System.Console.WriteLine("(Este é um relatório de exemplo; conecte-se a serviços para dados reais.)");
    }
}