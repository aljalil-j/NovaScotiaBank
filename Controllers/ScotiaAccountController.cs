using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NovaScotia.Data;
using NovaScotia.Models;
using NovaScotia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NovaScotia.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ScotiaAccountController : Controller
    {
        
        private readonly ScotiaCustomerContext _context;
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;

        public ScotiaAccountController(UserManager<Customer> userManager, SignInManager<Customer> signInManager,ScotiaCustomerContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CusAccount()
        {

            string userid = userManager.GetUserId(HttpContext.User);
            Customer cus = userManager.FindByIdAsync(userid).Result;
            //var items = await _context.ScotiaCustomer.Where(x => x.AccountNumber == cus.AccountNum).ToListAsync();

            return View(await _context.ScotiaCustomer.Where(x => x.AccountNumber == cus.AccountNum).ToListAsync());



            //return View(items);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ScotiaRegister model)
        {
            if (ModelState.IsValid)
            {
                var user = new Customer
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Fname = model.Fname,
                    Lname = model.Lname,
                    AccountNum = model.AccountNumber
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {


                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "ScotiaHome");
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "ScotiaHome");
                    }

                }


                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}