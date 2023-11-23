using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Model;

namespace ExpenseTracker.Data
{
    public class ExpenseTrackerContext : DbContext
    {
        public ExpenseTrackerContext (DbContextOptions<ExpenseTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseTracker.Model.Expense> Expense { get; set; } = default!;
        public DbSet<ExpenseTracker.Model.Category> ExpenseCategory { get; set; } = default!;
        public DbSet<ExpenseTracker.Model.Budget> Budget { get; set; } = default!;
    }
}
