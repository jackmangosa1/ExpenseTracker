using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Model;

namespace ExpenseTracker.Pages.Budgets
{
    public class EditModel : PageModel
    {
        private readonly ExpenseTracker.Data.ExpenseTrackerContext _context;

        public EditModel(ExpenseTracker.Data.ExpenseTrackerContext context)
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

            var budget =  await _context.Budget.FirstOrDefaultAsync(m => m.BudgetID == id);
            if (budget == null)
            {
                return NotFound();
            }
            Budget = budget;
           ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
           // {
               // return Page();
           // }

            _context.Attach(Budget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(Budget.BudgetID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BudgetExists(int id)
        {
            return _context.Budget.Any(e => e.BudgetID == id);
        }
    }
}
