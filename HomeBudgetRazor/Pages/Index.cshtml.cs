using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetRazor.Data;
using HomeBudgetRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetRazor.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly HomeBudgetRazorContext _context;

        public IndexModel(HomeBudgetRazorContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get; set; }
        public string StringForChart { get; set; }


        public async Task OnGetAsync()
        {
            Expense = await _context.Expense.ToListAsync();

            StringForChart = CreateStringForChart();
        }

        private string CreateStringForChart()
        {
            Dictionary<string, decimal> amountByCategory = _context.Expense.GroupBy(m => m.Category).ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[['Category', 'Amount'],");
            foreach (var item in amountByCategory)
            {
                sb.AppendFormat("['{0}', {1}],", item.Key, item.Value.ToString().Replace(",", "."));
            }
            sb.AppendFormat("]");
            return sb.ToString();
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
