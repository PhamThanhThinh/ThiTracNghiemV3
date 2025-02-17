using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThiTracNghiemV3.Api.Data;
using ThiTracNghiemV3.Api.Data.Entities;
using ThiTracNghiemV3.Api.Endpoints;
using ThiTracNghiemV3.Api.Services;

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

// khai báo service chức năng xác thực
builder.Services.AddTransient<AuthenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MappingAuthenEndpoints();

app.UseAuthorization();

app.MapControllers();

app.Run();
