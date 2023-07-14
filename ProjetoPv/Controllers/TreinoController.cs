using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<ProjetoPvUser> _userManager;

        public TreinoController(ProjetoPvContext context, UserManager<ProjetoPvUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Treino
        public async Task<IActionResult> Index(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var treinador = _context.Treinadores.ToList();
            foreach (var item in treinador)
            {
                if (user.Id == item.AspNetUsersId)
                {
                    var treinadorId = item.Id;
                    foreach (var item1 in _context.Equipas.ToList())
                    {
                        if (item1.TreinadoresId == treinadorId)
                       {
                            var Equipa = item1;
                            if(id == 0 || id == null) 
                            { 
                            var projetoPvContext1 = _context.Treino.Where(t=> t.Data > DateTime.Now).Where(t => t.Equipa == Equipa);
                            return View(await projetoPvContext1.ToListAsync());
                            }
                            var projetoPvContext2 = _context.Treino.Where(t => t.Data < DateTime.Now).Where(t => t.Equipa == Equipa).Include(t => t.Equipa).Include(t => t.Equipa.Treinador);
                return View(await projetoPvContext2.ToListAsync());

                        }
                    }
                }

            }

            var atleta = _context.Atletas.ToList();
            foreach (var item in atleta)
            {
                if (user.Id == item.AspNetUsersId)
                {
                    var atletaId = item;
                    foreach (var item1 in _context.Equipas.ToList())
                    {
                        if (item1.Id == atletaId.EquipasId)
                        {
                            var Equipa = item1;
                            if (id == 0 || id != null)
                            {
                                var projetoPvContext3 = _context.Treino.Where(t => t.Data > DateTime.Now).Where(t => t.Equipa == Equipa);
                                return View(await projetoPvContext3.ToListAsync());
                            }
                            var projetoPvContext4 = _context.Treino.Where(t => t.Data < DateTime.Now).Where(t => t.Equipa == Equipa).Include(t => t.Equipa).Include(t => t.Equipa.Treinador);
                            return View(await projetoPvContext4.ToListAsync());

                        }
                    }
                }

            }

            if (id == 0 || id == null)
            {
                var projetoPvContext5 = _context.Treino.Where(t => t.Data > DateTime.Now).Include(t => t.Equipa).Include(t => t.Equipa.Treinador);
                return View(await projetoPvContext5.ToListAsync());
            }
            var projetoPvContext6 = _context.Treino.Where(t => t.Data < DateTime.Now).Where(t => t.Equipa.Id == id).Include(t => t.Equipa).Include(t => t.Equipa.Treinador);
            return View(await projetoPvContext6.ToListAsync());
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
        [Authorize(Roles = "Admin, Treinadores")]
        // GET: Treino/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var treinador = _context.Treinadores.ToList();
            foreach (var item in treinador)
            {
                if (user.Id == item.AspNetUsersId)
                {
                    var treinadorId = item.Id;
                    foreach (var item1 in _context.Equipas.ToList())
                    {
                        if (item1.TreinadoresId == treinadorId)
                        {
                            var Equipa = item1;
                            ViewData["Equipa"] = Equipa;
                            return View();
                        }
                    }
                }
                
            }

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
        [Authorize(Roles = "Admin, Treinadores")]
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

        [Authorize(Roles = "Admin, Treinadores")]
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
