using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.Data;
using ExpenseTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Pages.Categories
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
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "Email");

            // Reload the BudgetTitles list in OnGet
            BudgetTitles = GetBudgetTitles();

            // Initialize Category with a new instance and load the associated Budget
            Category = new Category();
            Category.Budget = _context.Budget.FirstOrDefault(); // You might need to adjust this based on your business logic

            return Page();
        }

        public List<SelectListItem> BudgetTitles { get; set; }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (Category.IsAmountValid())
            {
                ViewData["ErrorMessage"] = "Allocated amount cannot exceed budget amount.";
                return OnGet(); // Reload the BudgetTitles list before rendering the page
            }

            var budget = _context.Budget
                .FirstOrDefault(b => b.BudgetID == Category.BudgetID);

            if (budget == null)
            {
                ViewData["ErrorMessage"] = "Error retrieving the associated budget.";
                return OnGet(); // Reload the BudgetTitles list before rendering the page
            }

            // Retrieve the associated expense categories
            var relatedExpenseCategories = _context.ExpenseCategory
                .Where(ec => ec.BudgetID == Category.BudgetID)
                .ToList();

            // Calculate the total allocated amount for the budget's categories
            decimal totalAllocatedAmount = relatedExpenseCategories.Sum(c => c.AllocatedAmount);

            // Check if the new category's allocation exceeds the budget amount
            if (totalAllocatedAmount + Category.AllocatedAmount > budget.TotalAmount)
            {
                ViewData["ErrorMessage"] = "Allocated amount cannot exceed budget amount.";
                return OnGet(); // Reload the BudgetTitles list before rendering the page
            }

            _context.ExpenseCategory.Add(Category);
            await _context.SaveChangesAsync();

            // Reload the BudgetTitles list after adding a category
            BudgetTitles = GetBudgetTitles();

            return RedirectToPage("./Index");
        }
        // Helper method to get the list of BudgetTitles
        private List<SelectListItem> GetBudgetTitles()
        {
            return _context.Budget
                .Select(b => new SelectListItem
                {
                    Value = b.BudgetID.ToString(),
                    Text = b.BudgetName
                })
                .ToList();
        }
    }

}
