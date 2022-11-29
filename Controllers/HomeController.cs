using FeelGoodSubstitutes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

//add email packages
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

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

        public async Task<IActionResult> Low_High(string search_category)
        {
            //basically selecting all the items in the DB (but in a way that provides flexibility later on in this method
            var category = from m in _context.Products.OrderByDescending(s => s.Product_Price)
                           select m;

            //if we have a returned value, then filter the results by the delected category
            if (!String.IsNullOrEmpty(search_category))
            {
                category = category.Where(s => s.Category!.Contains(search_category)).OrderByDescending(s => s.Product_Price).ThenBy(s => s.Customer_Rating);
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
        //load email/recommendation form view
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string from, string subject, string a_message)
        {
            var message = new MimeMessage();
            //will send an email using a new outlook account
            message.From.Add(new MailboxAddress("Jack Witherspoon", "thisisagreatbigtest@outlook.com"));
            //next part is mostly just to fill a parameter requiement 
            message.To.Add(new MailboxAddress("Estimed ASP.NET developer", "mckaymoulton1@gmail.com"));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = from + a_message
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("thisisagreatbigtest@outlook.com", "theBagelBoy67");

                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToAction("Index");
        }


        public IActionResult AboutUs()
        {
            return View();
        }

    }

}