using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudgetRazor.Data
{
    public class Expense
    {
        public int Id { get; set; }

        [Display(Name = "Date of expense")]
        [DataType(DataType.Date)]
        public DateTime DateOfExpense { get; set; }

        //[Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required, StringLength(100)]
        public string Category { get; set; }

        public string Description { get; set; }
    }
}
