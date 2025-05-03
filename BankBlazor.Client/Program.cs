using BankBlazor.Client.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BankBlazor.Client;

public class Program
{
    public static async Task Main(string[] args)
    {

        // Create a new WebAssemblyHostBuilder
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Add HttpClient for server-side API calls
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7068") });
        builder.Services.AddScoped<AccountListViewModel>();
        builder.Services.AddScoped<CustomerListViewModel>();
        // Add other services as needed
        await builder.Build().RunAsync();

    }
}
