using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThiTracNghiemV3.Api.Data;
using ThiTracNghiemV3.Api.Data.Entities;
using ThiTracNghiemV3.Api.Endpoints;
using ThiTracNghiemV3.Api.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using ThiTracNghiemV3.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// khai báo dịch vụ hash password (mã hóa mật khẩu)
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

// khai báo dbcontext
// khai báo chuỗi kết nối
builder.Services.AddDbContext<UngDungDbContext>(options =>
{
  var chuoiKetNoi = builder.Configuration.GetConnectionString("ChuoiKetNoi");
  options.UseSqlServer(chuoiKetNoi);
}
);

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");
    //var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
    policy.WithOrigins(allowedOrigins)
          .AllowAnyHeader()
          .AllowAnyMethod();
  });

  //options.AddDefaultPolicy(policy =>
  //{
  //  policy.AllowAnyOrigin() // Cho phép tất cả các domain
  //        .AllowAnyHeader()
  //        .AllowAnyMethod();
  //});
});

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
  var khoaBiMat = builder.Configuration.GetValue<string>("Jwt:KhoaBiMat");
  var khoa = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(khoaBiMat));
  options.TokenValidationParameters = new TokenValidationParameters
  {
    IssuerSigningKey = khoa,
    ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
    ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
  };
});

// khai báo service cho chức năng xác thực và category
// cách viết 1:
builder.Services
    .AddTransient<AuthenService>()
    //.AddTransient<ThiTracNghiemApiResponse>()
    .AddTransient<CategoryService>();
// cách viết 2:
//builder.Services.AddTransient<AuthenService>();
//builder.Services.AddTransient<ThiTracNghiemApiResponse>();
//builder.Services.AddTransient<CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

// cách viết thứ 1:
app.MappingAuthenEndpoints()
  .MappingCategoryEndpoints();
// cách viết thứ 2:
//app.MappingAuthenEndpoints();
//app.MappingCategoryEndpoints();

app.UseAuthorization();

app.MapControllers();

app.Run();
