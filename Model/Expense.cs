using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Model
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenseID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Budget")]
        public int? BudgetID { get; set; }

        [ForeignKey("Category")]
        public int? CategoryID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Budget Budget { get; set; }
        public Category Category { get; set; }

        public bool IsAmountValid()
        {

            return Budget != null && Amount > 0 && Amount <= Category.AllocatedAmount;
        }

        public bool IsAllocatedAmountExceeding50Percent(decimal totalAllocatedAmount, decimal totalBudgetAmount)
        {
            decimal fiftyPercentOfBudget = totalBudgetAmount * 0.5m;
            return totalAllocatedAmount >= fiftyPercentOfBudget;
        }
    }
}