
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonPortfolio_M1_2526.Entities;
using MonPortfolio_M1_2526.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class ModerationController : Controller
{
    private readonly ApplicationDbContext _context;

    public ModerationController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: COMMENTENTITYS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Comments.ToListAsync());
    }

    // GET: COMMENTENTITYS/Details/5
    public async Task<IActionResult> Details(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var commententity = await _context.Comments
            .FirstOrDefaultAsync(m => m.Id == id);
        if (commententity == null)
        {
            return NotFound();
        }

        return View(commententity);
    }

    // GET: COMMENTENTITYS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: COMMENTENTITYS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Text,Author,Article")] CommentEntity commententity)
    {
        if (ModelState.IsValid)
        {
            _context.Add(commententity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(commententity);
    }

    // GET: COMMENTENTITYS/Edit/5
    public async Task<IActionResult> Edit(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var commententity = await _context.Comments.FindAsync(id);
        if (commententity == null)
        {
            return NotFound();
        }
        return View(commententity);
    }

    // POST: COMMENTENTITYS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(System.Guid? id, [Bind("Id,Text,Author,Article")] CommentEntity commententity)
    {
        if (id != commententity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(commententity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentEntityExists(commententity.Id))
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
        return View(commententity);
    }

    // GET: COMMENTENTITYS/Delete/5
    public async Task<IActionResult> Delete(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var commententity = await _context.Comments
            .FirstOrDefaultAsync(m => m.Id == id);
        if (commententity == null)
        {
            return NotFound();
        }

        return View(commententity);
    }

    // POST: COMMENTENTITYS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? id)
    {
        var commententity = await _context.Comments.FindAsync(id);
        if (commententity != null)
        {
            _context.Comments.Remove(commententity);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CommentEntityExists(System.Guid? id)
    {
        return _context.Comments.Any(e => e.Id == id);
    }
}
