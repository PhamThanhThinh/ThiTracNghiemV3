using System.ComponentModel.DataAnnotations;
using ThiTracNghiemV3.Shared;

namespace ThiTracNghiemV3.Api.Data.Entities
{
  // User chứa tài khoản của 1 giáo viên và nhiều học sinh
  // tài khoản giáo viên chính là tải khoản admin
  public class User
  {
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [Length(9, 15)]
    public string Phone { get; set; }

    //[MaxLength(200)]
    public string PasswordHash { get; set; }
    //public string Role { get; set; } = "Admin";
    //public string Role { get; set; } = "Student";

    [MaxLength(20)]
    public string Role { get; set; } = nameof(UserRole.Student);

    // tài khoản có được kích hoạt chưa? có được thông qua bởi admin
    public bool IsApproved { get; set; } // true: tk được kích hoạt, false: tk chưa được kích hoạt
  }
}
