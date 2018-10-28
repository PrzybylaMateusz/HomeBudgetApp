using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;

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
        public string SearchString { get; set; }
        public SelectList Categories { get; set; }
        public string ExpenseCategory { get; set; }

        public async Task OnGetAsync(string expenseCategory, string searchString)
        {
            IQueryable<string> categoryQuery = from m in _context.Category
                                            orderby m.Name
                                            select m.Name;

            var expenses = from e in _context.Expense
                           select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                expenses = expenses.Where(s => s.Description.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(expenseCategory))
            {
                expenses = expenses.Where(x => x.Category == expenseCategory);
            }

            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Expense = await expenses.ToListAsync();
            SearchString = searchString;
        }
    }
}
