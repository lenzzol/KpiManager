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
    public class DataFieldsController : Controller
    {
        private readonly MetricContext _context;

        public DataFieldsController(MetricContext context)
        {
            _context = context;    
        }

        // GET: DataFields
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.DataField.OrderBy(x => x.DataNodeId).Include(d => d.DataFieldRelationship).Include(d => d.DataNode).Include(d => d.DataType);
            return View(await metricContext.ToListAsync());
        }

        // GET: DataFields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataField = await _context.DataField
                .Include(d => d.DataFieldRelationship)
                .Include(d => d.DataNode)
                .Include(d => d.DataType)
                .OrderBy(x => x.DataNodeId)
                .SingleOrDefaultAsync(m => m.DataFieldId == id);
            if (dataField == null)
            {
                return NotFound();
            }

            return View(dataField);
        }

        // GET: DataFields/Create
        public IActionResult Create()
        {
            ViewData["DataFieldRelationshipId"] = new SelectList(_context.DataFieldRelationship, "DataFieldRelationshipId", "DataFieldRelationshipId");
            ViewData["DataNodeId"] = new SelectList(_context.DataNode, "DataNodeId", "NodeName");
            ViewData["DataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1");
            return View();
        }

        // POST: DataFields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataFieldId,CreatedBy,CreatedDate,DataFieldName,DataFieldRelationshipId,DataNodeId,DataTypeId,ModifiedBy,ModifiedDate")] DataField dataField)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataField);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DataFieldRelationshipId"] = new SelectList(_context.DataFieldRelationship, "DataFieldRelationshipId", "DataFieldRelationshipId", dataField.DataFieldRelationshipId);
            ViewData["DataNodeId"] = new SelectList(_context.DataNode, "DataNodeId", "NodeName", dataField.DataNodeId);
            ViewData["DataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", dataField.DataTypeId);
            return View(dataField);
        }

        // GET: DataFields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataField = await _context.DataField.OrderBy(x => x.DataNodeId).SingleOrDefaultAsync(m => m.DataFieldId == id);
            if (dataField == null)
            {
                return NotFound();
            }
            ViewData["DataFieldRelationshipId"] = new SelectList(_context.DataFieldRelationship, "DataFieldRelationshipId", "DataFieldRelationshipId", dataField.DataFieldRelationshipId);
            ViewData["DataNodeId"] = new SelectList(_context.DataNode, "DataNodeId", "NodeName", dataField.DataNodeId);
            ViewData["DataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", dataField.DataTypeId);
            return View(dataField);
        }

        // POST: DataFields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataFieldId,CreatedBy,CreatedDate,DataFieldName,DataFieldRelationshipId,DataNodeId,DataTypeId,ModifiedBy,ModifiedDate")] DataField dataField)
        {
            if (id != dataField.DataFieldId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataFieldExists(dataField.DataFieldId))
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
            ViewData["DataFieldRelationshipId"] = new SelectList(_context.DataFieldRelationship, "DataFieldRelationshipId", "DataFieldRelationshipId", dataField.DataFieldRelationshipId);
            ViewData["DataNodeId"] = new SelectList(_context.DataNode, "DataNodeId", "NodeName", dataField.DataNodeId);
            ViewData["DataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", dataField.DataTypeId);
            return View(dataField);
        }

        // GET: DataFields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataField = await _context.DataField
                .Include(d => d.DataFieldRelationship)
                .Include(d => d.DataNode)
                .Include(d => d.DataType)
                .OrderBy(x => x.DataNodeId)
                .SingleOrDefaultAsync(m => m.DataFieldId == id);
            if (dataField == null)
            {
                return NotFound();
            }

            return View(dataField);
        }

        // POST: DataFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataField = await _context.DataField.SingleOrDefaultAsync(m => m.DataFieldId == id);
            _context.DataField.Remove(dataField);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DataFieldExists(int id)
        {
            return _context.DataField.Any(e => e.DataFieldId == id);
        }
    }
}
