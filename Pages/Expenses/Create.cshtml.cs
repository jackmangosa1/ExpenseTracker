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
            if (Expense.IsAmountValid())
            {
                ModelState.AddModelError(string.Empty, "Expense amount cannot exceed Category allocated amount.");
                ViewData["BudgetID"] = new SelectList(_context.Budget, "BudgetID", "BudgetName");
                ViewData["CategoryID"] = new SelectList(_context.ExpenseCategory, "CategoryID", "CategoryName");
                ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
                return Page();
            }

            // Retrieve the associated category
            var category = _context.ExpenseCategory
                .FirstOrDefault(c => c.CategoryID == Expense.CategoryID);

            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "Error retrieving the associated category.");
                ViewData["BudgetID"] = new SelectList(_context.Budget, "BudgetID", "BudgetName");
                ViewData["CategoryID"] = new SelectList(_context.ExpenseCategory, "CategoryID", "CategoryName");
                ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
                return Page();
            }

            // Calculate the total amount of expenses for the category
            decimal totalExpenseAmount = _context.Expense
                .Where(e => e.CategoryID == Expense.CategoryID)
                .Sum(e => e.Amount);

            // Check if the new expense's amount exceeds the category's allocated amount
            if (totalExpenseAmount + Expense.Amount > category.AllocatedAmount)
            {
                ViewData["ErrorMessage"] = "Expense amount cannot exceed Category allocated amount.";
                ViewData["BudgetID"] = new SelectList(_context.Budget, "BudgetID", "BudgetName");
                ViewData["CategoryID"] = new SelectList(_context.ExpenseCategory, "CategoryID", "CategoryName");
                ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
                return Page();
            }

            _context.Expense.Add(Expense);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
