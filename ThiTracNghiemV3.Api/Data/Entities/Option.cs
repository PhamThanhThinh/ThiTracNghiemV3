using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiemV3.Api.Data.Entities
{
  // lưu đáp án của bài kiểm tra trắc nghiệm
  // mỗi một câu trả lời thì nó có id riêng
  public class Option
  {
    // mã định danh
    [Key]
    public int Id { get; set; }

    [MaxLength(200)]
    // chứa nội dung của câu trả lời
    public string Text { get; set; }
    public bool IsCorrect { get; set; } // true -> câu trả lời đúng, false -> câu trả lời sai

    // 1 câu hỏi sẽ có nhiều đáp án
    public int QuestionId { get; set; }
    [ForeignKey(nameof(QuestionId))]
    public virtual Question Question { get; set; }
  }
}
