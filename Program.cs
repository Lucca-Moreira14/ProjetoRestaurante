using System;

class Program
{
    static void Main(string[] args)
    {
        var cardapio = new CardapioService();
        var pedidoService = new PedidoService();
        var estoqueService = new EstoqueService();

        var clientes = new List<Cliente>();
        var funcionarios = new List<Funcionario>();

        cardapio.AdicionarPrato(new Prato("Hamburguer", 35));
        cardapio.AdicionarPrato(new Prato("Pizza", 50));

        while (true)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("      SISTEMA RESTAURANTE");
            Console.WriteLine("=================================");
            Console.WriteLine("1 - Cardápio");
            Console.WriteLine("2 - Pedidos");
            Console.WriteLine("3 - Estoque");
            Console.WriteLine("4 - Clientes");
            Console.WriteLine("5 - Funcionários");
            Console.WriteLine("0 - Sair");

            Console.Write("\nEscolha: ");

            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    MenuCardapio(cardapio);
                    break;

                case "2":
                    MenuPedidos(cardapio, pedidoService, clientes);
                    break;

                case "3":
                    MenuEstoque(estoqueService);
                    break;

                case "4":
                    MenuClientes(clientes);
                    break;

                case "5":
                    MenuFuncionarios(funcionarios);
                    break;

                case "0":
                    return;
            }
        }
    }

    static void MenuCardapio(CardapioService cardapio)
    {
        Console.Clear();

        Console.WriteLine("===== CARDÁPIO =====");
        Console.WriteLine("1 - Listar pratos");
        Console.WriteLine("2 - Adicionar prato");

        string op = Console.ReadLine() ?? ""; // se o valor da esquerda foir nulo, use o da direita,(garantir que nunca sera null). para nunca fechar o menu.

        switch (op)
        {
            case "1":

                var pratos = cardapio.ListarPratos();

                if (pratos.Count == 0)
                {
                    Console.WriteLine("Nenhum prato cadastrado.");
                }
                else
                {
                    foreach (var prato in pratos)
                    {
                        Console.WriteLine($"{prato.Nome} - R$ {prato.Preco}");
                    }
                }

                break;

            case "2":

                Console.Write("Nome: ");
                string nome = Console.ReadLine() ?? "";

                Console.Write("Preço: ");
                double preco = Convert.ToDouble(Console.ReadLine());

                cardapio.AdicionarPrato(new Prato(nome, preco));

                Console.WriteLine("Prato cadastrado.");
                break;
        }

        Console.ReadKey();
    }
    static void MenuPedidos(
        CardapioService cardapio,
        PedidoService pedidoService,
        List<Cliente> clientes)
    {
        Console.Clear();

        Console.WriteLine("===== PEDIDOS =====");
        Console.WriteLine("1 - Criar pedido");
        Console.WriteLine("2 - Listar pedidos");

        string op = Console.ReadLine() ?? "";

        switch (op)
        {
            case "1":

                Console.Write("Id do cliente: ");

                int idCliente =
                    Convert.ToInt32(Console.ReadLine());

                Cliente? cliente = // ? deixa a variavel ser nula, ou seja, pode nao conter um cliente
                    clientes.FirstOrDefault(c =>
                    c.IdCliente == idCliente);

                if (cliente == null)
                {
                    cliente = new Cliente(idCliente);
                    clientes.Add(cliente);
                }

                Pedido pedidoAtual;

                var pedidoAberto =
                    cliente.ObterPedidoAberto();

                if (pedidoAberto != null)
                {
                    pedidoAtual = pedidoAberto;

                    Console.WriteLine(
                        $"Pedido #{pedidoAtual.NumeroPedido} encontrado.");
                }
                else
                {
                    Console.Write("Número da mesa: ");

                    int numeroMesa =
                        Convert.ToInt32(Console.ReadLine());

                    PedidoPresencial novoPedido =
                        new PedidoPresencial(numeroMesa);

                    novoPedido.DefinirNumeroPedido(
                        pedidoService.ListarPedidos().Count + 1);

                    novoPedido.DefinirCliente(cliente);

                    pedidoService.CriarPedido(novoPedido);

                    cliente.AdicionarPedido(novoPedido);

                    pedidoAtual = novoPedido;

                    Console.WriteLine(
                        $"Novo pedido #{pedidoAtual.NumeroPedido} criado.");
                }

                var pratos = cardapio.ListarPratos();

                for (int i = 0; i < pratos.Count; i++)
                {
                    Console.WriteLine(
                        $"[{i}] {pratos[i].Nome} - R$ {pratos[i].Preco}");
                }

                while (true)
                {
                    Console.Write(
                        "Escolha um prato (-1 para finalizar): ");

                    int indice =
                        Convert.ToInt32(Console.ReadLine());

                    if (indice == -1)
                        break;

                    if (indice < 0 || indice >= pratos.Count)
                    {
                        Console.WriteLine("Prato inválido.");
                        continue;
                    }

                    pedidoAtual.AdicionarItem(pratos[indice]);

                    Console.WriteLine(
                        $"{pratos[indice].Nome} adicionado.");
                }

                pedidoAtual.CalcularTotal();

                Console.WriteLine(
                    $"Pedido #{pedidoAtual.NumeroPedido} atualizado.");

                Console.WriteLine(
                    $"Total atual: R$ {pedidoAtual.ValorTotal}");

                break;

            case "2":

                foreach (var p in pedidoService.ListarPedidos())
                {
                    Console.WriteLine(
                        $"Pedido #{p.NumeroPedido} - R$ {p.ValorTotal} - {p.Status}");
                }

                break;
        }

        Console.ReadKey();
    }
    static void MenuEstoque(EstoqueService estoque)
    {
        Console.Clear();

        Console.WriteLine("===== ESTOQUE =====");
        Console.WriteLine("1 - Adicionar ingrediente");
        Console.WriteLine("2 - Listar ingredientes");

        string op = Console.ReadLine() ?? "";

        switch (op)
        {
            case "1":

                Console.Write("Nome: ");
                string nome = Console.ReadLine() ?? "";

                Console.Write("Quantidade: ");
                double quantidade =
                    Convert.ToDouble(Console.ReadLine());

                Ingrediente ingrediente =
                    new Ingrediente(
                        nome,
                        quantidade,
                        "Un",
                        5);

                estoque.AdicionarIngrediente(ingrediente);

                Console.WriteLine("Ingrediente cadastrado.");

                break;

            case "2":

                estoque.ListarIngredientes();

                break;
        }

        Console.ReadKey();
    }
    static void MenuClientes(List<Cliente> clientes)
    {
        Console.Clear();

        Console.WriteLine("===== CLIENTES =====");

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            foreach (var cliente in clientes)
            {
                double valorConta = 0;
                int numeroMesa = 0;

                var pedidoAberto = cliente.ObterPedidoAberto();

                if (pedidoAberto != null)
                {
                    valorConta = pedidoAberto.ValorTotal;

                    if (pedidoAberto is PedidoPresencial pedidoPresencial)
                    {
                        numeroMesa = pedidoPresencial.NumeroMesa;
                    }
                }

                Console.WriteLine(
                $"ID: {cliente.IdCliente} | Mesa: {numeroMesa} | Conta: R$ {valorConta:F2} | Pedidos: {cliente.Pedidos.Count}");
            }
        }

        Console.ReadKey();
    }
    static void MenuFuncionarios(
        List<Funcionario> funcionarios)
    {
        Console.Clear();

        Console.WriteLine("===== FUNCIONÁRIOS =====");
        Console.WriteLine("1 - Cadastrar Garçom");
        Console.WriteLine("2 - Cadastrar Cozinheiro");
        Console.WriteLine("3 - Cadastrar Gerente");
        Console.WriteLine("4 - Executar Tarefas");

        string op = Console.ReadLine() ?? "";

        switch (op)
        {
            case "1":

                funcionarios.Add(
                    new Garcom("Salão"));

                Console.WriteLine(
                    "Garçom cadastrado.");

                break;

            case "2":

                funcionarios.Add(
                    new Cozinheiro("Massas"));

                Console.WriteLine(
                    "Cozinheiro cadastrado.");

                break;

            case "3":

                funcionarios.Add(
                    new Gerente(10));

                Console.WriteLine(
                    "Gerente cadastrado.");

                break;

            case "4":

                foreach (var funcionario in funcionarios)
                {
                    funcionario.ExecutarTarefa();
                }

                break;
        }

        Console.ReadKey();
    }
}