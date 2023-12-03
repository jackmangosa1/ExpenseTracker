using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Model;

namespace ExpenseTracker.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ExpenseTrackerContext _context;

        public DeleteModel(ExpenseTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.ExpenseCategory.FirstOrDefaultAsync(m => m.CategoryID == id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.ExpenseCategory.FindAsync(id);
            if (category != null)
            {
                // Manually delete related expenses
                var relatedExpenses = _context.Expense.Where(e => e.CategoryID == id);
                _context.Expense.RemoveRange(relatedExpenses);

                // Remove the category
                _context.ExpenseCategory.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
