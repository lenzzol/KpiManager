using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KpiManager;

namespace KpiManager.Controllers
{
    public class PointofsalesController : Controller
    {
        private readonly MetricContext _context;

        public PointofsalesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: Pointofsales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pointofsale.ToListAsync());
        }

        // GET: Pointofsales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointofsale = await _context.Pointofsale
                .SingleOrDefaultAsync(m => m.PointofsaleId == id);
            if (pointofsale == null)
            {
                return NotFound();
            }

            return View(pointofsale);
        }

        // GET: Pointofsales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pointofsales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PointofsaleId")] Pointofsale pointofsale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointofsale);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pointofsale);
        }

        // GET: Pointofsales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointofsale = await _context.Pointofsale.SingleOrDefaultAsync(m => m.PointofsaleId == id);
            if (pointofsale == null)
            {
                return NotFound();
            }
            return View(pointofsale);
        }

        // POST: Pointofsales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PointofsaleId")] Pointofsale pointofsale)
        {
            if (id != pointofsale.PointofsaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointofsale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointofsaleExists(pointofsale.PointofsaleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(pointofsale);
        }

        // GET: Pointofsales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointofsale = await _context.Pointofsale
                .SingleOrDefaultAsync(m => m.PointofsaleId == id);
            if (pointofsale == null)
            {
                return NotFound();
            }

            return View(pointofsale);
        }

        // POST: Pointofsales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pointofsale = await _context.Pointofsale.SingleOrDefaultAsync(m => m.PointofsaleId == id);
            _context.Pointofsale.Remove(pointofsale);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PointofsaleExists(int id)
        {
            return _context.Pointofsale.Any(e => e.PointofsaleId == id);
        }
    }
}
