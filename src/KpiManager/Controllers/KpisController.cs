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
    public class KpisController : Controller
    {
        private readonly MetricContext _context;

        public KpisController(MetricContext context)
        {
            _context = context;    
        }

        // GET: Kpis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kpi.ToListAsync());
        }

        // GET: Kpis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpi = await _context.Kpi
                .SingleOrDefaultAsync(m => m.KpiId == id);
            if (kpi == null)
            {
                return NotFound();
            }

            return View(kpi);
        }

        // GET: Kpis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kpis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KpiId,CreatedBy,CreatedDate,Description,ModifiedBy,ModifiedDate,Name")] Kpi kpi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kpi);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kpi);
        }

        // GET: Kpis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpi = await _context.Kpi.SingleOrDefaultAsync(m => m.KpiId == id);
            if (kpi == null)
            {
                return NotFound();
            }
            return View(kpi);
        }

        // POST: Kpis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KpiId,CreatedBy,CreatedDate,Description,ModifiedBy,ModifiedDate,Name")] Kpi kpi)
        {
            if (id != kpi.KpiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kpi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KpiExists(kpi.KpiId))
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
            return View(kpi);
        }

        // GET: Kpis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpi = await _context.Kpi
                .SingleOrDefaultAsync(m => m.KpiId == id);
            if (kpi == null)
            {
                return NotFound();
            }

            return View(kpi);
        }

        // POST: Kpis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kpi = await _context.Kpi.SingleOrDefaultAsync(m => m.KpiId == id);
            _context.Kpi.Remove(kpi);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KpiExists(int id)
        {
            return _context.Kpi.Any(e => e.KpiId == id);
        }
    }
}
