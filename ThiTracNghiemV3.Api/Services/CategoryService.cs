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
    public async Task SaveCategoryAsync(CategoryDTO categoryDTO)
    {
      if (await _dbContext.Categories.AsNoTracking().AnyAsync(item => item.Name == categoryDTO.Name))
      {

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
        var dbCategory = await _dbContext.Categories
          .FirstOrDefaultAsync(item => item.Id == categoryDTO.CategoryId);
        // trường hợp chưa có môn học nào
        if (dbCategory == null)
        {
          // ngắt
          return;
        }
        dbCategory.Name = categoryDTO.Name;
        _dbContext.Categories.Update(dbCategory);
      }
      await _dbContext.SaveChangesAsync();
    }
  }
}
