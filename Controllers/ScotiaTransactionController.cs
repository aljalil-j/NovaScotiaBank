using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NovaScotia.Data;
using NovaScotia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace NovaScotia.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ScotiaTransactionController : Controller
    {
        
        private readonly ScotiaTransactionContext _context;
        private readonly ScotiaCustomerContext _context2;
        private readonly UserManager<Customer> userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ScotiaTransactionController(ScotiaTransactionContext context, UserManager<Customer> userManager, IHttpContextAccessor httpContextAccessor,
            ScotiaCustomerContext context2 )
        {
            _context = context;
            this.userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context2 = context2;
        }

        // GET: ScotiaTransaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScotiaTransaction.ToListAsync());
        }

        public async Task<IActionResult> CusTransactions()
        {

            string userid = userManager.GetUserId(HttpContext.User);
            Customer cus = userManager.FindByIdAsync(userid).Result;
           // var items = await _context2.Payment.Where(x => x.premisesNum == cus.premisesNumber).ToListAsync();

            return View(await _context.ScotiaTransaction.Where(x => x.AccountNumber == cus.AccountNum).ToListAsync());

            
        }

        [HttpGet]
        public IActionResult Deposit()
        {

            return View();

        }

        [HttpPost]
        public  async Task<IActionResult> Deposit([Bind("Id,AccountNumber,TransactionType,Amount")] ScotiaTransaction scotiaTransaction)
        {

            // Query the database for the row to be updated.
            var query =
                from p in _context2.ScotiaCustomer
                where p.AccountNumber == scotiaTransaction.AccountNumber
                select p;

            // Execute the query, and change the column values
            // you want to change.
            foreach (ScotiaCustomer p in query)
            {

                p.Balance = p.Balance + scotiaTransaction.Amount;
                p.AvaBalance = p.AvaBalance + scotiaTransaction.Amount;
                // Insert any additional changes to column values.
            }
            _context.Add(scotiaTransaction);
            await _context.SaveChangesAsync();
           // return RedirectToAction(nameof(Index));
            // Submit the changes to the database.
            try
            {
                _context2.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }

            return RedirectToAction(nameof(Index));


        }

        [HttpGet]
        public IActionResult Withdraw()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Withdraw([Bind("Id,AccountNumber,TransactionType,Amount")] ScotiaTransaction scotiaTransaction)
        {
                 

            // Query the database for the row to be updated.
            var query =
                from p in _context2.ScotiaCustomer
                where p.AccountNumber == scotiaTransaction.AccountNumber
                select p;



            // Execute the query, and change the column values
            // you want to change.
            foreach (ScotiaCustomer p in query)
            {
                Expression subtractExpr = Expression.Subtract(
                             Expression.Constant(p.Balance),
                            Expression.Constant(scotiaTransaction.Amount)
                    );

                p.Balance = Expression.Lambda<Func<decimal>>(subtractExpr).Compile().Invoke();
                p.AvaBalance = Expression.Lambda<Func<decimal>>(subtractExpr).Compile().Invoke();
                // Insert any additional changes to column values.
            }
            _context.Add(scotiaTransaction);
            await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            // Submit the changes to the database.
            try
            {
                _context2.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }

            return RedirectToAction(nameof(Index));


        }


        // GET: ScotiaTransaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scotiaTransaction = await _context.ScotiaTransaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scotiaTransaction == null)
            {
                return NotFound();
            }

            return View(scotiaTransaction);
        }

        // GET: ScotiaTransaction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScotiaTransaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountNumber,TransactionType,Amount")] ScotiaTransaction scotiaTransaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scotiaTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scotiaTransaction);
        }

        // GET: ScotiaTransaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scotiaTransaction = await _context.ScotiaTransaction.FindAsync(id);
            if (scotiaTransaction == null)
            {
                return NotFound();
            }
            return View(scotiaTransaction);
        }

        // POST: ScotiaTransaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountNumber,TransactionType,Amount")] ScotiaTransaction scotiaTransaction)
        {
            if (id != scotiaTransaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scotiaTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScotiaTransactionExists(scotiaTransaction.Id))
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
            return View(scotiaTransaction);
        }

        // GET: ScotiaTransaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scotiaTransaction = await _context.ScotiaTransaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scotiaTransaction == null)
            {
                return NotFound();
            }

            return View(scotiaTransaction);
        }

        // POST: ScotiaTransaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scotiaTransaction = await _context.ScotiaTransaction.FindAsync(id);
            _context.ScotiaTransaction.Remove(scotiaTransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScotiaTransactionExists(int id)
        {
            return _context.ScotiaTransaction.Any(e => e.Id == id);
        }
    }
}
