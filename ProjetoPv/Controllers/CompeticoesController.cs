using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoPv.Areas.Identity.Data;
using ProjetoPv.Models;

namespace ProjetoPv.Controllers
{
    public class CompeticoesController : Controller
    {
        private readonly ProjetoPvContext _context;

        public CompeticoesController(ProjetoPvContext context)
        {
            _context = context;
        }

        // GET: Competicoes
        public async Task<IActionResult> Index()
        {
            var projetoPvContext = _context.Competicoes.Include(c => c.Equipa);
            return View(await projetoPvContext.ToListAsync());
        }

        // GET: Competicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Competicoes == null)
            {
                return NotFound();
            }

            var competicoes = await _context.Competicoes
                .Include(c => c.Equipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competicoes == null)
            {
                return NotFound();
            }

            return View(competicoes);
        }
        [Authorize(Roles = "Admin")]
        // GET: Competicoes/Create
        public IActionResult Create()
        {
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome");
            return View();
        }

        // POST: Competicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,EquipasId,EquipaOponente,Data,Localidade,Resultados")] Competicoes competicoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competicoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", competicoes.EquipasId);
            return View(competicoes);
        }

        // GET: Competicoes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Competicoes == null)
            {
                return NotFound();
            }

            var competicoes = await _context.Competicoes.FindAsync(id);
            if (competicoes == null)
            {
                return NotFound();
            }
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", competicoes.EquipasId);
            return View(competicoes);
        }

        // POST: Competicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,EquipasId,EquipaOponente,Data,Localidade,Resultados")] Competicoes competicoes)
        {
            if (id != competicoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competicoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompeticoesExists(competicoes.Id))
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
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", competicoes.EquipasId);
            return View(competicoes);
        }
        [Authorize(Roles = "Admin")]
        // GET: Competicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Competicoes == null)
            {
                return NotFound();
            }

            var competicoes = await _context.Competicoes
                .Include(c => c.Equipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competicoes == null)
            {
                return NotFound();
            }

            return View(competicoes);
        }

        // POST: Competicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Competicoes == null)
            {
                return Problem("Entity set 'ProjetoPvContext.Competicoes'  is null.");
            }
            var competicoes = await _context.Competicoes.FindAsync(id);
            if (competicoes != null)
            {
                _context.Competicoes.Remove(competicoes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompeticoesExists(int id)
        {
          return (_context.Competicoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
