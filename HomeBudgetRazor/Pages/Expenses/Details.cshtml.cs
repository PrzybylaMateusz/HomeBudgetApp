using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;
using HomeBudgetRazor.Models;

namespace HomeBudgetRazor.Pages.Expenses
{
    public class DetailsModel : PageModel
    {
        private readonly HomeBudgetRazor.Models.HomeBudgetRazorContext _context;

        public DetailsModel(HomeBudgetRazor.Models.HomeBudgetRazorContext context)
        {
            _context = context;
        }

        public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Expense = await _context.Expense.FirstOrDefaultAsync(m => m.Id == id);

            if (Expense == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
