using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThiTracNghiemV3.Shared.DTOs
{
  public class CategoryDTO
  {
    public int CategoryId { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }
  }
}
