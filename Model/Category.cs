using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Model
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Budget")]
        public int? BudgetID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public decimal AllocatedAmount { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Budget Budget { get; set; }

        public bool IsAmountValid()
        {
            return Budget != null && AllocatedAmount > 0 && AllocatedAmount <= Budget.TotalAmount;
        }
    }
    
}
