using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseTrackerDbContext _context;

        //inject the instance of database into the controller

        public HomeController(ILogger<HomeController> logger, ExpenseTrackerDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            //get all the expenses from the database
            var allExpenses = _context.Expenses.ToList();

            var totalExpenses = allExpenses.Sum(e => e.Value);
            ViewBag.TotalExpenses = totalExpenses;

            return View(allExpenses);
        }

        public IActionResult ManageExpense(int? id)
        {
            if (id != null) 
            { 
                var expenseInDb = _context.Expenses.SingleOrDefault(e => e.Id == id);
                return View(expenseInDb);
            }
            return View();
		}

		public IActionResult AddExpense()
		{
			return View();
        }

		public IActionResult DeleteExpense(int id)
		{
            //Take the id and find the first match for that in the database
            var expenseInDb = _context.Expenses.SingleOrDefault(e => e.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();

			return RedirectToAction("Expenses");
		}

		public IActionResult ManageExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                //add the object into the table and save
                _context.Expenses.Add(model);  
            }
            else
            {
                //edit the object in the table and save
                 _context.Expenses.Update(model);
            }
			_context.SaveChanges();
			return RedirectToAction("Expenses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
