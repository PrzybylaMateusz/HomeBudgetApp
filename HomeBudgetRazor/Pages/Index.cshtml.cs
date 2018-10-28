using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudgetRazor.Data;
using HomeBudgetRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HomeBudgetRazorContext _context;

        public IndexModel(HomeBudgetRazorContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get; set; }

        public async Task OnGetAsync()
        {
            Expense = await _context.Expense.ToListAsync();
        }
        //public async Task OnGetAsync()
        //{           
        //    AllExpenses = await _db.AllExpenses.AsNoTracking().ToListAsync();
        //}

        //public async Task<IActionResult> OnPostDeleteAsync(int id)
        //{
        //    var expense = await _db.AllExpenses.FindAsync(id);

        //    if (expense != null)
        //    {
        //        _db.AllExpenses.Remove(expense);
        //        await _db.SaveChangesAsync();
        //    }

        //    return RedirectToPage();
        //}
    }
}
