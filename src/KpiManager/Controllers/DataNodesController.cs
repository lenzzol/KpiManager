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
    public class DataNodesController : Controller
    {
        private readonly MetricContext _context;

        public DataNodesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: DataNodes
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.DataNode.Include(d => d.DataCategory).Include(d => d.Pointofsale);
            return View(await metricContext.ToListAsync());
        }

        // GET: DataNodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataNode = await _context.DataNode
                .Include(d => d.DataCategory)
                .Include(d => d.Pointofsale)
                .SingleOrDefaultAsync(m => m.DataNodeId == id);
            if (dataNode == null)
            {
                return NotFound();
            }

            return View(dataNode);
        }

        // GET: DataNodes/Create
        public IActionResult Create()
        {
            ViewData["DataCategoryId"] = new SelectList(_context.DataCategory, "DataCategoryId", "Category");
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId");
            return View();
        }

        // POST: DataNodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataNodeId,CreatedBy,CreatedDate,DataCategoryId,ModifiedBy,ModifiedDate,NodeName,PointofsaleId")] DataNode dataNode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataNode);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DataCategoryId"] = new SelectList(_context.DataCategory, "DataCategoryId", "Category", dataNode.DataCategoryId);
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId", dataNode.PointofsaleId);
            return View(dataNode);
        }

        // GET: DataNodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataNode = await _context.DataNode.SingleOrDefaultAsync(m => m.DataNodeId == id);
            if (dataNode == null)
            {
                return NotFound();
            }
            ViewData["DataCategoryId"] = new SelectList(_context.DataCategory, "DataCategoryId", "Category", dataNode.DataCategoryId);
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId", dataNode.PointofsaleId);
            return View(dataNode);
        }

        // POST: DataNodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataNodeId,CreatedBy,CreatedDate,DataCategoryId,ModifiedBy,ModifiedDate,NodeName,PointofsaleId")] DataNode dataNode)
        {
            if (id != dataNode.DataNodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataNode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataNodeExists(dataNode.DataNodeId))
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
            ViewData["DataCategoryId"] = new SelectList(_context.DataCategory, "DataCategoryId", "Category", dataNode.DataCategoryId);
            ViewData["PointofsaleId"] = new SelectList(_context.Pointofsale, "PointofsaleId", "PointofsaleId", dataNode.PointofsaleId);
            return View(dataNode);
        }

        // GET: DataNodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataNode = await _context.DataNode
                .Include(d => d.DataCategory)
                .Include(d => d.Pointofsale)
                .SingleOrDefaultAsync(m => m.DataNodeId == id);
            if (dataNode == null)
            {
                return NotFound();
            }

            return View(dataNode);
        }

        // POST: DataNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataNode = await _context.DataNode.SingleOrDefaultAsync(m => m.DataNodeId == id);
            _context.DataNode.Remove(dataNode);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DataNodeExists(int id)
        {
            return _context.DataNode.Any(e => e.DataNodeId == id);
        }
    }
}
