using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment2.Controllers
{   [Authorize]
    public class MountainsController : Controller
    {
        private readonly Assignment2Context _context;

        public MountainsController(Assignment2Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Mountains
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mountain.ToListAsync());
        }
        [AllowAnonymous]
        // GET: Mountains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mountain = await _context.Mountain
                .FirstOrDefaultAsync(m => m.MountainId == id);
            if (mountain == null)
            {
                return NotFound();
            }

            return View(mountain);
        }

        // GET: Mountains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mountains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MountainId,MountainName,Elevation,Difficulty")] Mountain mountain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mountain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mountain);
        }

        // GET: Mountains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mountain = await _context.Mountain.FindAsync(id);
            if (mountain == null)
            {
                return NotFound();
            }
            return View(mountain);
        }

        // POST: Mountains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MountainId,MountainName,Elevation,Difficulty")] Mountain mountain)
        {
            if (id != mountain.MountainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mountain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MountainExists(mountain.MountainId))
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
            return View(mountain);
        }

        // GET: Mountains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mountain = await _context.Mountain
                .FirstOrDefaultAsync(m => m.MountainId == id);
            if (mountain == null)
            {
                return NotFound();
            }

            return View(mountain);
        }

        // POST: Mountains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mountain = await _context.Mountain.FindAsync(id);
            _context.Mountain.Remove(mountain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MountainExists(int id)
        {
            return _context.Mountain.Any(e => e.MountainId == id);
        }
    }
}
