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
    public class MetricsController : Controller
    {
        private readonly MetricContext _context;

        public MetricsController(MetricContext context)
        {
            _context = context;    
        }

        // GET: Metrics
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.Metric.Include(m => m.ResultDataType);
            return View(await metricContext.ToListAsync());
        }

        // GET: Metrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metric = await _context.Metric
                .Include(m => m.ResultDataType)
                .SingleOrDefaultAsync(m => m.MetricId == id);
            if (metric == null)
            {
                return NotFound();
            }

            return View(metric);
        }

        // GET: Metrics/Create
        public IActionResult Create()
        {
            ViewData["ResultDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1");
            return View();
        }

        // POST: Metrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MetricId,CreatedBy,CreatedDate,DefaultResultValue,Metric1,ModifiedBy,ModifiedDate,ResultDataTypeId")] Metric metric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metric);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ResultDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", metric.ResultDataTypeId);
            return View(metric);
        }

        // GET: Metrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metric = await _context.Metric.SingleOrDefaultAsync(m => m.MetricId == id);
            if (metric == null)
            {
                return NotFound();
            }
            ViewData["ResultDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", metric.ResultDataTypeId);
            return View(metric);
        }

        // POST: Metrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MetricId,CreatedBy,CreatedDate,DefaultResultValue,Metric1,ModifiedBy,ModifiedDate,ResultDataTypeId")] Metric metric)
        {
            if (id != metric.MetricId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetricExists(metric.MetricId))
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
            ViewData["ResultDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", metric.ResultDataTypeId);
            return View(metric);
        }

        // GET: Metrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metric = await _context.Metric
                .Include(m => m.ResultDataType)
                .SingleOrDefaultAsync(m => m.MetricId == id);
            if (metric == null)
            {
                return NotFound();
            }

            return View(metric);
        }

        // POST: Metrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metric = await _context.Metric.SingleOrDefaultAsync(m => m.MetricId == id);
            _context.Metric.Remove(metric);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MetricExists(int id)
        {
            return _context.Metric.Any(e => e.MetricId == id);
        }
    }
}
