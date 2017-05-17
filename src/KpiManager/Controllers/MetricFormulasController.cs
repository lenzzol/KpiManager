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
    public class MetricFormulasController : Controller
    {
        private readonly MetricContext _context;

        public MetricFormulasController(MetricContext context)
        {
            _context = context;    
        }

        // GET: MetricFormulas
        public async Task<IActionResult> Index()
        {
            return View(await _context.MetricFormula.ToListAsync());
        }

        // GET: MetricFormulas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricFormula = await _context.MetricFormula
                .SingleOrDefaultAsync(m => m.MetricFormulaId == id);
            if (metricFormula == null)
            {
                return NotFound();
            }

            return View(metricFormula);
        }

        // GET: MetricFormulas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetricFormulas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetricFormulaId,CreatedBy,CreatedDate,Description,ModifiedBy,ModifiedDate")] MetricFormula metricFormula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metricFormula);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(metricFormula);
        }

        // GET: MetricFormulas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricFormula = await _context.MetricFormula.SingleOrDefaultAsync(m => m.MetricFormulaId == id);
            if (metricFormula == null)
            {
                return NotFound();
            }
            return View(metricFormula);
        }

        // POST: MetricFormulas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetricFormulaId,CreatedBy,CreatedDate,Description,ModifiedBy,ModifiedDate")] MetricFormula metricFormula)
        {
            if (id != metricFormula.MetricFormulaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metricFormula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetricFormulaExists(metricFormula.MetricFormulaId))
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
            return View(metricFormula);
        }

        // GET: MetricFormulas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metricFormula = await _context.MetricFormula
                .SingleOrDefaultAsync(m => m.MetricFormulaId == id);
            if (metricFormula == null)
            {
                return NotFound();
            }

            return View(metricFormula);
        }

        // POST: MetricFormulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metricFormula = await _context.MetricFormula.SingleOrDefaultAsync(m => m.MetricFormulaId == id);
            _context.MetricFormula.Remove(metricFormula);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MetricFormulaExists(int id)
        {
            return _context.MetricFormula.Any(e => e.MetricFormulaId == id);
        }
    }
}
