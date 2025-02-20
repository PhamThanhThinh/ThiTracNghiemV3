using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using ThiTracNghiemV3.Web;
using ThiTracNghiemV3.Web.APIs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



ConfigureRefit(builder.Services);

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

static void ConfigureRefit(IServiceCollection services)
{
  const string ApiUrl = "https://localhost:7091";
  services.AddRefitClient<IAuthenApi>()
    .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(ApiUrl));
}