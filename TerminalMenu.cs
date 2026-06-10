public static class TerminalMenu
{
    private static readonly CardapioService cardapioService = new();
    private static readonly PedidoService pedidoService = new();
    private static readonly EstoqueService estoqueService = new();
    private static readonly RestauranteState estado = new();

    public static void Run()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         Fast - Pedidos (Menu)          ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Dashboard");
            Console.WriteLine("2. Cardápio");
            Console.WriteLine("3. Pedidos");
            Console.WriteLine("4. Estoque");
            Console.WriteLine("5. Clientes");
            Console.WriteLine("6. Funcionários");
            Console.WriteLine("0. Sair");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    MostrarDashboard();
                    break;
                case "2":
                    MostrarCardapio();
                    break;
                case "3":
                    MostrarPedidos();
                    break;
                case "4":
                    MostrarEstoque();
                    break;
                case "5":
                    MostrarClientes();
                    break;
                case "6":
                    MostrarFuncionarios();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("\nAté logo!");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void MostrarDashboard()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║           Dashboard                    ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine($"📋 Pratos no cardápio: {cardapioService.ListarPratos().Count}");
        Console.WriteLine($"🛒 Total de pedidos: {pedidoService.ListarPedidos().Count}");
        Console.WriteLine($"📦 Ingredientes em estoque: {estoqueService.ListarIngredientes().Count}");
        Console.WriteLine($"👥 Clientes cadastrados: {estado.Clientes.Count}");
        Console.WriteLine($"👔 Funcionários: {estado.Funcionarios.Count}");
        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void MostrarCardapio()
    {
        bool voltar = false;
        while (!voltar)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         Cardápio                      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Listar pratos");
            Console.WriteLine("2. Adicionar prato");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarPratos();
                    break;
                case "2":
                    AdicionarPrato();
                    break;
                case "0":
                    voltar = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ListarPratos()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║         Pratos do Cardápio            ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        var pratos = cardapioService.ListarPratos();
        if (pratos.Count == 0)
        {
            Console.WriteLine("Nenhum prato cadastrado.");
        }
        else
        {
            Console.WriteLine("PRATO                           PREÇO");
            Console.WriteLine("────────────────────────────────────────");
            foreach (var prato in pratos)
            {
                Console.WriteLine($"{prato.Nome,-30} R$ {prato.Preco:F2}");
                if (!string.IsNullOrWhiteSpace(prato.Descricao))
                    Console.WriteLine($"  └─ {prato.Descricao}");
            }
        }

        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void AdicionarPrato()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║        Adicionar Novo Prato            ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        Console.Write("Nome do prato: ");
        string? nome = Console.ReadLine();

        Console.Write("Descrição (opcional): ");
        string? descricao = Console.ReadLine();

        Console.Write("Preço: ");
        if (double.TryParse(Console.ReadLine(), out double preco))
        {
            var prato = new Prato(nome ?? "Sem nome", preco);
            if (!string.IsNullOrWhiteSpace(descricao))
                prato.AtualizarDescricao(descricao);
            
            cardapioService.AdicionarPrato(prato);
            Console.WriteLine("\n✓ Prato adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("\n✗ Preço inválido!");
        }

        Console.ReadKey();
    }

    private static void MostrarPedidos()
    {
        bool voltar = false;
        while (!voltar)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         Pedidos                        ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Listar pedidos");
            Console.WriteLine("2. Criar novo pedido");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarPedidos();
                    break;
                case "2":
                    CriarPedido();
                    break;
                case "0":
                    voltar = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ListarPedidos()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║         Lista de Pedidos               ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        var pedidos = pedidoService.ListarPedidos();
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido cadastrado.");
        }
        else
        {
            Console.WriteLine("ID    STATUS         DATA/HORA          TOTAL");
            Console.WriteLine("────────────────────────────────────────────────");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"{pedido.NumeroPedido,-5} {pedido.Status,-13} {pedido.DataHora:dd/MM HH:mm}    R$ {pedido.ValorTotal:F2}");
            }
        }

        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void CriarPedido()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║        Criar Novo Pedido               ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        Console.Write("ID do cliente: ");
        if (int.TryParse(Console.ReadLine(), out int idCliente))
        {
            var cliente = estado.ObterOuCriarCliente(idCliente);

            Console.Write("Número da mesa: ");
            if (int.TryParse(Console.ReadLine(), out int numeroMesa))
            {
                var pedido = new PedidoPresencial(numeroMesa);
                pedido.DefinirNumeroPedido(pedidoService.ListarPedidos().Count + 1);
                pedido.DefinirCliente(cliente);

                var pratos = cardapioService.ListarPratos();
                if (pratos.Count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Prato inicial (opcional):");
                    Console.WriteLine("0. Sem prato inicial");
                    for (int i = 0; i < pratos.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {pratos[i].Nome} - R$ {pratos[i].Preco:F2}");
                    }

                    Console.Write("Escolha um item: ");
                    if (int.TryParse(Console.ReadLine(), out int opcaoPrato)
                        && opcaoPrato > 0
                        && opcaoPrato <= pratos.Count)
                    {
                        pedido.AdicionarItem(pratos[opcaoPrato - 1]);
                        pedido.CalcularTotal();
                    }
                }
                
                pedidoService.CriarPedido(pedido);
                cliente.AdicionarPedido(pedido);

                Console.WriteLine($"\n✓ Pedido #{pedido.NumeroPedido} criado com sucesso!");
            }
            else
            {
                Console.WriteLine("\n✗ Número de mesa inválido!");
            }
        }
        else
        {
            Console.WriteLine("\n✗ ID do cliente inválido!");
        }

        Console.ReadKey();
    }

    private static void MostrarEstoque()
    {
        bool voltar = false;
        while (!voltar)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         Estoque                        ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Listar ingredientes");
            Console.WriteLine("2. Adicionar ingrediente");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarIngredientes();
                    break;
                case "2":
                    AdicionarIngrediente();
                    break;
                case "0":
                    voltar = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ListarIngredientes()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║         Ingredientes em Estoque        ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        var ingredientes = estoqueService.ListarIngredientes();
        if (ingredientes.Count == 0)
        {
            Console.WriteLine("Nenhum ingrediente em estoque.");
        }
        else
        {
            Console.WriteLine("INGREDIENTE                     QUANTIDADE");
            Console.WriteLine("────────────────────────────────────────────");
            foreach (var ing in ingredientes)
            {
                Console.WriteLine($"{ing.Nome,-30} {ing.Quantidade}");
            }
        }

        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void AdicionarIngrediente()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║      Adicionar Ingrediente             ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        Console.Write("Nome do ingrediente: ");
        string? nome = Console.ReadLine();

        Console.Write("Quantidade: ");
        if (double.TryParse(Console.ReadLine(), out double quantidade))
        {
            var ingrediente = new Ingrediente(nome ?? "Sem nome", quantidade, "Un", 5);
            estoqueService.AdicionarIngrediente(ingrediente);
            Console.WriteLine("\n✓ Ingrediente adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("\n✗ Quantidade inválida!");
        }

        Console.ReadKey();
    }

    private static void MostrarClientes()
    {
        bool voltar = false;
        while (!voltar)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         Clientes                       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Listar clientes");
            Console.WriteLine("2. Cadastrar cliente");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarClientes();
                    break;
                case "2":
                    CadastrarCliente();
                    break;
                case "0":
                    voltar = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ListarClientes()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║         Clientes Cadastrados           ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        if (estado.Clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            Console.WriteLine("ID    NOME                         TELEFONE");
            Console.WriteLine("────────────────────────────────────────────");
            foreach (var cliente in estado.Clientes)
            {
                Console.WriteLine($"{cliente.IdCliente,-5} {cliente.nome,-30} {cliente.telefone}");
            }
        }

        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void CadastrarCliente()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║      Cadastrar Novo Cliente            ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        Console.Write("ID do cliente: ");
        if (int.TryParse(Console.ReadLine(), out int idCliente))
        {
            var cliente = estado.ObterOuCriarCliente(idCliente);
            
            Console.Write("Nome: ");
            string? nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome))
            {
                // Usando reflexão para setar a propriedade
                var prop = cliente.GetType().GetProperty("nome", 
                    System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                prop?.SetValue(cliente, nome);
            }

            Console.Write("Telefone: ");
            string? telefone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(telefone))
            {
                var prop = cliente.GetType().GetProperty("telefone",
                    System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                prop?.SetValue(cliente, telefone);
            }

            Console.WriteLine("\n✓ Cliente cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("\n✗ ID inválido!");
        }

        Console.ReadKey();
    }

    private static void MostrarFuncionarios()
    {
        bool voltar = false;
        while (!voltar)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         Funcionários                   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Listar funcionários");
            Console.WriteLine("2. Cadastrar Garçom");
            Console.WriteLine("3. Cadastrar Cozinheiro");
            Console.WriteLine("4. Cadastrar Gerente");
            Console.WriteLine("0. Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarFuncionarios();
                    break;
                case "2":
                    CadastrarGarcom();
                    break;
                case "3":
                    CadastrarCozinheiro();
                    break;
                case "4":
                    CadastrarGerente();
                    break;
                case "0":
                    voltar = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ListarFuncionarios()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║      Funcionários Cadastrados          ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        if (estado.Funcionarios.Count == 0)
        {
            Console.WriteLine("Nenhum funcionário cadastrado.");
        }
        else
        {
            Console.WriteLine("NOME                           CARGO");
            Console.WriteLine("────────────────────────────────────────");
            foreach (var funcionario in estado.Funcionarios)
            {
                var cargo = funcionario.GetType().Name;
                Console.WriteLine($"{funcionario.nome,-30} {cargo}");
            }
        }

        Console.WriteLine();
        Console.Write("Pressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    private static void CadastrarGarcom()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║      Cadastrar Novo Garçom             ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        Console.Write("Nome: ");
        string? nome = Console.ReadLine();

        Console.Write("Setor (ex: Salão): ");
        string? setor = Console.ReadLine();

        var garcom = new Garcom(setor ?? "Salão");
        
        // Setar o nome
        var prop = garcom.GetType().BaseType?.GetProperty("nome",
            System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        prop?.SetValue(garcom, nome);

        estado.Funcionarios.Add(garcom);
        Console.WriteLine("\n✓ Garçom cadastrado com sucesso!");
        Console.ReadKey();
    }

    private static void CadastrarCozinheiro()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║    Cadastrar Novo Cozinheiro           ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        Console.Write("Nome: ");
        string? nome = Console.ReadLine();

        Console.Write("Especialidade (ex: Massas): ");
        string? especialidade = Console.ReadLine();

        var cozinheiro = new Cozinheiro(especialidade ?? "Geral");
        
        // Setar o nome
        var prop = cozinheiro.GetType().BaseType?.GetProperty("nome",
            System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        prop?.SetValue(cozinheiro, nome);

        estado.Funcionarios.Add(cozinheiro);
        Console.WriteLine("\n✓ Cozinheiro cadastrado com sucesso!");
        Console.ReadKey();
    }

    private static void CadastrarGerente()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║      Cadastrar Novo Gerente            ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();

        Console.Write("Nome: ");
        string? nome = Console.ReadLine();

           Console.Write("Nível de Acesso (1-10): ");
           if (int.TryParse(Console.ReadLine(), out int nivelAcesso))
        {
               var gerente = new Gerente(nivelAcesso);
            
            // Setar o nome
            var prop = gerente.GetType().BaseType?.GetProperty("nome",
                System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            prop?.SetValue(gerente, nome);

            estado.Funcionarios.Add(gerente);
            Console.WriteLine("\n✓ Gerente cadastrado com sucesso!");
        }
        else
        {
               Console.WriteLine("\n✗ Nível inválido!");
        }
        
        Console.ReadKey();
    }
}
