using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThiTracNghiemV3.Shared.DTOs
{
  public record AuthenResponseDto(string Token, string? ErrorMessage = null)
  {
    [JsonIgnore]
    public bool HasError => ErrorMessage != null;
  }
}
