using FeelGoodSubstitutes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace FeelGoodSubstitutes.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext _context { get; set; }
        public HomeController(MyDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            var prod = _context.Products.ToList();
            return View(prod);
        }

        // make method for returning results based off category
        public async Task<IActionResult> CategoryView(string search_category)
        {
            //basically selecting all the items in the DB (but in a way that provides flexibility later on in this method
            var category = from m in _context.Products
                           select m;
            
            //if we have a returned value, then filter the results by the delected category
            if (!String.IsNullOrEmpty(search_category))
            {
                category = category.Where(s => s.Category!.Contains(search_category)).OrderByDescending(s => s.Eco_Rating).ThenBy(s => s.Customer_Rating);
            }

            return View(await category.ToListAsync());
        }


        public IActionResult Product(int id)
        {
            var product = _context.Products.Single(x => x.ID == id);

            return View("Product", product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

    }
}