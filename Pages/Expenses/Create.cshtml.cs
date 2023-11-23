using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.Data;
using ExpenseTracker.Model;

namespace ExpenseTracker.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly ExpenseTracker.Data.ExpenseTrackerContext _context;

        public CreateModel(ExpenseTracker.Data.ExpenseTrackerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BudgetID"] = new SelectList(_context.Budget, "BudgetID", "BudgetName");
        ViewData["CategoryID"] = new SelectList(_context.ExpenseCategory, "CategoryID", "CategoryName");
        ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.Expense.Add(Expense);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
