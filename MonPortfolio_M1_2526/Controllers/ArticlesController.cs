
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonPortfolio_M1_2526.Entities;
using MonPortfolio_M1_2526.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

[Authorize]
public class ArticlesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ArticlesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Articles
    [AllowAnonymous]
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Articles.ToListAsync());
    }

    // GET: Articles/Details/5
    [AllowAnonymous]
    public async Task<IActionResult> Details(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var articleentity = await _context.Articles.Include(a => a.Comments)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (articleentity == null)
        {
            return NotFound();
        }

        return View(articleentity);
    }

    // POST: Articles/PostComment
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostComment(System.Guid id, string text)
    {
        var articleentity = await _context.Articles.FindAsync(id);
        if (articleentity == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            return RedirectToAction(nameof(Details), new { id });
        }

        var commententity = new CommentEntity
        {
            Text = text,
            Article = articleentity,
            Author = await _userManager.GetUserAsync(User)
        };

        _context.Comments.Add(commententity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Details), new { id });
    }

    // GET: Articles/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Articles/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Description,Content,Author")] ArticleEntity articleentity)
    {
        if (ModelState.IsValid)
        {
            _context.Add(articleentity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(articleentity);
    }

    // GET: Articles/Edit/5
    public async Task<IActionResult> Edit(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var articleentity = await _context.Articles.FindAsync(id);
        if (articleentity == null)
        {
            return NotFound();
        }
        return View(articleentity);
    }

    // POST: Articles/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(System.Guid? id, [Bind("Id,Title,Description,Content,Author")] ArticleEntity articleentity)
    {
        if (id != articleentity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(articleentity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleEntityExists(articleentity.Id))
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
        return View(articleentity);
    }

    // GET: Articles/Delete/5
    public async Task<IActionResult> Delete(System.Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var articleentity = await _context.Articles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (articleentity == null)
        {
            return NotFound();
        }

        return View(articleentity);
    }

    // POST: Articles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(System.Guid? id)
    {
        var articleentity = await _context.Articles.FindAsync(id);
        if (articleentity != null)
        {
            _context.Articles.Remove(articleentity);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ArticleEntityExists(System.Guid? id)
    {
        return _context.Articles.Any(e => e.Id == id);
    }
}
