using Microsoft.AspNetCore.Mvc;
using MyMailBox.Models;

namespace MyMailBox.Controllers
{
  public class MailBoxController : Controller
  {
    private MailBoxContext context;
    public MailBoxController(MailBoxContext context)
    { this.context = context; }
    public IActionResult Index(string reference)
    {
      MailBox? mailBox = context.MailBoxes
              .FirstOrDefault(mb => (mb.Reference??"").ToUpper() == reference.ToUpper());
      if ( mailBox == null)
        return NotFound("No such Mailbox");
      return View(mailBox);
    }
  }
}
