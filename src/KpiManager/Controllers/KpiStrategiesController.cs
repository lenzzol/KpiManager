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
    public class KpiStrategiesController : Controller
    {
        private readonly MetricContext _context;

        public KpiStrategiesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: KpiStrategies
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.KpiStrategy.Include(k => k.Kpi).Include(k => k.Pointofsale);
            return View(await metricContext.ToListAsync());
        }

        // GET: KpiStrategies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpiStrategy = await _context.KpiStrategy
                .Include(k => k.Kpi)
                .Include(k => k.Pointofsale)
                .SingleOrDefaultAsync(m => m.KpiStrategyId == id);
            if (kpiStrategy == null)
            {
                return NotFound();
            }

            return View(kpiStrategy);
        }

        // GET: KpiStrategies/Create
        public IActionResult Create()
        {
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Name");
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId");
            return View();
        }

        // POST: KpiStrategies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KpiStrategyId,CreatedBy,CreatedDate,Description,IsSystem,KpiId,ModifiedBy,ModifiedDate,PointofsaleId")] KpiStrategy kpiStrategy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kpiStrategy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Name", kpiStrategy.KpiId);
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId", kpiStrategy.PointofsaleId);
            return View(kpiStrategy);
        }

        // GET: KpiStrategies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpiStrategy = await _context.KpiStrategy.SingleOrDefaultAsync(m => m.KpiStrategyId == id);
            if (kpiStrategy == null)
            {
                return NotFound();
            }
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Name", kpiStrategy.KpiId);
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId", kpiStrategy.PointofsaleId);
            return View(kpiStrategy);
        }

        // POST: KpiStrategies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KpiStrategyId,CreatedBy,CreatedDate,Description,IsSystem,KpiId,ModifiedBy,ModifiedDate,PointofsaleId")] KpiStrategy kpiStrategy)
        {
            if (id != kpiStrategy.KpiStrategyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kpiStrategy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KpiStrategyExists(kpiStrategy.KpiStrategyId))
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
            ViewData["KpiId"] = new SelectList(_context.Kpi, "KpiId", "Name", kpiStrategy.KpiId);
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId", kpiStrategy.PointofsaleId);
            return View(kpiStrategy);
        }

        // GET: KpiStrategies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kpiStrategy = await _context.KpiStrategy
                .Include(k => k.Kpi)
                .Include(k => k.Pointofsale)
                .SingleOrDefaultAsync(m => m.KpiStrategyId == id);
            if (kpiStrategy == null)
            {
                return NotFound();
            }

            return View(kpiStrategy);
        }

        // POST: KpiStrategies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kpiStrategy = await _context.KpiStrategy.SingleOrDefaultAsync(m => m.KpiStrategyId == id);
            _context.KpiStrategy.Remove(kpiStrategy);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KpiStrategyExists(int id)
        {
            return _context.KpiStrategy.Any(e => e.KpiStrategyId == id);
        }
    }
}
