using Microsoft.AspNetCore.Mvc;
using MyMailBox.Models;

namespace MyMailBox.Controllers
{
  public class MailBoxController : Controller
  {
    public IActionResult Index(string reference)
    {
      var mailBox = new MailBox
      {
        Reference = "X102",
        Color = "Red",
        Depth = 200,
        Width = 300,
        Height = 250,
        ImagePath= "/images/mailboxes/mailbox1.jpg"
      };
      return View(mailBox);
    }
  }
}
