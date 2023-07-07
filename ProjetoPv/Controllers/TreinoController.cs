using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPv.Areas.Identity.Data;
using ProjetoPv.Models;

namespace ProjetoPv.Controllers
{
    public class TreinoController : Controller
    {
        private readonly ProjetoPvContext _context;

        public TreinoController(ProjetoPvContext context)
        {
            _context = context;
        }

        // GET: Treino
        public async Task<IActionResult> Index()
        {
            var projetoPvContext = _context.Treino.Include(t => t.Equipa);
            return View(await projetoPvContext.ToListAsync());
        }

        // GET: Treino/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Treino == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino
                .Include(t => t.Equipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // GET: Treino/Create
        public IActionResult Create()
        {
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome");
            return View();
        }

        // POST: Treino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Localizacao,EquipasId")] Treino treino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", treino.EquipasId);
            return View(treino);
        }

        // GET: Treino/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Treino == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", treino.EquipasId);
            return View(treino);
        }

        // POST: Treino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Localizacao,EquipasId")] Treino treino)
        {
            if (id != treino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreinoExists(treino.Id))
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
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", treino.EquipasId);
            return View(treino);
        }

        // GET: Treino/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Treino == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino
                .Include(t => t.Equipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // POST: Treino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Treino == null)
            {
                return Problem("Entity set 'ProjetoPvContext.Treino'  is null.");
            }
            var treino = await _context.Treino.FindAsync(id);
            if (treino != null)
            {
                _context.Treino.Remove(treino);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinoExists(int id)
        {
          return (_context.Treino?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
