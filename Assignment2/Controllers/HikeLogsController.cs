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
{   
    [Authorize]
    public class HikeLogsController : Controller
    {
        private readonly Assignment2Context _context;

        public HikeLogsController(Assignment2Context context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: HikeLogs
        public async Task<IActionResult> Index()
        {
            var assignment2Context = _context.HikeLog.Include(h => h.Mountain).Include(h => h.MountainNameNavigation);
            return View(await assignment2Context.ToListAsync());
        }
        [AllowAnonymous]
        // GET: HikeLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hikeLog = await _context.HikeLog
                .Include(h => h.Mountain)
                .Include(h => h.MountainNameNavigation)
                .FirstOrDefaultAsync(m => m.HikeId == id);
            if (hikeLog == null)
            {
                return NotFound();
            }

            return View(hikeLog);
        }

        // GET: HikeLogs/Create
        public IActionResult Create()
        {
            ViewData["MountainId"] = new SelectList(_context.Mountain, "MountainId", "MountainName");
            ViewData["MountainName"] = new SelectList(_context.Mountain, "MountainName", "MountainName");
            return View();
        }

        // POST: HikeLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HikeId,MountainName,MountainId,DateHiked,TimeToSummit")] HikeLog hikeLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hikeLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MountainId"] = new SelectList(_context.Mountain, "MountainId", "MountainName", hikeLog.MountainId);
            ViewData["MountainName"] = new SelectList(_context.Mountain, "MountainName", "MountainName", hikeLog.MountainName);
            return View(hikeLog);
        }

        // GET: HikeLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hikeLog = await _context.HikeLog.FindAsync(id);
            if (hikeLog == null)
            {
                return NotFound();
            }
            ViewData["MountainId"] = new SelectList(_context.Mountain, "MountainId", "MountainName", hikeLog.MountainId);
            ViewData["MountainName"] = new SelectList(_context.Mountain, "MountainName", "MountainName", hikeLog.MountainName);
            return View(hikeLog);
        }

        // POST: HikeLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HikeId,MountainName,MountainId,DateHiked,TimeToSummit")] HikeLog hikeLog)
        {
            if (id != hikeLog.HikeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hikeLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HikeLogExists(hikeLog.HikeId))
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
            ViewData["MountainId"] = new SelectList(_context.Mountain, "MountainId", "MountainName", hikeLog.MountainId);
            ViewData["MountainName"] = new SelectList(_context.Mountain, "MountainName", "MountainName", hikeLog.MountainName);
            return View(hikeLog);
        }

        // GET: HikeLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hikeLog = await _context.HikeLog
                .Include(h => h.Mountain)
                .Include(h => h.MountainNameNavigation)
                .FirstOrDefaultAsync(m => m.HikeId == id);
            if (hikeLog == null)
            {
                return NotFound();
            }

            return View(hikeLog);
        }

        // POST: HikeLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hikeLog = await _context.HikeLog.FindAsync(id);
            _context.HikeLog.Remove(hikeLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HikeLogExists(int id)
        {
            return _context.HikeLog.Any(e => e.HikeId == id);
        }
    }
}
