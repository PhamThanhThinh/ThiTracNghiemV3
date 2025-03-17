using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ThiTracNghiemV3.Shared;
using ThiTracNghiemV3.Web;
using ThiTracNghiemV3.Web.APIs;
using ThiTracNghiemV3.Web.Authen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<DangKyTaiKhoanHeThongThiTracNghiem>();
builder.Services
  .AddSingleton<AuthenticationStateProvider, DangKyTaiKhoanHeThongThiTracNghiem>(sp => sp.GetRequiredService<DangKyTaiKhoanHeThongThiTracNghiem>());

ConfigureRefit(builder.Services);

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

static void ConfigureRefit(IServiceCollection services)
{
  const string ApiUrl = "https://localhost:7091";

  //services.AddRefitClient<IAuthenApi>()
  //  .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(ApiUrl));
  //services.AddRefitClient<ICategoryApi>()
  //  .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(ApiUrl));

  services.AddRefitClient<IAuthenApi>()
    .ConfigureHttpClient(SetHttpClient);
  services.AddRefitClient<ICategoryApi>(GetRefitSettings)
    .ConfigureHttpClient(SetHttpClient);

  static void SetHttpClient(HttpClient httpClient) =>
    httpClient.BaseAddress = new Uri(ApiUrl);
  static RefitSettings GetRefitSettings(IServiceProvider serviceProvider)
  {
    var authStateProvider = serviceProvider
      .GetRequiredService<DangKyTaiKhoanHeThongThiTracNghiem>();

    return new RefitSettings
    {
      //AuthorizationHeaderValueGetter = (_, __) => Task.FromResult("TOKEN")
      //AuthorizationHeaderValueGetter = (_, __) => Task.FromResult("KhoaBiMat")
      //AuthorizationHeaderValueGetter = (_, __) => Task.FromResult("tB8nZ!xw2p@V4Qf#M6sLk9&yR3dPj*Gz;")
      AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(authStateProvider.User?.Token ?? "")
      //AuthorizationHeaderValueGetter = async (_, __) =>
      //{
      //  var tokenService = serviceProvider.GetRequiredService<ITokenService>();
      //  return await tokenService.GetAccessTokenAsync() ?? string.Empty;
      //}
    };
  }
}