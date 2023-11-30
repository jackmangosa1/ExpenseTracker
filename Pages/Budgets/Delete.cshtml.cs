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
    public class DeleteModel : PageModel
    {
        private readonly ExpenseTracker.Data.ExpenseTrackerContext _context;

        public DeleteModel(ExpenseTracker.Data.ExpenseTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Budget Budget { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget.FirstOrDefaultAsync(m => m.BudgetID == id);

            if (budget == null)
            {
                return NotFound();
            }
            else
            {
                Budget = budget;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budget.FindAsync(id);
            if (budget != null)
            {
                // Manually delete related expenses
                var relatedExpenses = _context.Expense.Where(e => e.BudgetID == id);
                _context.Expense.RemoveRange(relatedExpenses);

                // Manually delete related expense categories
                var relatedExpenseCategories = _context.ExpenseCategory.Where(ec => ec.BudgetID == id);
                _context.ExpenseCategory.RemoveRange(relatedExpenseCategories);

                // Remove the budget
                _context.Budget.Remove(budget);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
