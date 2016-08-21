using System.Collections.Generic;

namespace Takabungi.Models.ViewModels
{
  public class LogFilesViewModel
  {
    public Environment Environment { get; set; }

    public IEnumerable<File> Files { get; set; }
  }
}
