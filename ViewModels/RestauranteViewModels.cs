public sealed class RestauranteDashboardViewModel
{
    public int TotalPratos { get; set; }

    public int TotalPedidos { get; set; }

    public int TotalIngredientes { get; set; }

    public int TotalClientes { get; set; }

    public int TotalFuncionarios { get; set; }
}

public sealed class CardapioViewModel
{
    public List<Prato> Pratos { get; set; } = new();

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public double Preco { get; set; }
}

public sealed class EstoqueViewModel
{
    public List<Ingrediente> Ingredientes { get; set; } = new();

    public string Nome { get; set; } = string.Empty;

    public double Quantidade { get; set; }

    public string UnidadeMedida { get; set; } = "Un";

    public double QuantidadeMinima { get; set; } = 5;
}

public sealed class PedidosViewModel
{
    public List<Pedido> Pedidos { get; set; } = new();

    public List<Prato> PratosDisponiveis { get; set; } = new();

    public int IdCliente { get; set; } = 1;

    public int NumeroMesa { get; set; } = 1;

    public int? IndicePrato { get; set; }
}

public sealed class ClientesViewModel
{
    public List<Cliente> Clientes { get; set; } = new();
}

public sealed class FuncionariosViewModel
{
    public List<Funcionario> Funcionarios { get; set; } = new();
}
