using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiTracNghiemV3.Shared.DTOs
{
  public record ThiTracNghiemApiResponse(bool IsSuccess, string? ErrorMessage)
  {
    public static ThiTracNghiemApiResponse Success() => new(true, null);
    public static ThiTracNghiemApiResponse Fail(string errorMessage) => new(false, errorMessage);
  }
}

/*

public class ThiTracNghiemApiResponse
{
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }

    private ThiTracNghiemApiResponse(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static ThiTracNghiemApiResponse Success()
    {
        return new ThiTracNghiemApiResponse(true, null);
    }

    public static ThiTracNghiemApiResponse Fail(string errorMessage)
    {
        return new ThiTracNghiemApiResponse(false, errorMessage);
    }
} 
 
*/