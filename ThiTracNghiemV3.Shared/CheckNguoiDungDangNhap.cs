using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ThiTracNghiemV3.Shared
{
  public record CheckNguoiDungDangNhap(int Id, string Name, string Role, string Token)
  {
    public string ToJson() => JsonSerializer.Serialize(this);

    public Claim[] GetClaims() => [
      new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
      new Claim(ClaimTypes.Name, Name),
      new Claim(ClaimTypes.Role, Role),
      new Claim(nameof(Token), Token)
      ];
    //public Claim[] ToClaims() => [
    //  new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
    //  new Claim(ClaimTypes.Name, Name),
    //  new Claim(ClaimTypes.Role, Role),
    //  new Claim(nameof(Token), Token)
    //  ];

    // ctrl K C
    // ctrl K U
    public static CheckNguoiDungDangNhap LoadFrom(string json) =>
      !string.IsNullOrWhiteSpace(json)
      ? JsonSerializer.Deserialize<CheckNguoiDungDangNhap>(json) : null;

    //public static CheckNguoiDungDangNhap? LoadFrom(string json) =>
    //  string.IsNullOrEmpty(json) ? null : TryDeserialize(json);

    //private static CheckNguoiDungDangNhap? TryDeserialize(string json)
    //{
    //  try
    //  {

    //  }
    //  catch
    //  {
    //    return null;
    //  }
    //}



  }
}
