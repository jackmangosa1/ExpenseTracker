using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Model;

namespace ExpenseTracker.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly ExpenseTracker.Data.ExpenseTrackerContext _context;

        public IndexModel(ExpenseTracker.Data.ExpenseTrackerContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<Expense> expenseQuery = _context.Expense
            .Include(e => e.Budget)
            .Include(e => e.Category)
            .Include(e => e.User);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                expenseQuery = expenseQuery.Where(e => e.Description.Contains(SearchTerm));
            }

            Expense = await expenseQuery.ToListAsync();
        }
    }
}
