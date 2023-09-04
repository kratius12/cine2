using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cine3.Models;

namespace cine3.Controllers
{
    public class RankingsController : Controller
    {
        private readonly CineContext _context;

        public RankingsController(CineContext context)
        {
            _context = context;
        }

        // GET: Rankings
        public async Task<IActionResult> Index()
        {
            var cineContext = _context.Rankings.Include(r => r.IdPelNavigation);
            return View(await cineContext.ToListAsync());
        }

        // GET: Rankings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rankings == null)
            {
                return NotFound();
            }

            var ranking = await _context.Rankings
                .Include(r => r.IdPelNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ranking == null)
            {
                return NotFound();
            }

            return View(ranking);
        }

        // GET: Rankings/Create
        public IActionResult Create()
        {
            ViewData["IdPel"] = new SelectList(_context.Peliculas, "Id", "Id");
            return View();
        }

        // POST: Rankings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPel,Puntuacion")] Ranking ranking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ranking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPel"] = new SelectList(_context.Peliculas, "Id", "nombre", ranking.IdPel);
            return View(ranking);
        }

        // GET: Rankings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rankings == null)
            {
                return NotFound();
            }

            var ranking = await _context.Rankings.FindAsync(id);
            if (ranking == null)
            {
                return NotFound();
            }
            ViewData["IdPel"] = new SelectList(_context.Peliculas, "Id", "Id", ranking.IdPel);
            return View(ranking);
        }

        // POST: Rankings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPel,Puntuacion")] Ranking ranking)
        {
            if (id != ranking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ranking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingExists(ranking.Id))
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
            ViewData["IdPel"] = new SelectList(_context.Peliculas, "Id", "Id", ranking.IdPel);
            return View(ranking);
        }

        // GET: Rankings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rankings == null)
            {
                return NotFound();
            }

            var ranking = await _context.Rankings
                .Include(r => r.IdPelNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ranking == null)
            {
                return NotFound();
            }

            return View(ranking);
        }

        // POST: Rankings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rankings == null)
            {
                return Problem("Entity set 'CineContext.Rankings'  is null.");
            }
            var ranking = await _context.Rankings.FindAsync(id);
            if (ranking != null)
            {
                _context.Rankings.Remove(ranking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingExists(int id)
        {
          return (_context.Rankings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
