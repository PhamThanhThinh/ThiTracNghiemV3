using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using ThiTracNghiemV3.Shared;

namespace ThiTracNghiemV3.Web.Authen
{
  //kiểm tra tài khoản đã được đăng ký trong hệ thống
  public class DangKyTaiKhoanHeThongThiTracNghiem : AuthenticationStateProvider
  {
    private const string AuthenType = "thitracnghiem-authen";

    private readonly Task<AuthenticationState> _authenStateTask;
    private readonly IJSRuntime _jSRuntime;

    public DangKyTaiKhoanHeThongThiTracNghiem(IJSRuntime jSRuntime)
    {
      var identiy = new ClaimsIdentity();
      var user = new ClaimsPrincipal(identiy);
      var authenState = new AuthenticationState(user);

      _jSRuntime = jSRuntime;
      _authenStateTask = Task.FromResult(authenState);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
      _authenStateTask;

    public CheckNguoiDungDangNhap User { get; private set; }
    public bool CheckNullThongTinUser => User?.Id > 0;

    public async Task LoginAsync(CheckNguoiDungDangNhap user)
    {
      User = user;
      await _jSRuntime.InvokeVoidAsync("localStorage.setItem", user.ToJson());

      var identity = new ClaimsIdentity(user.GetClaims(), AuthenType);
    }
  }
}
