using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;

namespace HomeBudgetRazor.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly Models.HomeBudgetRazorContext _context;

        public IndexModel(Models.HomeBudgetRazorContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get;set; }

        public async Task OnGetAsync()
        {
            Expense = await _context.Expense.ToListAsync();
        }
    }
}
