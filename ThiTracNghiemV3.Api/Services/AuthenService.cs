using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ThiTracNghiemV3.Api.Data;
using ThiTracNghiemV3.Api.Data.Entities;
using ThiTracNghiemV3.Shared;
using ThiTracNghiemV3.Shared.DTOs;

namespace ThiTracNghiemV3.Api.Services
{
  public class AuthenService
  {
    private readonly UngDungDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthenService(UngDungDbContext dbContext, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
      _dbContext = dbContext;
      _passwordHasher = passwordHasher;
      _configuration = configuration;
    }

    //private readonly UngDungDbContext dbContext;
    //public AuthenService(UngDungDbContext dbContext)
    //{
    //  this.dbContext = dbContext;
    //}

    public async Task<AuthenResponseDto> LoginAsync(LoginDto loginDto)
    {
      var user = await _dbContext.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Email == loginDto.UserName);

      if (user == null)
      {
        // không tìm thấy user và FirstOrDefaultAsync trả về null
        // người dùng chưa đăng ký
        return new AuthenResponseDto(default, "Không có User này trong hệ thống");
      }

      var xacThucMatKhau = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

      if (xacThucMatKhau == PasswordVerificationResult.Failed)
      {
        // mật khẩu sai
        return new AuthenResponseDto(default, "Mật khẩu sai");
      }

      // tạo khóa JWT Token
      var jwt = GenerateJwtToken(user);
      var checkNguoiDungDangNhap = new CheckNguoiDungDangNhap(user.Id, user.Name, user.Role, jwt);
      return new AuthenResponseDto(checkNguoiDungDangNhap);
    }

    // function tạo khóa JWT Token
    private string GenerateJwtToken(User user)
    {
      Claim[] claims = [
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Name),
          new Claim(ClaimTypes.Name, user.Role),
        ];

      //var khoaBiMat = "KhoaBiMat"; // lấy từ appsettings
      //var khoaBiMat = _configuration.GetValue<string>("Jwt");
      var khoaBiMat = _configuration.GetValue<string>("Jwt:KhoaBiMat");
      var khoa = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(khoaBiMat));
      var thongTinDangNhap = new SigningCredentials(khoa, SecurityAlgorithms.HmacSha256);

      var jwtSecurityToken = new JwtSecurityToken(
        issuer: _configuration.GetValue<string>("Jwt:Issuer"), // lấy từ appsettings
        audience: _configuration.GetValue<string>("Jwt:Audience"), // lấy từ appsettings
        claims: claims, 
        signingCredentials: thongTinDangNhap,
        expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpiresMinutes"))
        );

      var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

      return token;
    }

  }
}
