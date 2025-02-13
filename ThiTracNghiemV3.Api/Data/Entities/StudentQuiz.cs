using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThiTracNghiemV3.Api.Data.Entities
{
  // một học sinh sẽ làm nhiều bài thi trắc nghiệm
  // bảng này quyết định 1 học sinh sẽ làm bao nhiêu bài thi trắc nghiệm
  public class StudentQuiz
  {
    [Key]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Guid QuizId { get; set; }

    // học sinh đó bắt đầu thời gian thi vào thời điểm nào
    public DateTime StartedOn { get; set; }

    // học sinh đó hoàn thành bài thi vào thời gian nào
    public DateTime CompletedOn { get; set; }

    // điểm số bài thi trắc nghiệm (thang điểm 100 vì kiểu dữ liệu là kiểu int: A A+ ...)
    public int Score { get; set; }

    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }

    [ForeignKey(nameof(QuizId))]
    public Quiz Quiz { get; set; }
  }
}
