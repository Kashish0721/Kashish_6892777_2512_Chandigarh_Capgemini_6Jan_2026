using Microsoft.AspNetCore.Mvc;
using StudentManagementMVC.Models;
using System.Collections.Generic;

namespace StudentManagementMVC.Controllers
{
    public class ProductController : Controller
    {
        static List<Product> products = new List<Product>();
        static int id = 1;

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            p.Id = id++;
            products.Add(p);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = products.Find(x => x.Id == id);
            products.Remove(product);

            return RedirectToAction("Index");
        }
    }
}