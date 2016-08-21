namespace Takabungi.Models
{
  public class AppSettings
  {
    public string AppName { get; set; }
    public Auth Auth { get; set; }
    public Environment[] Environments { get; set; }
  }

  public class Auth
  {
    public string ID { get; set; }
    public string Password { get; set; }
  }
}
