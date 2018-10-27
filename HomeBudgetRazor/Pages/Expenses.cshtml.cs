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
    public class ExpensesModel : PageModel
    {
        private readonly AppDbContext _db;

        public ExpensesModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<Expense> Expenses { get; private set; }

        public async Task OnGetAsync()
        {
            Expenses = await _db.Expenses.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var expense = await _db.Expenses.FindAsync(id);

            if (expense != null)
            {
                _db.Expenses.Remove(expense);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}