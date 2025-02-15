using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ThiTracNghiemV3.Api.Data;
using ThiTracNghiemV3.Api.Data.Entities;
using ThiTracNghiemV3.Shared.DTOs;

namespace ThiTracNghiemV3.Api.Services
{
  public class AuthenService
  {
    private readonly UngDungDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthenService(UngDungDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
      _dbContext = dbContext;
      _passwordHasher = passwordHasher;
    }

    //private readonly UngDungDbContext dbContext;
    //public AuthenService(UngDungDbContext dbContext)
    //{
    //  this.dbContext = dbContext;
    //}

    public async Task LoginAsync(LoginDto loginDto)
    {
      var user = await _dbContext.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == loginDto.UserName);

      if (user == null)
      {
        // không tìm thấy user và FirstOrDefaultAsync trả về null
        // người dùng chưa đăng ký
        return;
      }

      var ketQuaXacThuc = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

      if (ketQuaXacThuc == PasswordVerificationResult.Failed)
      {
        // mật khẩu sai
        return;
      }

      // tạo khóa JWT Token
      var jwt = GeneratePassword(user);

    }

    // function tạo khóa JWT Token
    private static string GeneratePassword(User user)
    {
      Claim[] claims = [
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Name),
          new Claim(ClaimTypes.Name, user.Role),
        ];

      var khoaBiMat = ""; // lấy từ appsettings
      var khoa = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(khoaBiMat));
      var thongTinDangNhap = new SigningCredentials(khoa, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        issuer: "", 
        audience: "", 
        claims: claims, 
        signingCredentials: thongTinDangNhap,
        expires: DateTime.UtcNow.AddMinutes(500)
        );

      return null;
    }

  }
}
