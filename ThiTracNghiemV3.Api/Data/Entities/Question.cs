using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThiTracNghiemV3.Api.Data.Entities
{
  // lưu nội dung câu hỏi của câu trắc nghiệm đó
  public class Question
  {
    [Key]
    public int Id { get; set; }

    // trường lưu nội dung của câu hỏi trong bài trắc nghiệm
    [MaxLength(250)]
    public string Text { get; set; }

    public Guid QuizId { get; set; }

    [ForeignKey(nameof(QuizId))]
    public virtual Quiz Quiz { get; set; }

    // 1 câu hỏi có nhiều câu trả lời
    public virtual ICollection<Option> Options { get; set; } = [];
  }
}
