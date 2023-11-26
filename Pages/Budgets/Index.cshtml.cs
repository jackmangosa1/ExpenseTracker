using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Model;

namespace ExpenseTracker.Pages.Budgets
{
    public class IndexModel : PageModel
    {
        private readonly ExpenseTracker.Data.ExpenseTrackerContext _context;

        public IndexModel(ExpenseTracker.Data.ExpenseTrackerContext context)
        {
            _context = context;
        }

        public IList<Budget> Budget { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<Budget> budgetQuery = _context.Budget.Include(b => b.User);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                budgetQuery = budgetQuery.Where(b => b.BudgetName.Contains(SearchTerm));
            }

            Budget = await budgetQuery.ToListAsync();
        }
    }
}
