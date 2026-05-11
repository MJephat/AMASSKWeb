using AMASSKWeb;
using AMASSKWeb.Pages.Service;
using AMASSKWeb.Repo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<AuthHandler>();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthHandler>();

    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        //BaseAddress = new Uri("https://aura-amassk-backend-fxcah5dkbpexc2gn.southafricanorth-01.azurewebsites.net/")

        //production
        BaseAddress = new Uri("https://bdn-ama-ssk-backend-v1-agcybpc6drcgfrhq.southafricanorth-01.azurewebsites.net/")
    };
});

builder.Services.AddScoped<IAdminRepo, AdminRepo>();
await builder.Build().RunAsync();
