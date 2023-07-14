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
    public class TreinadoresController : Controller
    {
        private readonly ProjetoPvContext _context;
        private UserManager<ProjetoPvUser> _userManager;

        public TreinadoresController(ProjetoPvContext context, UserManager<ProjetoPvUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

        // GET: Treinadores
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
           
            if (await _userManager.IsInRoleAsync(user, "Treinadores") && !user.HasClicked)
            {
                ViewData["User"] = user.Id;
                user.HasClicked = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Create");
            }
            else
            {
                ViewData["User"] = user.Id;
                return View(await _context.Treinadores.ToListAsync());
            }
        }

        // GET: Treinadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Treinadores == null)
            {
                return NotFound();
            }

            var treinadores = await _context.Treinadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treinadores == null)
            {
                return NotFound();
            }

            return View(treinadores);
        }

        // GET: Treinadores/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["User"] = user.Id;
            return View();
        }

        // POST: Treinadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Contacto,Qualificacoes,EquipasId,AspNetUsersId")] Treinadores treinadores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treinadores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treinadores);
        }
        [Authorize(Roles = "Admin")]
        // GET: Treinadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Treinadores == null)
            {
                return NotFound();
            }

            var treinadores = await _context.Treinadores.FindAsync(id);
            if (treinadores == null)
            {
                return NotFound();
            }
            return View(treinadores);
        }

        // POST: Treinadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Contacto,Qualificacoes,EquipasId")] Treinadores treinadores)
        {
            if (id != treinadores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treinadores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreinadoresExists(treinadores.Id))
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
            return View(treinadores);
        }
        [Authorize(Roles = "Admin")]
        // GET: Treinadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Treinadores == null)
            {
                return NotFound();
            }

            var treinadores = await _context.Treinadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treinadores == null)
            {
                return NotFound();
            }

            return View(treinadores);
        }

        // POST: Treinadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Treinadores == null)
            {
                return Problem("Entity set 'ProjetoPvContext.Treinadores'  is null.");
            }
            var treinadores = await _context.Treinadores.FindAsync(id);
            if (treinadores != null)
            {
                _context.Treinadores.Remove(treinadores);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinadoresExists(int id)
        {
          return (_context.Treinadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
