using ThiTracNghiemV3.Api.Services;
using ThiTracNghiemV3.Shared.DTOs;

namespace ThiTracNghiemV3.Api.Endpoints
{
  public static class AuthenEndpoints
  {
    public static IEndpointRouteBuilder MappingAuthenEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
      endpointRouteBuilder.MapPost("/api/authen/login", async (LoginDto loginDto, AuthenService authenService) =>
      Results.Ok(await authenService.LoginAsync(loginDto))
      );
      return endpointRouteBuilder;
    }
  }
}
