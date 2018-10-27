using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudgetRazor.Data
{
    public class Expense
    {
        public int Id { get; set; }
        
        public DateTime DateOfExpense { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [Required, StringLength(100)]
        public string Category { get; set; }

        public string Description { get; set; }
    }
}
