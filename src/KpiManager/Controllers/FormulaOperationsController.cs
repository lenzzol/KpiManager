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
    public class FormulaOperationsController : Controller
    {
        private readonly MetricContext _context;

        public FormulaOperationsController(MetricContext context)
        {
            _context = context;    
        }

        // GET: FormulaOperations
        public async Task<IActionResult> Index()
        {
            var metricContext = _context.FormulaOperation.Include(f => f.MetricFormula).Include(f => f.Operand).Include(f => f.Operator).OrderBy(x => x.FormulaOperationId);
            return View(await metricContext.ToListAsync());
        }

        // GET: FormulaOperations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulaOperation = await _context.FormulaOperation
                .Include(f => f.MetricFormula)
                .Include(f => f.Operand)
                .Include(f => f.Operator)
                .SingleOrDefaultAsync(m => m.FormulaOperationId == id);
            if (formulaOperation == null)
            {
                return NotFound();
            }

            return View(formulaOperation);
        }

        // GET: FormulaOperations/Create
        public IActionResult Create()
        {
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description");
            ViewData["OperandId"] = new SelectList(_context.Operand, "OperandId", "OperandId");
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1");
            return View();
        }

        // POST: FormulaOperations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormulaOperationId,CreatedBy,CreatedDate,MetricFormulaId,ModifiedBy,ModifiedDate,OperandId,OperationOrder,OperatorId")] FormulaOperation formulaOperation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formulaOperation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description", formulaOperation.MetricFormulaId);
            ViewData["OperandId"] = new SelectList(_context.Operand, "OperandId", "OperandId", formulaOperation.OperandId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1", formulaOperation.OperatorId);
            return View(formulaOperation);
        }

        // GET: FormulaOperations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulaOperation = await _context.FormulaOperation.SingleOrDefaultAsync(m => m.FormulaOperationId == id);
            if (formulaOperation == null)
            {
                return NotFound();
            }
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description", formulaOperation.MetricFormulaId);
            ViewData["OperandId"] = new SelectList(_context.Operand, "OperandId", "OperandId", formulaOperation.OperandId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1", formulaOperation.OperatorId);
            return View(formulaOperation);
        }

        // POST: FormulaOperations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormulaOperationId,CreatedBy,CreatedDate,MetricFormulaId,ModifiedBy,ModifiedDate,OperandId,OperationOrder,OperatorId")] FormulaOperation formulaOperation)
        {
            if (id != formulaOperation.FormulaOperationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formulaOperation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormulaOperationExists(formulaOperation.FormulaOperationId))
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
            ViewData["MetricFormulaId"] = new SelectList(_context.MetricFormula, "MetricFormulaId", "Description", formulaOperation.MetricFormulaId);
            ViewData["OperandId"] = new SelectList(_context.Operand, "OperandId", "OperandId", formulaOperation.OperandId);
            ViewData["OperatorId"] = new SelectList(_context.Operator, "OperatorId", "Operator1", formulaOperation.OperatorId);
            return View(formulaOperation);
        }

        // GET: FormulaOperations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulaOperation = await _context.FormulaOperation
                .Include(f => f.MetricFormula)
                .Include(f => f.Operand)
                .Include(f => f.Operator)
                .SingleOrDefaultAsync(m => m.FormulaOperationId == id);
            if (formulaOperation == null)
            {
                return NotFound();
            }

            return View(formulaOperation);
        }

        // POST: FormulaOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formulaOperation = await _context.FormulaOperation.SingleOrDefaultAsync(m => m.FormulaOperationId == id);
            _context.FormulaOperation.Remove(formulaOperation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FormulaOperationExists(int id)
        {
            return _context.FormulaOperation.Any(e => e.FormulaOperationId == id);
        }
    }
}
