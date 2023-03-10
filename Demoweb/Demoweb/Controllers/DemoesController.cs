using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demoweb.Data;
using Demoweb.Models;

namespace Demoweb.Controllers
{
    public class DemoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Demoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Demos.ToListAsync());
        }

        // GET: Demoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Demos == null)
            {
                return NotFound();
            }

            var demo = await _context.Demos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demo == null)
            {
                return NotFound();
            }

            return View(demo);
        }

        // GET: Demoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Demoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Rank,Performance")] Demo demo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(demo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(demo);
        }

        // GET: Demoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Demos == null)
            {
                return NotFound();
            }

            var demo = await _context.Demos.FindAsync(id);
            if (demo == null)
            {
                return NotFound();
            }
            return View(demo);
        }

        // POST: Demoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Rank,Performance")] Demo demo)
        {
            if (id != demo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemoExists(demo.Id))
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
            return View(demo);
        }

        // GET: Demoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Demos == null)
            {
                return NotFound();
            }

            var demo = await _context.Demos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (demo == null)
            {
                return NotFound();
            }

            return View(demo);
        }

        // POST: Demoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Demos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Demos'  is null.");
            }
            var demo = await _context.Demos.FindAsync(id);
            if (demo != null)
            {
                _context.Demos.Remove(demo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DemoExists(int id)
        {
          return _context.Demos.Any(e => e.Id == id);
        }
    }
}
