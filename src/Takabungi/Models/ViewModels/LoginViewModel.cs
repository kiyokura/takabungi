using System.ComponentModel.DataAnnotations;

namespace Takabungi.Models.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name = "ID")]
    public string ID { get; set; }

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
