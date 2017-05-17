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
    public class MetricFormulaSchemesController : Controller
    {
        private readonly MetricContext _context;

        public MetricFormulaSchemesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: MetricFormulaSchemes
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.MetricFormulaScheme.Include(m => m.Metric).Include(m => m.MetricFormula);
            return View(await metricContext.ToListAsync());
        }

        // GET: MetricFormulaSchemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricFormulaScheme = await _context.MetricFormulaScheme
                .Include(m => m.Metric)
                .Include(m => m.MetricFormula)
                .SingleOrDefaultAsync(m => m.MetricFormulaSchemeId == id);
            if (metricFormulaScheme == null)
            {
                return NotFound();
            }

            return View(metricFormulaScheme);
        }

        // GET: MetricFormulaSchemes/Create
        public IActionResult Create()
        {
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1");
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description");
            return View();
        }

        // POST: MetricFormulaSchemes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetricFormulaSchemeId,CreatedBy,CreatedDate,MetricId,MetricFormulaId,FormulaOrder,ModifiedBy,ModifiedDate")] MetricFormulaScheme metricFormulaScheme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metricFormulaScheme);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1", metricFormulaScheme.MetricId);
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description", metricFormulaScheme.MetricFormulaId);
            return View(metricFormulaScheme);
        }

        // GET: MetricFormulaSchemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricFormulaScheme = await _context.MetricFormulaScheme.SingleOrDefaultAsync(m => m.MetricFormulaSchemeId == id);
            if (metricFormulaScheme == null)
            {
                return NotFound();
            }
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1", metricFormulaScheme.MetricId);
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description", metricFormulaScheme.MetricFormulaId);
            return View(metricFormulaScheme);
        }

        // POST: MetricFormulaSchemes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetricFormulaSchemeId,CreatedBy,CreatedDate,MetricId,MetricFormulaId,FormulaOrder,ModifiedBy,ModifiedDate")] MetricFormulaScheme metricFormulaScheme)
        {
            if (id != metricFormulaScheme.MetricFormulaSchemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metricFormulaScheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetricFormulaSchemeExists(metricFormulaScheme.MetricFormulaSchemeId))
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
            ViewData["MetricId"] = new SelectList(_context.Metric, "MetricId", "Metric1", metricFormulaScheme.MetricId);
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description", metricFormulaScheme.MetricFormulaId);
            return View(metricFormulaScheme);
        }

        // GET: MetricFormulaSchemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricFormulaScheme = await _context.MetricFormulaScheme
                .Include(m => m.Metric)
                .Include(m => m.MetricFormula)
                .SingleOrDefaultAsync(m => m.MetricFormulaSchemeId == id);
            if (metricFormulaScheme == null)
            {
                return NotFound();
            }

            return View(metricFormulaScheme);
        }

        // POST: MetricFormulaSchemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metricFormulaScheme = await _context.MetricFormulaScheme.SingleOrDefaultAsync(m => m.MetricFormulaSchemeId == id);
            _context.MetricFormulaScheme.Remove(metricFormulaScheme);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MetricFormulaSchemeExists(int id)
        {
            return _context.MetricFormulaScheme.Any(e => e.MetricFormulaSchemeId == id);
        }
    }
}
