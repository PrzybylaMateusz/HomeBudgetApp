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
    public class AddExpenseModel : PageModel
    {
        private readonly AppDbContext _db;

        public AddExpenseModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<SelectListItem> Categories { get; private set; }

        //public List<SelectListItem> Categories ;

        public async Task OnGetAsync()
        {
            Categories = new List<SelectListItem>();

            foreach (var category in await _db.Categories.AsNoTracking().Select(x => x.Name).ToListAsync())
            {
                Categories.Add(new SelectListItem { Text = category, Value = category });
            }
            //Categories = await _db.Categories.AsNoTracking().Select(x => x.Name).ToListAsync();           
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Expenses.Add(Expense);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}