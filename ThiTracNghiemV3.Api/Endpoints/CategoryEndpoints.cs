//namespace ThiTracNghiemV3.Api.Endpoints;

//public class CategoryEndpoints
//{
//}

using ThiTracNghiemV3.Api.Services;
using ThiTracNghiemV3.Shared;
using ThiTracNghiemV3.Shared.DTOs;

namespace ThiTracNghiemV3.Api.Endpoints
{
  public static class CategoryEndpoints
  {
    public static IEndpointRouteBuilder MappingCategoryEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
      var categoryGroup = endpointRouteBuilder.MapGroup("/api/categories")
        .RequireAuthorization();
      categoryGroup.MapGet("", async (CategoryService categoryService) =>
      Results.Ok(await categoryService.GetCategoriesAsync()));

      categoryGroup.MapPost("", async (CategoryDTO categoryDTO, CategoryService categoryService) =>
      Results.Ok(await categoryService.SaveCategoryAsync(categoryDTO)))
        .RequireAuthorization(r => r.RequireRole(nameof(UserRole.Admin)));

      return endpointRouteBuilder;
    }
  }
}
