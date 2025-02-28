using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using ThiTracNghiemV3.Shared;

namespace ThiTracNghiemV3.Web.Authen
{
  //kiểm tra tài khoản đã được đăng ký trong hệ thống
  public class DangKyTaiKhoanHeThongThiTracNghiem : AuthenticationStateProvider
  {
    // ctrl + K + C để comment
    // ctrl + K + U mở comment
    // ctrl / => cũng để comment và mở comment
    //private const string Authen = "custom";
    private const string Authen = "admin";
    //private const string AuthenType = "thitracnghiem-authen";
    private const string UserDataKey = "user";

    private Task<AuthenticationState> _authenStateTask;
    private readonly IJSRuntime _jSRuntime;

    // khai báo constructor
    public DangKyTaiKhoanHeThongThiTracNghiem(IJSRuntime jSRuntime)
    {
      var identity = new ClaimsIdentity();
      var user = new ClaimsPrincipal(identity);
      var authenState = new AuthenticationState(user);

      _jSRuntime = jSRuntime;
      //_authenStateTask = Task.FromResult(authenState);
      SetAuthenStateTask();
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
      _authenStateTask;

    public CheckNguoiDungDangNhap User { get; private set; }
    public bool CheckNullThongTinUser => User?.Id > 0;

    //public async Task SetLoginAsync(CheckNguoiDungDangNhap user)
    //{
    //  User = user;
    //  await _jSRuntime.InvokeVoidAsync("localStorage.setItem", UserDataKey, user.ToJson());

    //  var identity = new ClaimsIdentity(User.GetClaims(), );
    //  var user = new ClaimsPrincipal(identity);
    //  var authenState = new AuthenticationState(user);

    //  NotifyAuthenticationStateChanged(_authenStateTask);
    //}

    public async Task SetLoginAsync(CheckNguoiDungDangNhap user)
    {
      User = user;

      //var identity = new ClaimsIdentity(user.GetClaims(), Authen);
      //var userPrincipal = new ClaimsPrincipal(identity);

      //var authenState = new AuthenticationState(userPrincipal);
      //_authenStateTask = Task.FromResult(authenState);

      SetAuthenStateTask();

      NotifyAuthenticationStateChanged(_authenStateTask);

      await _jSRuntime.InvokeVoidAsync("localStorage.setItem", UserDataKey, user.ToJson());

    }

    public async Task SetLogoutAsync()
    {
      //   ❤❤❤❤❤❤❤❤
      User = null;
      //var identity = new ClaimsIdentity();
      //var user = new ClaimsPrincipal(identity);
      //var authenState = new AuthenticationState(user);

      SetAuthenStateTask();

      NotifyAuthenticationStateChanged(_authenStateTask);

      await _jSRuntime.InvokeVoidAsync("localStorage.removeItem", UserDataKey);
    }

    public async Task InitializeAsync()
    {
      var userData = await _jSRuntime.InvokeAsync<string?>("localStorage.getItem", UserDataKey);
      if (string.IsNullOrWhiteSpace(userData))
      {
        return;
      }

      var user = JsonSerializer.Deserialize<CheckNguoiDungDangNhap>(userData);
    }

    //private void SetAuthSateTask()
    //{
    //  //if ()
    //  //{

    //  //}
    //}

    private void SetAuthenStateTask()
    {
      if (CheckNullThongTinUser)
      {
        var identity = new ClaimsIdentity(User.GetClaims(), Authen);
        var userPrincipal = new ClaimsPrincipal(identity);
        var authenState = new AuthenticationState(userPrincipal);

        _authenStateTask = Task.FromResult(authenState);

      }
      else
      {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        var authenState = new AuthenticationState(user);
      }
    }

  }
}
