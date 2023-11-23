using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.Data;
using ExpenseTracker.Model;

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
            BudgetTitles = _context.Budget
            .Select(b => new SelectListItem
            {
                Value = b.BudgetID.ToString(),
                Text = b.BudgetName
            })
            .ToList();
            return Page();
        }
        public List<SelectListItem> BudgetTitles { get; set; }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.ExpenseCategory.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
