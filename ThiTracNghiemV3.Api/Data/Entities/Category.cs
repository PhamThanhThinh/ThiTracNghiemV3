using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiemV3.Api.Data.Entities
{
  // bài thi đó thuộc môn học nào
  public class Category
  {
    // mã định danh
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    // trường lưu tên môn học
    public string Name { get; set; }
  }
}
