
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasGrupo4.Models;

public class GenerosController : Controller
{
    private readonly PeliculasGrupo4Context _context;

    public GenerosController(PeliculasGrupo4Context context)
    {
        _context = context;
    }

    // GET: GENEROS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Generos.ToListAsync());
    }

    // GET: GENEROS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genero = await _context.Generos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genero == null)
        {
            return NotFound();
        }

        return View(genero);
    }

    // GET: GENEROS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: GENEROS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Peliculas")] Genero genero)
    {
        if (ModelState.IsValid)
        {
            _context.Add(genero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(genero);
    }

    // GET: GENEROS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genero = await _context.Generos.FindAsync(id);
        if (genero == null)
        {
            return NotFound();
        }
        return View(genero);
    }

    // POST: GENEROS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Descripcion,Peliculas")] Genero genero)
    {
        if (id != genero.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(genero);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(genero.Id))
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
        return View(genero);
    }

    // GET: GENEROS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var genero = await _context.Generos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (genero == null)
        {
            return NotFound();
        }

        return View(genero);
    }

    // POST: GENEROS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var genero = await _context.Generos.FindAsync(id);
        if (genero != null)
        {
            _context.Generos.Remove(genero);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GeneroExists(int? id)
    {
        return _context.Generos.Any(e => e.Id == id);
    }
}
