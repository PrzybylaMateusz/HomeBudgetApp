using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetRazor.Pages
{
    public class CreateExpenseModel : PageModel
    {
        private readonly AppDbContext _db;

        public CreateExpenseModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<SelectListItem> AllCategories { get; private set; }


        public async Task OnGetAsync()
        {
            AllCategories = new List<SelectListItem>();

            foreach (var category in await _db.AllCategories.AsNoTracking().Select(x => x.Name).ToListAsync())
            {
                AllCategories.Add(new SelectListItem { Text = category, Value = category });
            }
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.AllExpenses.Add(Expense);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}