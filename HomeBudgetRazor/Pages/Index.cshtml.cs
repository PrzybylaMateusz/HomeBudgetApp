using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<Expense> AllExpenses { get; private set; }

        public async Task OnGetAsync()
        {           
            AllExpenses = await _db.AllExpenses.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var expense = await _db.AllExpenses.FindAsync(id);
                       
            if (expense != null)
            {
                _db.AllExpenses.Remove(expense);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
