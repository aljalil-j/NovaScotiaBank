using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NovaScotia.Data;
using NovaScotia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace NovaScotia.Controllers
{
    public class HomeController : Controller

    {
        private readonly ScotiaCustomerContext _context;
        private readonly UserManager<Customer> userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ScotiaCustomerContext context, UserManager<Customer> userManager)
        {
            _context = context;
            this.userManager = userManager;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Teller()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> AccountDetailsAsync()
        {
            string userid = userManager.GetUserId(HttpContext.User);
            Customer cus = userManager.FindByIdAsync(userid).Result;
            var scotiaCus = _context.ScotiaCustomer.Where(x => x.Email == userid);
            var customer = await scotiaCus.ToListAsync();
            for (int i = 0; i < customer.Count; i++)
            {
                return View(customer[i]);
            }

            return View();
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
