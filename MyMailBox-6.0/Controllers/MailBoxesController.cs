using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMailBox.Models;

namespace MyMailBox.Controllers
{
  public class MailBoxesController : Controller
  {
    private readonly MailBoxContext _context;




    public MailBoxesController(MailBoxContext context)
    {
      _context = context;
    }
    public IActionResult DetailsByReference(string reference)
    {
      MailBox? mailBox = _context.MailBoxes
              .FirstOrDefault(mb => (mb.Reference ?? "").ToUpper() == reference.ToUpper());
      if (mailBox == null)
        return NotFound("No such Mailbox");
      return View(viewName: nameof(Index), model: mailBox);
    }

    // GET: MailBoxes
    public async Task<IActionResult> Index()
    {
      return View(await _context.MailBoxes.ToListAsync());
    }

    // GET: MailBoxes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var mailBox = await _context.MailBoxes
          .FirstOrDefaultAsync(m => m.Id == id);
      if (mailBox == null)
      {
        return NotFound();
      }

      return View(mailBox);
    }

    // GET: MailBoxes/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: MailBoxes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Reference,Name,Color,Height,Width,Depth,ImagePath")] MailBox mailBox)
    {
      if (ModelState.IsValid)
      {
        _context.Add(mailBox);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(mailBox);
    }

    // GET: MailBoxes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var mailBox = await _context.MailBoxes.FindAsync(id);
      if (mailBox == null)
      {
        return NotFound();
      }
      return View(mailBox);
    }

    // POST: MailBoxes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Reference,Name,Color,Height,Width,Depth,ImagePath")] MailBox mailBox)
    {
      if (id != mailBox.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(mailBox);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!MailBoxExists(mailBox.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(mailBox);
    }

    // GET: MailBoxes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var mailBox = await _context.MailBoxes
          .FirstOrDefaultAsync(m => m.Id == id);
      if (mailBox == null)
      {
        return NotFound();
      }

      return View(mailBox);
    }

    // POST: MailBoxes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var mailBox = await _context.MailBoxes.FindAsync(id);
      if ( mailBox==null)
        return NotFound("No such mailbox to delete");
      _context.MailBoxes.Remove(mailBox);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool MailBoxExists(int id)
    {
      return _context.MailBoxes.Any(e => e.Id == id);
    }
  }
}
