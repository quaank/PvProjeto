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
    public class EquipasController : Controller
    {
        private readonly ProjetoPvContext _context;

        public EquipasController(ProjetoPvContext context)
        {
            _context = context;
        }

        // GET: Equipas
        public async Task<IActionResult> Index()
        {
            var projetoPvContext = _context.Equipas.Include(e => e.Treinador);
            return View(await projetoPvContext.ToListAsync());
        }

        // GET: Equipas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Equipas == null)
            {
                return NotFound();
            }

            var equipas = await _context.Equipas
                .Include(e => e.Treinador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipas == null)
            {
                return NotFound();
            }

            return View(equipas);
        }

        // GET: Equipas/Create
        public IActionResult Create()
        {
            ViewData["TreinadoresId"] = new SelectList(_context.Treinadores, "Id", "Nome");
            return View();
        }

        // POST: Equipas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,ModalidadesId,Modalidade,Categoria,TreinadoresId")] Equipas equipas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TreinadoresId"] = new SelectList(_context.Treinadores, "Id", "Nome", equipas.TreinadoresId);
            return View(equipas);
        }

        // GET: Equipas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Equipas == null)
            {
                return NotFound();
            }

            var equipas = await _context.Equipas.FindAsync(id);
            if (equipas == null)
            {
                return NotFound();
            }
            ViewData["TreinadoresId"] = new SelectList(_context.Treinadores, "Id", "Nome", equipas.TreinadoresId);
            return View(equipas);
        }

        // POST: Equipas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,ModalidadesId,Modalidade,Categoria,TreinadoresId")] Equipas equipas)
        {
            if (id != equipas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipasExists(equipas.Id))
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
            ViewData["TreinadoresId"] = new SelectList(_context.Treinadores, "Id", "Nome", equipas.TreinadoresId);
            return View(equipas);
        }

        // GET: Equipas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Equipas == null)
            {
                return NotFound();
            }

            var equipas = await _context.Equipas
                .Include(e => e.Treinador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipas == null)
            {
                return NotFound();
            }

            return View(equipas);
        }

        // POST: Equipas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Equipas == null)
            {
                return Problem("Entity set 'ProjetoPvContext.Equipas'  is null.");
            }
            var equipas = await _context.Equipas.FindAsync(id);
            if (equipas != null)
            {
                _context.Equipas.Remove(equipas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipasExists(int id)
        {
          return (_context.Equipas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
