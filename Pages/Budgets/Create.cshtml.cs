using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.Data;
using ExpenseTracker.Model;

namespace ExpenseTracker.Pages.Budgets
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
        ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "Email");
            return Page();
        }

        [BindProperty]
        public Budget Budget { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{

                //return Page();
           // }

            
                Console.WriteLine("Redirecting to Index page.");
                _context.Budget.Add(Budget);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
           
        }
    }
}
