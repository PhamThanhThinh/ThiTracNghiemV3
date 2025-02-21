using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThiTracNghiemV3.Shared.DTOs
{
  //public record AuthenResponseDto(string Token, CheckNguoiDungDangNhap Check, string? ErrorMessage = null)
  //{
  //  [JsonIgnore]
  //  public bool HasError => ErrorMessage != null;
  //}
  public record AuthenResponseDto(CheckNguoiDungDangNhap CheckUser, string? ErrorMessage = null)
  {
    [JsonIgnore]
    public bool HasError => ErrorMessage != null;
  }
}
