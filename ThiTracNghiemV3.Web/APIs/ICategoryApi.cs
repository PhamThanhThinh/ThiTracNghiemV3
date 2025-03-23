using Refit;
using ThiTracNghiemV3.Shared.DTOs;

namespace ThiTracNghiemV3.Web.APIs
{
  [Headers("Authorization: Bearer")]
  public interface ICategoryApi
  {
    // lưu dữ liệu vào database từ trình duyệt
    [Post("/api/categories")]
    Task<ThiTracNghiemApiResponse> SaveCategoryAsync(CategoryDTO categoryDTO);
    
    //show dữ liệu từ database lên trình duyệt
    [Get("/api/categories")]
    Task<CategoryDTO[]> GetCategoriesAsync();
  }
}
