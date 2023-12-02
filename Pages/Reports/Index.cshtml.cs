using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ExpenseTracker.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly ExpenseTrackerContext _context;

        public IndexModel(ExpenseTrackerContext context)
        {
            _context = context;
        }

        public async Task<JsonResult> OnGetMonthlyExpenseData()
        {
            var monthlyExpenses = await _context.Expense
                .Where(e => e.Date.Month == DateTime.Now.Month && e.Date.Year == DateTime.Now.Year)
                .Include(e => e.Category)
                .GroupBy(e => e.Category.CategoryName)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalExpense = g.Sum(e => e.Amount)
                })
                .ToListAsync();

            return new JsonResult(monthlyExpenses);
        }

        public async Task OnGet()
        {
            var monthlyExpenseData = await OnGetMonthlyExpenseData();
            ViewData["MonthlyExpenseData"] = monthlyExpenseData.Value; // Access the Value property
            Debug.WriteLine("Monthly Expense Data: " + JsonConvert.SerializeObject(monthlyExpenseData.Value));
            // Ensure that ViewData is properly populated before rendering the view
            ViewData["PageLoaded"] = true;
        }
    }
}
