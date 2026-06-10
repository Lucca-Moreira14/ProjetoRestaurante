using System.Globalization;
using Microsoft.AspNetCore.Localization;

// Verifica se o argumento --menu foi passado para rodar o menu de terminal
if (args.Contains("--menu"))
{
    TerminalMenu.Run();
    return;
}

var builder = WebApplication.CreateBuilder(args);

var ptBr = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = ptBr;
CultureInfo.DefaultThreadCurrentUICulture = ptBr;

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<CardapioService>();
builder.Services.AddSingleton<PedidoService>();
builder.Services.AddSingleton<EstoqueService>();
builder.Services.AddSingleton<RestauranteState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ptBr),
    SupportedCultures = [ptBr],
    SupportedUICultures = [ptBr]
});

SeedApplicationData(app.Services);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurante}/{action=Index}/{id?}");

app.Run();

static void SeedApplicationData(IServiceProvider services)
{
    var cardapio = services.GetRequiredService<CardapioService>();
    var estoque = services.GetRequiredService<EstoqueService>();
    var estado = services.GetRequiredService<RestauranteState>();

    if (cardapio.ListarPratos().Count == 0)
    {
        var hamburguer = new Prato("Hambúrguer da Casa", 35);
        hamburguer.AtualizarDescricao("Pão, carne artesanal e molho especial.");
        cardapio.AdicionarPrato(hamburguer);

        var pizza = new Prato("Pizza Margherita", 50);
        pizza.AtualizarDescricao("Molho de tomate, muçarela e manjericão.");
        cardapio.AdicionarPrato(pizza);

        var sobremesa = new Prato("Pudim", 18);
        sobremesa.AtualizarDescricao("Sobremesa clássica da casa.");
        cardapio.AdicionarPrato(sobremesa);
    }

    if (estoque.ListarIngredientes().Count == 0)
    {
        estoque.AdicionarIngrediente(new Ingrediente("Pão brioche", 24, "un", 6));
        estoque.AdicionarIngrediente(new Ingrediente("Queijo muçarela", 8, "kg", 3));
        estoque.AdicionarIngrediente(new Ingrediente("Tomate", 12, "kg", 4));
    }

    if (estado.Clientes.Count == 0)
    {
        estado.Clientes.Add(new Cliente(1));
        estado.Clientes.Add(new Cliente(2));
    }

    if (estado.Funcionarios.Count == 0)
    {
        estado.Funcionarios.Add(new Garcom("Salão"));
        estado.Funcionarios.Add(new Cozinheiro("Massas"));
        estado.Funcionarios.Add(new Gerente(10));
    }

        // _ = pedidoService; // Removed unused variable
}