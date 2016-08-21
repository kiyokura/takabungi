using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using Takabungi.Models;
using Takabungi.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Takabungi.Controllers
{
  public class AuthController : Controller
  {

    private AppSettings AppSettings { get; }
    public AuthController(IOptions<AppSettings> appSettings)
    {
      AppSettings = appSettings.Value;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null)
    {
      ViewBag.AppName = AppSettings.AppName;
      return View();
    }

    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "/")
    {
      ViewBag.AppName = AppSettings.AppName;
      ViewBag.ReturnUrl = returnUrl;
      var result = await SingIn(model.ID, model.Password);
      if (result.Succeeded)
      {
        return LocalRedirect(returnUrl);
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Failed to Login.");
        return View(model);
      }
    }


    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.Authentication.SignOutAsync("MyCookieAuth");
      return LocalRedirect("/");
    }

    [AllowAnonymous]
    public IActionResult Forbidden()
    {
      ViewBag.AppName = AppSettings.AppName;
      return View();
    }


    private async Task<AuthResult> SingIn(string ID, string Password)
    {
      var result = new AuthResult();
      if (ID == AppSettings.Auth.ID && Password == AppSettings.Auth.Password)
      {
        var identity = new ClaimsIdentity("Password");
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, ID));
        var principal = new ClaimsPrincipal(identity);
        var authenticationProperties = new AuthenticationProperties { IsPersistent = true };
        await HttpContext.Authentication.SignInAsync("MyCookieAuth", principal, authenticationProperties);
        result.Succeeded = true;
      }
      else
      {
        result.Succeeded = false;
      }
      return result;
    }

  }
}
