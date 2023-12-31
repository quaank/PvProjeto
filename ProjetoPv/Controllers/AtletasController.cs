﻿using System;
using System.Collections.Generic;
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
    
    public class AtletasController : Controller
    {
        private readonly ProjetoPvContext _context;
        private UserManager<ProjetoPvUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private bool isFirstTime = false;
        public AtletasController(ProjetoPvContext context, UserManager<ProjetoPvUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Atletas

        public async Task<IActionResult> Index(int id)
        {
            if(id == 0 || id == null) { 
            
                var user = await _userManager.GetUserAsync(User);
            ViewData["User"] = user.Id;
                if (await _userManager.IsInRoleAsync(user, "Atletas") && !user.HasClicked)
                {
                user.HasClicked = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Create");
                }
            else { 

            var projetoPvContext = _context.Atletas.Include(a => a.Equipa);
            return View(await projetoPvContext.ToListAsync());
            }
            }
            if(id != 0 || id != null)
            {
                var projetoPvContext = _context.Atletas.Where(a => a.Equipa.Id == id).Include(a => a.Equipa);
                return View(await projetoPvContext.ToListAsync());
            }
            return View();
        }



        // GET: Atletas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Atletas == null)
            {
                return NotFound();
            }

            var atletas = await _context.Atletas
                .Include(a => a.Equipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atletas == null)
            {
                return NotFound();
            }

            return View(atletas);
        }
       
        // GET: Atletas/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["User"] = user.Id;
            ViewData["Equipas"] = new SelectList(_context.Equipas, "Id", "Nome");
            return View();
        }

        // POST: Atletas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataNascimento,ModalidadesId,Modalidade,EquipasId,Posicao,AspNetUsersId")] Atletas atletas)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(atletas);
        
               
                await _context.SaveChangesAsync();
                    var Modalidade = _context.Equipas.Where(a => a.Id == atletas.EquipasId);
               
               // atletas.Modalidade = Modalidade;
                //_context.Update(atletas);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", atletas.EquipasId);
            return View(atletas);
        }
        
        // GET: Atletas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Atletas == null)
            {
                return NotFound();
            }

            var atletas = await _context.Atletas.FindAsync(id);
            if (atletas == null)
            {
                return NotFound();
            }
            var diferenca = DateTime.Now.Subtract(atletas.DataNascimento).TotalDays;

            var anos = diferenca / 365;

            
               // atletas.DataNascimento.CompareTo(DateTime.Now);
         if (anos > 19)
                {
                ViewData["EquipasId"] = new SelectList(_context.Equipas.Where(t => t.Categoria != Categorias.sub19), "Id", "Nome", atletas.EquipasId);
                return View(atletas);
            }

            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", atletas.EquipasId);
            return View(atletas);
        }

        // POST: Atletas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNascimento,ModalidadesId,Modalidade,EquipasId,Posicao,AspNetUsersId")] Atletas atletas)
        {
            if (id != atletas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atletas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtletasExists(atletas.Id))
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
            ViewData["EquipasId"] = new SelectList(_context.Equipas, "Id", "Nome", atletas.EquipasId);
            return View(atletas);
        }
        [Authorize(Roles = "Admin")]

        // GET: Atletas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Atletas == null)
            {
                return NotFound();
            }

            var atletas = await _context.Atletas
                .Include(a => a.Equipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atletas == null)
            {
                return NotFound();
            }

            return View(atletas);
        }

        // POST: Atletas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Atletas == null)
            {
                return Problem("Entity set 'ProjetoPvContext.Atletas'  is null.");
            }
            var atletas = await _context.Atletas.FindAsync(id);
            if (atletas != null)
            {
                _context.Atletas.Remove(atletas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtletasExists(int id)
        {
          return (_context.Atletas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
