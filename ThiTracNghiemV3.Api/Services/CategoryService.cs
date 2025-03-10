using Microsoft.EntityFrameworkCore;
using ThiTracNghiemV3.Api.Data;
using ThiTracNghiemV3.Api.Data.Entities;
using ThiTracNghiemV3.Shared.DTOs;

namespace ThiTracNghiemV3.Api.Services
{
  public class CategoryService
  {
    private readonly UngDungDbContext _dbContext;

    public CategoryService(UngDungDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    // lưu thông tin môn học: id, tên của môn học
    // (hoặc lưu thông tin danh mục sản phẩm trong dự án bán hàng)
    // đang code cho bảng category
    public async Task<ThiTracNghiemApiResponse> SaveCategoryAsync(CategoryDTO categoryDTO)
    {
      // xử lý ngoại lệ
      if (await _dbContext.Categories
        .AsNoTracking()
        .AnyAsync(item => item.Name == categoryDTO.Name 
        && item.Id != categoryDTO.CategoryId))
      {
        // xử lý code logic
        return ThiTracNghiemApiResponse.Fail("tên môn học bạn mới thêm vào đã tồn tại (bị trùng tên môn học)");
      }

      // categoryDTO.CategoryId == 0 đây là đối tượng mới
      if (categoryDTO.CategoryId == 0)
      {
        // tạo một môn học (hoặc một danh mục) mới
        var category = new Category
        {
          Name = categoryDTO.Name
        };
        _dbContext.Categories.Add(category);
      }
      // id > 0
      // id có trong hệ thống
      else
      {
        // cập nhật thông tin môn học (thông tin danh mục)
        // gần như không bao giờ xảy ra điều kiện id bị trùng
        var dbCategory = await _dbContext.Categories
          .FirstOrDefaultAsync(item => item.Id == categoryDTO.CategoryId);
        // trường hợp chưa có môn học nào
        if (dbCategory == null)
        {
          // ngắt
          return ThiTracNghiemApiResponse.Fail("môn học không tồn tại/bị trùng id");
        }
        dbCategory.Name = categoryDTO.Name;
        _dbContext.Categories.Update(dbCategory);
      }
      await _dbContext.SaveChangesAsync();

      // sau khi đã xử lý xong các ngoại lệ
      return ThiTracNghiemApiResponse.Success();
    }

    public async Task<CategoryDTO[]> GetCategoriesAsync() =>
      await _dbContext.Categories.AsNoTracking()
      .Select(c => new CategoryDTO
      {
        Name = c.Name,
        CategoryId = c.Id
      }).ToArrayAsync();
  }
}
