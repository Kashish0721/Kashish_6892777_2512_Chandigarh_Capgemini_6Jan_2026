using Microsoft.AspNetCore.Mvc;
using EcommerceOrderSystem.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceOrderSystem.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            var topProducts = _context.OrderItems
                .GroupBy(o => o.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToList();

            var pendingOrders = _context.ShippingDetails
                .Where(s => s.Status == "Pending")
                .ToList();

            ViewBag.TopProducts = topProducts;
            ViewBag.PendingOrders = pendingOrders;

            return View();
        }
    }
}