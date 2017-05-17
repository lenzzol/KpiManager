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
    public class DataCategoriesController : Controller
    {
        private readonly MetricContext _context;

        public DataCategoriesController(MetricContext context)
        {
            _context = context;    
        }

        // GET: DataCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.DataCategory.ToListAsync());
        }

        // GET: DataCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataCategory = await _context.DataCategory
                .SingleOrDefaultAsync(m => m.DataCategoryId == id);
            if (dataCategory == null)
            {
                return NotFound();
            }

            return View(dataCategory);
        }

        // GET: DataCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataCategoryId,Category,CreatedBy,CreatedDate,Description,ModifiedBy,ModifiedDate")] DataCategory dataCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dataCategory);
        }

        // GET: DataCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataCategory = await _context.DataCategory.SingleOrDefaultAsync(m => m.DataCategoryId == id);
            if (dataCategory == null)
            {
                return NotFound();
            }
            return View(dataCategory);
        }

        // POST: DataCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataCategoryId,Category,CreatedBy,CreatedDate,Description,ModifiedBy,ModifiedDate")] DataCategory dataCategory)
        {
            if (id != dataCategory.DataCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataCategoryExists(dataCategory.DataCategoryId))
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
            return View(dataCategory);
        }

        // GET: DataCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataCategory = await _context.DataCategory
                .SingleOrDefaultAsync(m => m.DataCategoryId == id);
            if (dataCategory == null)
            {
                return NotFound();
            }

            return View(dataCategory);
        }

        // POST: DataCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataCategory = await _context.DataCategory.SingleOrDefaultAsync(m => m.DataCategoryId == id);
            _context.DataCategory.Remove(dataCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DataCategoryExists(int id)
        {
            return _context.DataCategory.Any(e => e.DataCategoryId == id);
        }
    }
}
