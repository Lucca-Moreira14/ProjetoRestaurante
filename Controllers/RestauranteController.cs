using Microsoft.AspNetCore.Mvc;

public class RestauranteController : Controller
{
    private readonly CardapioService cardapioService;
    private readonly PedidoService pedidoService;
    private readonly EstoqueService estoqueService;
    private readonly RestauranteState estado;

    public RestauranteController(
        CardapioService cardapioService,
        PedidoService pedidoService,
        EstoqueService estoqueService,
        RestauranteState estado)
    {
        this.cardapioService = cardapioService;
        this.pedidoService = pedidoService;
        this.estoqueService = estoqueService;
        this.estado = estado;
    }

    public IActionResult Index()
    {
        var viewModel = new RestauranteDashboardViewModel
        {
            TotalPratos = cardapioService.ListarPratos().Count,
            TotalPedidos = pedidoService.ListarPedidos().Count,
            TotalIngredientes = estoqueService.ListarIngredientes().Count,
            TotalClientes = estado.Clientes.Count,
            TotalFuncionarios = estado.Funcionarios.Count
        };

        return View(viewModel);
    }

    public IActionResult Cardapio()
    {
        return View(MontarCardapioViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AdicionarPrato(CardapioViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Nome) || model.Preco <= 0)
        {
            TempData["Error"] = "Informe um nome e um preço válido para o prato.";
            return RedirectToAction(nameof(Cardapio));
        }

        var prato = new Prato(model.Nome.Trim(), model.Preco);

        if (!string.IsNullOrWhiteSpace(model.Descricao))
        {
            prato.AtualizarDescricao(model.Descricao.Trim());
        }

        cardapioService.AdicionarPrato(prato);
        TempData["Success"] = $"Prato {prato.Nome} cadastrado com sucesso.";
        return RedirectToAction(nameof(Cardapio));
    }

    public IActionResult Estoque()
    {
        return View(MontarEstoqueViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AdicionarIngrediente(EstoqueViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Nome) || model.Quantidade <= 0 || model.QuantidadeMinima < 0)
        {
            TempData["Error"] = "Informe um ingrediente válido com quantidade positiva.";
            return RedirectToAction(nameof(Estoque));
        }

        var ingrediente = new Ingrediente(
            model.Nome.Trim(),
            model.Quantidade,
            string.IsNullOrWhiteSpace(model.UnidadeMedida) ? "Un" : model.UnidadeMedida.Trim(),
            model.QuantidadeMinima);

        estoqueService.AdicionarIngrediente(ingrediente);
        TempData["Success"] = $"Ingrediente {ingrediente.Nome} cadastrado com sucesso.";
        return RedirectToAction(nameof(Estoque));
    }

    public IActionResult Pedidos()
    {
        return View(MontarPedidosViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CriarPedido(PedidosViewModel model)
    {
        if (model.IdCliente <= 0 || model.NumeroMesa <= 0)
        {
            TempData["Error"] = "Informe um cliente e uma mesa válidos.";
            return RedirectToAction(nameof(Pedidos));
        }

        var cliente = estado.ObterOuCriarCliente(model.IdCliente);
        var pedidoAberto = cliente.ObterPedidoAberto() as PedidoPresencial;
        var pedidoCriado = false;

        if (pedidoAberto == null)
        {
            pedidoAberto = new PedidoPresencial(model.NumeroMesa);
            pedidoAberto.DefinirNumeroPedido(pedidoService.ListarPedidos().Count + 1);
            pedidoAberto.DefinirCliente(cliente);
            pedidoService.CriarPedido(pedidoAberto);
            cliente.AdicionarPedido(pedidoAberto);
            pedidoCriado = true;
        }
        else
        {
            pedidoAberto.DefinirNumeroMesa(model.NumeroMesa);
        }

        var pratos = cardapioService.ListarPratos();
        if (model.IndicePrato.HasValue && model.IndicePrato.Value >= 0 && model.IndicePrato.Value < pratos.Count)
        {
            pedidoAberto.AdicionarItem(pratos[model.IndicePrato.Value]);
            pedidoAberto.CalcularTotal();
        }

        if (pedidoCriado)
        {
            TempData["Success"] = $"Pedido #{pedidoAberto.NumeroPedido} criado para o cliente {cliente.IdCliente}.";
        }
        else
        {
            TempData["Success"] = $"Pedido #{pedidoAberto.NumeroPedido} atualizado.";
        }

        return RedirectToAction(nameof(Pedidos));
    }

    public IActionResult Clientes()
    {
        return View(new ClientesViewModel
        {
            Clientes = estado.Clientes
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CriarCliente(int idCliente)
    {
        if (idCliente <= 0)
        {
            TempData["Error"] = "Informe um identificador válido.";
            return RedirectToAction(nameof(Clientes));
        }

        estado.ObterOuCriarCliente(idCliente);
        TempData["Success"] = "Cliente criado com sucesso.";
        return RedirectToAction(nameof(Clientes));
    }

    public IActionResult Funcionarios()
    {
        return View(new FuncionariosViewModel
        {
            Funcionarios = estado.Funcionarios
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CadastrarGarcom()
    {
        estado.Funcionarios.Add(new Garcom("Salão"));
        TempData["Success"] = "Garçom cadastrado com sucesso.";
        return RedirectToAction(nameof(Funcionarios));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CadastrarCozinheiro()
    {
        estado.Funcionarios.Add(new Cozinheiro("Massas"));
        TempData["Success"] = "Cozinheiro cadastrado com sucesso.";
        return RedirectToAction(nameof(Funcionarios));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CadastrarGerente()
    {
        estado.Funcionarios.Add(new Gerente(10));
        TempData["Success"] = "Gerente cadastrado com sucesso.";
        return RedirectToAction(nameof(Funcionarios));
    }

    private CardapioViewModel MontarCardapioViewModel()
    {
        return new CardapioViewModel
        {
            Pratos = cardapioService.ListarPratos()
        };
    }

    private EstoqueViewModel MontarEstoqueViewModel()
    {
        return new EstoqueViewModel
        {
            Ingredientes = estoqueService.ListarIngredientes()
        };
    }

    private PedidosViewModel MontarPedidosViewModel()
    {
        return new PedidosViewModel
        {
            Pedidos = pedidoService.ListarPedidos(),
            PratosDisponiveis = cardapioService.ListarPratos()
        };
    }
}
