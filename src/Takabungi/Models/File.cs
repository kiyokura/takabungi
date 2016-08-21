using System;

namespace Takabungi.Models
{
  public class File
  {
    public string Name { get; set; }
    public long Size { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
  }
}
