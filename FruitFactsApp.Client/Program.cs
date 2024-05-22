using FruitFactsApp.Client;
using FruitFactsApp.Client.Services.Contracts;
using FruitFactsApp.Client.Services.Implementations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IFruitService, FruitService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    httpClient.BaseAddress = new Uri("https://localhost:7038");
    return new FruitService(httpClient);
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();
