using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using Takabungi.Models;
using Takabungi.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Takabungi.Controllers
{

  [Authorize]
  public class LogController : Controller
  {
    private AppSettings AppSettings { get; }
    public LogController(IOptions<AppSettings> appSettings)
    {
      AppSettings = appSettings.Value;
    }
    // GET: /<controller>/
    public IActionResult Environments()
    {
      ViewBag.AppName = AppSettings.AppName;

      var viewModel = new EnvironmentsViewModel();
      viewModel.Environments = AppSettings.Environments;
      return View(viewModel);
    }

    public IActionResult LogFiles(int EnvID)
    {
      ViewBag.AppName = AppSettings.AppName;

      var reader = new LogReader();
      var target = AppSettings.Environments.Where(x => x.EnvID == EnvID).FirstOrDefault();
      return View(reader.GetFiles(target));
    }

    public IActionResult FileDetail(int EnvID , string filename)
    {
      ViewBag.AppName = AppSettings.AppName;

      var reader = new LogReader();
      var target = AppSettings.Environments.Where(x => x.EnvID == EnvID).FirstOrDefault();
      return View(reader.GetFile(target, filename));
    }
    
  }
}
