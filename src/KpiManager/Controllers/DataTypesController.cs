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
    public class DataTypesController : Controller
    {
        private readonly MetricContext _context;

        public DataTypesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: DataTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DataType.ToListAsync());
        }

        // GET: DataTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataType = await _context.DataType
                .SingleOrDefaultAsync(m => m.DataTypeId == id);
            if (dataType == null)
            {
                return NotFound();
            }

            return View(dataType);
        }

        // GET: DataTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataTypeId,CreatedBy,CreatedDate,DataType1,FloatPrecision,IsNumeric,ModifiedBy,ModifiedDate")] DataType dataType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dataType);
        }

        // GET: DataTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataType = await _context.DataType.SingleOrDefaultAsync(m => m.DataTypeId == id);
            if (dataType == null)
            {
                return NotFound();
            }
            return View(dataType);
        }

        // POST: DataTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataTypeId,CreatedBy,CreatedDate,DataType1,FloatPrecision,IsNumeric,ModifiedBy,ModifiedDate")] DataType dataType)
        {
            if (id != dataType.DataTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataTypeExists(dataType.DataTypeId))
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
            return View(dataType);
        }

        // GET: DataTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataType = await _context.DataType
                .SingleOrDefaultAsync(m => m.DataTypeId == id);
            if (dataType == null)
            {
                return NotFound();
            }

            return View(dataType);
        }

        // POST: DataTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataType = await _context.DataType.SingleOrDefaultAsync(m => m.DataTypeId == id);
            _context.DataType.Remove(dataType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DataTypeExists(int id)
        {
            return _context.DataType.Any(e => e.DataTypeId == id);
        }
    }
}
