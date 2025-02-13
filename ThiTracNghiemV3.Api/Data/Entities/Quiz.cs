using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiemV3.Api.Data.Entities
{
  // lưu thông tin của bài thi trắc nghiệm
  // 1 bài thi trắc nghiệm có nhiều câu hỏi
  public class Quiz
  {
    // xxx-xxxxx-... bao gồm số và chữ cái thường
    [Key]
    public Guid Id { get; set; }

    // trường lưu tên của bài thi trắc nghiệm
    // bài kiểm tra học kỳ môn kỹ thuật lập trình
    // bài kiểm tra học kỳ môn hướng đối tượng
    [MaxLength(100)]
    public string Name { get; set; }
    
    // trường lưu số câu hỏi (đếm có bao nhiêu câu hỏi trong bài thi trắc nghiệm)
    // lưu tổng số câu hỏi bài thi trắc nghiệm đó
    public int TotalQuestions { get; set; }
    
    // trường thời gian làm bài
    // thời gian làm bài thi trắc nghiệm đó do giáo viên quy định
    // bài thi 15 phút
    // bài thi 1 tiếng = 60 phút
    // bài làm 120 phút
    public int TimeInMinutes { get; set; }

    // bài thi này có được kích hoạt hay chưa
    public bool IsActive { get; set; } // false bài thi chưa được kích hoạt, true bài thi đã được kích hoạt và sẵn sàng cho thí sinh thi

    // bài thi này thuộc môn học nào
    public int CategoryId { get; set; }

    //[ForeignKey(nameof(Quiz.CategoryId))]
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }

    // từ khóa virtual làm cho trang web nhanh hơn
    // dùng cơ chế Lazy Loading
    // câu hỏi phỏng vấn

    // một bài thi trắc nghiệm thì có nhiều câu hỏi trắc nghiệm
    // 1 bài kiểm tra có nhiều câu hỏi
    public virtual ICollection<Question> Questions { get; set; } = [];
  }
}
