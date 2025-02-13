using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThiTracNghiemV3.Api.Data.Entities;
using ThiTracNghiemV3.Shared;

namespace ThiTracNghiemV3.Api.Data
{
  public class UngDungDbContext : DbContext
  {
    private readonly IPasswordHasher<User> _passwordHasher;

    public UngDungDbContext(DbContextOptions<UngDungDbContext> options, IPasswordHasher<User> passwordHasher) : base(options)
    {
      // khai báo construct
      _passwordHasher = passwordHasher;
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<StudentQuiz> StudentQuizzes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);


      var adminUser = new User
      {
        Id = 1,
        Name = "Thinh Pham",
        Email = "codingwiththinh@gmail.com",
        Phone = "0866439504",
        Role = nameof(UserRole.Admin)
      };

      adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, "123456");

      modelBuilder.Entity<User>()
        .HasData(adminUser);

    }

  }
}
