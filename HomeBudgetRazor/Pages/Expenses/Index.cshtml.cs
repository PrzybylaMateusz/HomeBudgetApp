using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
        public decimal Sum { get; set; }        
        public string StringForChart { get; set; }

        public async Task OnGetAsync(string expenseCategory, string searchString)
        {
            IQueryable<string> categoryQuery = from m in _context.Category
                                            orderby m.Name
                                            select m.Name;


            var expenses = from e in _context.Expense
                           select e;

            StringForChart = CreateStringForChart();           

            if (!String.IsNullOrEmpty(searchString))
            {
                expenses = expenses.Where(s => s.Description.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(expenseCategory))
            {
                expenses = expenses.Where(x => x.Category == expenseCategory);
            }

            Sum = expenses.Sum(x => x.Amount);
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Expense = await expenses.ToListAsync();
            SearchString = searchString;
        }

        private string CreateStringForChart()
        {
            Dictionary<string, decimal> amountByCategory = _context.Expense.GroupBy(m => m.Category).ToDictionary(g => g.Key, g => g.Sum(x=>x.Amount));

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[['Category', 'Amount'],");
            foreach (var item in amountByCategory)
            {
                sb.AppendFormat("['{0}', {1}],", item.Key, item.Value.ToString().Replace(",","."));
            }
            sb.AppendFormat("]");
            return sb.ToString();
        }
    }
}
