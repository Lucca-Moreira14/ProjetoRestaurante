using System;
class Program
{
    static void Main(string[] args)
    {
        CardapioService cardapio = new();

        Prato prato = new("Hamburguer", 35);

        cardapio.AdicionarPrato(prato);

        foreach (var p in cardapio.ListarPratos())
        {
            Console.WriteLine(p.Nome);
        }

        PagamentoPix pagamentoPix = new PagamentoPix();
        pagamentoPix.ProcessarPagamento();
    }
}