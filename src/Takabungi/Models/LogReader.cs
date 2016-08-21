using System.IO;
using System.Linq;
using Takabungi.Models.ViewModels;

namespace Takabungi.Models
{
  public class LogReader
  {

    public LogFilesViewModel GetFiles(Environment target)
    {
      var di = new DirectoryInfo(target.PhysicalPath);
      var fi = di.GetFiles();
      var f = fi.Select<FileInfo, Models.File>(x => new Models.File() { Name = x.Name, Size = x.Length, CreatedDate = x.CreationTime, UpdatedDate = x.LastWriteTime });

      var viewModel = new LogFilesViewModel();
      viewModel.Files = f;
      viewModel.Environment = target;


      return viewModel;
    }

    public FileDetailViewModel GetFile(Environment target, string fileName)
    {
      var fileDetail = new FileDetailViewModel();
      fileDetail.Environment = target;

      var di = new DirectoryInfo(fileDetail.Environment.PhysicalPath);

      var f = di.GetFiles()
                .Where(x => x.Name == fileName)
                .Select<FileInfo, Models.File>(x => new Models.File() { Name = x.Name, Size = x.Length, CreatedDate = x.CreationTime, UpdatedDate = x.LastWriteTime })
                .FirstOrDefault();

      fileDetail.FileInfo = f;

      using (FileStream stream = new FileStream(System.IO.Path.Combine(fileDetail.Environment.PhysicalPath, f.Name), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      using (var sr = new System.IO.StreamReader(stream, System.Text.Encoding.GetEncoding(fileDetail.Environment.Encoding)))
      {
        fileDetail.FileContent = sr.ReadToEnd();
        return fileDetail;
      }

    }
  }
}
