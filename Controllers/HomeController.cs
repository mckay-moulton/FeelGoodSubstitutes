using FeelGoodSubstitutes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}