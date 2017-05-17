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
    public class OperandsController : Controller
    {
        private readonly MetricContext _context;

        public OperandsController(MetricContext context)
        {
            _context = context;    
        }

        // GET: Operands
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.Operand.Include(o => o.OperandDataField).Include(o => o.OperandDataType).OrderBy(x => x.OperandId);
            return View(await metricContext.ToListAsync());
        }

        // GET: Operands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operand = await _context.Operand
                .Include(o => o.OperandDataField)
                .Include(o => o.OperandDataType)
                .OrderBy(x => x.OperandId)
                .SingleOrDefaultAsync(m => m.OperandId == id);
            if (operand == null)
            {
                return NotFound();
            }

            return View(operand);
        }

        // GET: Operands/Create
        public IActionResult Create()
        {
            var list = new SelectList(_context.DataField, "DataFieldId", "DataFieldName");
            ViewData["OperandDataFieldId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName").Append(new SelectListItem() { Selected = true, Text = null, Value = null});
            ViewData["OperandDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1");
            return View();
        }

        // POST: Operands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperandId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,OperandDataFieldId,OperandDataTypeId,OperandValue")] Operand operand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operand);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["OperandDataFieldId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", operand.OperandDataFieldId).Append(new SelectListItem() { Selected = true, Text = null, Value = null });
            ViewData["OperandDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", operand.OperandDataTypeId);
            return View(operand);
        }

        // GET: Operands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operand = await _context.Operand.SingleOrDefaultAsync(m => m.OperandId == id);
            if (operand == null)
            {
                return NotFound();
            }
            ViewData["OperandDataFieldId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", operand.OperandDataFieldId).Append(new SelectListItem() { Selected = true, Text = null, Value = null });
            ViewData["OperandDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", operand.OperandDataTypeId);
            return View(operand);
        }

        // POST: Operands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OperandId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,OperandDataFieldId,OperandDataTypeId,OperandValue")] Operand operand)
        {
            if (id != operand.OperandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperandExists(operand.OperandId))
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
            ViewData["OperandDataFieldId"] = new SelectList(_context.DataField, "DataFieldId", "DataFieldName", operand.OperandDataFieldId).Append(new SelectListItem() { Selected = true, Text = null, Value = null });
            ViewData["OperandDataTypeId"] = new SelectList(_context.DataType, "DataTypeId", "DataType1", operand.OperandDataTypeId);
            return View(operand);
        }

        // GET: Operands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operand = await _context.Operand
                .Include(o => o.OperandDataField)
                .Include(o => o.OperandDataType)
                .OrderBy(x => x.OperandId)
                .SingleOrDefaultAsync(m => m.OperandId == id);
            if (operand == null)
            {
                return NotFound();
            }

            return View(operand);
        }

        // POST: Operands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operand = await _context.Operand.SingleOrDefaultAsync(m => m.OperandId == id);
            _context.Operand.Remove(operand);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OperandExists(int id)
        {
            return _context.Operand.Any(e => e.OperandId == id);
        }
    }
}
