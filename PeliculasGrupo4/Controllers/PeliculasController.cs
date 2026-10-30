using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectList
using Microsoft.EntityFrameworkCore;
using PeliculasGrupo4.Models;


namespace PeliculasGrupo4.Controllers // Asegúrate de que coincida con tu namespace si es distinto
{
    public class PeliculasController : Controller
    {
        private readonly PeliculasGrupo4Context _context;

        public PeliculasController(PeliculasGrupo4Context context)
        {
            _context = context;
        }

        // GET: PELICULAS
        public async Task<IActionResult> Index()
        {
            // Se usa Include para traer la información del Género asociado
            var peliculas = _context.Pelicula.Include(p => p.Genero);
            return View(await peliculas.ToListAsync());
        }

        // GET: PELICULAS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .Include(p => p.Genero) // Se usa Include para el Details
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: PELICULAS/Create
        public IActionResult Create()
        {
            // COMPONENTE AUTÓNOMO
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre");
            return View();
        }

        // POST: PELICULAS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Director,DuracionMinutos,Sinopsis,GeneroId")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // COMPONENTE AUTÓNOMO: Se recarga la lista en caso de error de validación
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: PELICULAS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            // COMPONENTE AUTÓNOMO: Se carga el SelectList marcando el Género actual de la película
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // POST: PELICULAS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Titulo,Director,DuracionMinutos,Sinopsis,GeneroId")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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

            // COMPONENTE AUTÓNOMO: Se recarga la lista en caso de error de validación
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: PELICULAS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .Include(p => p.Genero) // Se usa Include para ver el género al eliminar
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: PELICULAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula != null)
            {
                _context.Pelicula.Remove(pelicula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int? id)
        {
            return _context.Pelicula.Any(e => e.Id == id);
        }
    }
}