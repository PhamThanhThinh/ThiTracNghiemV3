using Refit;
using ThiTracNghiemV3.Shared.DTOs;

namespace ThiTracNghiemV3.Web.APIs
{
  public interface IAuthenApi
  {
    [Post("/api/authen/login")]
    Task<AuthenResponseDto> LoginAsync(LoginDto loginDto);
  }
}
