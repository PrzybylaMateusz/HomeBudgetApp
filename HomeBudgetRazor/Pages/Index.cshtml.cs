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

        public IList<User> Users { get; private set; }
        public IList<Expense> Expenses { get; private set; }

        public async Task OnGetAsync()
        {
            Users = await _db.Users.AsNoTracking().ToListAsync();
            Expenses = await _db.Expenses.AsNoTracking().ToListAsync();

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            var expense = await _db.Expenses.FindAsync(id);

            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
            if (expense != null)
            {
                _db.Expenses.Remove(expense);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
