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
    public class DataFieldRelationshipsController : Controller
    {
        private readonly MetricContext _context;

        public DataFieldRelationshipsController(MetricContext context)
        {
            _context = context;    
        }

        // GET: DataFieldRelationships
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.DataFieldRelationship.Include(d => d.FieldSource).Include(d => d.FieldTarget);
            return View(await metricContext.ToListAsync());
        }

        // GET: DataFieldRelationships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataFieldRelationship = await _context.DataFieldRelationship
                .Include(d => d.FieldSource)
                .Include(d => d.FieldTarget)
                .SingleOrDefaultAsync(m => m.DataFieldRelationshipId == id);
            if (dataFieldRelationship == null)
            {
                return NotFound();
            }

            return View(dataFieldRelationship);
        }

        // GET: DataFieldRelationships/Create
        public IActionResult Create()
        {
            ViewData["FieldSourceId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName");
            ViewData["FieldTargetId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName");
            return View();
        }

        // POST: DataFieldRelationships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataFieldRelationshipId,FieldSourceId,FieldTargetId")] DataFieldRelationship dataFieldRelationship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataFieldRelationship);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FieldSourceId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", dataFieldRelationship.FieldSourceId);
            ViewData["FieldTargetId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", dataFieldRelationship.FieldTargetId);
            return View(dataFieldRelationship);
        }

        // GET: DataFieldRelationships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataFieldRelationship = await _context.DataFieldRelationship.SingleOrDefaultAsync(m => m.DataFieldRelationshipId == id);
            if (dataFieldRelationship == null)
            {
                return NotFound();
            }
            ViewData["FieldSourceId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", dataFieldRelationship.FieldSourceId);
            ViewData["FieldTargetId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", dataFieldRelationship.FieldTargetId);
            return View(dataFieldRelationship);
        }

        // POST: DataFieldRelationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataFieldRelationshipId,FieldSourceId,FieldTargetId")] DataFieldRelationship dataFieldRelationship)
        {
            if (id != dataFieldRelationship.DataFieldRelationshipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataFieldRelationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataFieldRelationshipExists(dataFieldRelationship.DataFieldRelationshipId))
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
            ViewData["FieldSourceId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", dataFieldRelationship.FieldSourceId);
            ViewData["FieldTargetId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", dataFieldRelationship.FieldTargetId);
            return View(dataFieldRelationship);
        }

        // GET: DataFieldRelationships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataFieldRelationship = await _context.DataFieldRelationship
                .Include(d => d.FieldSource)
                .Include(d => d.FieldTarget)
                .SingleOrDefaultAsync(m => m.DataFieldRelationshipId == id);
            if (dataFieldRelationship == null)
            {
                return NotFound();
            }

            return View(dataFieldRelationship);
        }

        // POST: DataFieldRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataFieldRelationship = await _context.DataFieldRelationship.SingleOrDefaultAsync(m => m.DataFieldRelationshipId == id);
            _context.DataFieldRelationship.Remove(dataFieldRelationship);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DataFieldRelationshipExists(int id)
        {
            return _context.DataFieldRelationship.Any(e => e.DataFieldRelationshipId == id);
        }
    }
}
