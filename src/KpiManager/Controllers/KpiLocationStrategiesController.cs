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
    public class KpiLocationStrategiesController : Controller
    {
        private readonly MetricContext _context;

        public KpiLocationStrategiesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: KpiLocationStrategies
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.KpiLocationStrategy.Include(k => k.Kpi).Include(k => k.KpiStrategy).Include(k => k.Location);
            return View(await metricContext.ToListAsync());
        }

        // GET: KpiLocationStrategies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpiLocationStrategy = await _context.KpiLocationStrategy
                .Include(k => k.Kpi)
                .Include(k => k.KpiStrategy)
                .Include(k => k.Location)
                .SingleOrDefaultAsync(m => m.KpiId == id);
            if (kpiLocationStrategy == null)
            {
                return NotFound();
            }

            return View(kpiLocationStrategy);
        }

        // GET: KpiLocationStrategies/Create
        public IActionResult Create()
        {
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Description");
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description");
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId");
            return View();
        }

        // POST: KpiLocationStrategies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KpiId,LocationId,CreatedBy,CreatedDate,KpiStrategyId,ModifiedBy,ModifiedDate")] KpiLocationStrategy kpiLocationStrategy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kpiLocationStrategy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Description", kpiLocationStrategy.KpiId);
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description", kpiLocationStrategy.KpiStrategyId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", kpiLocationStrategy.LocationId);
            return View(kpiLocationStrategy);
        }

        // GET: KpiLocationStrategies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpiLocationStrategy = await _context.KpiLocationStrategy.SingleOrDefaultAsync(m => m.KpiId == id);
            if (kpiLocationStrategy == null)
            {
                return NotFound();
            }
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Description", kpiLocationStrategy.KpiId);
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description", kpiLocationStrategy.KpiStrategyId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", kpiLocationStrategy.LocationId);
            return View(kpiLocationStrategy);
        }

        // POST: KpiLocationStrategies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KpiId,LocationId,CreatedBy,CreatedDate,KpiStrategyId,ModifiedBy,ModifiedDate")] KpiLocationStrategy kpiLocationStrategy)
        {
            if (id != kpiLocationStrategy.KpiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kpiLocationStrategy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KpiLocationStrategyExists(kpiLocationStrategy.KpiId))
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
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Description", kpiLocationStrategy.KpiId);
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description", kpiLocationStrategy.KpiStrategyId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", kpiLocationStrategy.LocationId);
            return View(kpiLocationStrategy);
        }

        // GET: KpiLocationStrategies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpiLocationStrategy = await _context.KpiLocationStrategy
                .Include(k => k.Kpi)
                .Include(k => k.KpiStrategy)
                .Include(k => k.Location)
                .SingleOrDefaultAsync(m => m.KpiId == id);
            if (kpiLocationStrategy == null)
            {
                return NotFound();
            }

            return View(kpiLocationStrategy);
        }

        // POST: KpiLocationStrategies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kpiLocationStrategy = await _context.KpiLocationStrategy.SingleOrDefaultAsync(m => m.KpiId == id);
            _context.KpiLocationStrategy.Remove(kpiLocationStrategy);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KpiLocationStrategyExists(int id)
        {
            return _context.KpiLocationStrategy.Any(e => e.KpiId == id);
        }
    }
}
