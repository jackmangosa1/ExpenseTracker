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
    public class IndexModel : PageModel
    {
        private readonly ExpenseTracker.Data.ExpenseTrackerContext _context;

        public IndexModel(ExpenseTracker.Data.ExpenseTrackerContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<Category> categoryQuery = _context.ExpenseCategory
       .Include(c => c.Budget)
       .Include(c => c.User);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                categoryQuery = categoryQuery.Where(c => c.CategoryName.Contains(SearchTerm));
            }

            Category = await categoryQuery.ToListAsync();
        }
    }
}
