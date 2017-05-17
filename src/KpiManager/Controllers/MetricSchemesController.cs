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
    public class MetricSchemesController : Controller
    {
        private readonly MetricContext _context;

        public MetricSchemesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: MetricSchemes
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.MetricScheme.Include(m => m.KpiStrategy).Include(m => m.Metric).Include(m => m.Operator);
            return View(await metricContext.ToListAsync());
        }

        // GET: MetricSchemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricScheme = await _context.MetricScheme
                .Include(m => m.KpiStrategy)
                .Include(m => m.Metric)
                .Include(m => m.Operator)
                .SingleOrDefaultAsync(m => m.MetricSchemeId == id);
            if (metricScheme == null)
            {
                return NotFound();
            }

            return View(metricScheme);
        }

        // GET: MetricSchemes/Create
        public IActionResult Create()
        {
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description");
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1");
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1");
            return View();
        }

        // POST: MetricSchemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetricSchemeId,CreatedBy,CreatedDate,KpiStrategyId,MetricId,ModifiedBy,ModifiedDate,OperationOrder,OperatorId")] MetricScheme metricScheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metricScheme);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description", metricScheme.KpiStrategyId);
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1", metricScheme.MetricId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1", metricScheme.OperatorId);
            return View(metricScheme);
        }

        // GET: MetricSchemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricScheme = await _context.MetricScheme.SingleOrDefaultAsync(m => m.MetricSchemeId == id);
            if (metricScheme == null)
            {
                return NotFound();
            }
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description", metricScheme.KpiStrategyId);
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1", metricScheme.MetricId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1", metricScheme.OperatorId);
            return View(metricScheme);
        }

        // POST: MetricSchemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetricSchemeId,CreatedBy,CreatedDate,KpiStrategyId,MetricId,ModifiedBy,ModifiedDate,OperationOrder,OperatorId")] MetricScheme metricScheme)
        {
            if (id != metricScheme.MetricSchemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metricScheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetricSchemeExists(metricScheme.MetricSchemeId))
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
            ViewData["KpiStrategyId"] = new SelectList(_context.KpiStrategy, "KpiStrategyId", "Description", metricScheme.KpiStrategyId);
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1", metricScheme.MetricId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1", metricScheme.OperatorId);
            return View(metricScheme);
        }

        // GET: MetricSchemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricScheme = await _context.MetricScheme
                .Include(m => m.KpiStrategy)
                .Include(m => m.Metric)
                .Include(m => m.Operator)
                .SingleOrDefaultAsync(m => m.MetricSchemeId == id);
            if (metricScheme == null)
            {
                return NotFound();
            }

            return View(metricScheme);
        }

        // POST: MetricSchemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metricScheme = await _context.MetricScheme.SingleOrDefaultAsync(m => m.MetricSchemeId == id);
            _context.MetricScheme.Remove(metricScheme);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MetricSchemeExists(int id)
        {
            return _context.MetricScheme.Any(e => e.MetricSchemeId == id);
        }
    }
}
