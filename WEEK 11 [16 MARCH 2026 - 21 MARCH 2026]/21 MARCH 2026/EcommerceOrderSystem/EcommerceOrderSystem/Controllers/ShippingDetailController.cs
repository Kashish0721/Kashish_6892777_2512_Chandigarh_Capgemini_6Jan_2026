using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceOrderSystem.Models;

namespace EcommerceOrderSystem.Controllers
{
    public class ShippingDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShippingDetail
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShippingDetails.Include(s => s.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ShippingDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            return View(shippingDetail);
        }

        // GET: ShippingDetail/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: ShippingDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address,Status,OrderId")] ShippingDetail shippingDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shippingDetail.OrderId);
            return View(shippingDetail);
        }

        // GET: ShippingDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shippingDetail.OrderId);
            return View(shippingDetail);
        }

        // POST: ShippingDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Status,OrderId")] ShippingDetail shippingDetail)
        {
            if (id != shippingDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingDetailExists(shippingDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shippingDetail.OrderId);
            return View(shippingDetail);
        }

        // GET: ShippingDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingDetail = await _context.ShippingDetails
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingDetail == null)
            {
                return NotFound();
            }

            return View(shippingDetail);
        }

        // POST: ShippingDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingDetail = await _context.ShippingDetails.FindAsync(id);
            if (shippingDetail != null)
            {
                _context.ShippingDetails.Remove(shippingDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingDetailExists(int id)
        {
            return _context.ShippingDetails.Any(e => e.Id == id);
        }
    }
}
