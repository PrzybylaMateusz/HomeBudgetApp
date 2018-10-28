using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomeBudgetRazor.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetRazor.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly Models.HomeBudgetRazorContext _context;

        public CreateModel(Models.HomeBudgetRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public IList<SelectListItem> AllCategories { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AllCategories = new List<SelectListItem>();

            foreach (var category in await _context.Category.Select(x=>x.Name).ToListAsync())
            {
                AllCategories.Add(new SelectListItem { Text = category, Value = category });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Expense.Add(Expense);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}