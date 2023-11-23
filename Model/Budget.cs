using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Model
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BudgetID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        [Required]
        public string BudgetName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        // Navigation property
        public User User { get; set; }
    }

}
