using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;
using System.Text;
using System.Security.Claims;

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
        public string[] CategoriesArray { get; set; }
        public string ExpenseCategory { get; set; }
        public decimal Sum { get; set; }        
        public string StringForChart { get; set; }
        public string SelectedCategories { get; set; }

        public async Task OnGetAsync(string categories, DateTime dateFrom, DateTime dateTo, string searchString)
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IQueryable<string> categoryQuery = from m in _context.Category
                                            orderby m.Name where m.OwnerID == currentUser
                                            select m.Name;      

            var expenses = from e in _context.Expense where e.OwnerID == currentUser
                           select e ;
      
            StringForChart = CreateStringForChart(currentUser);            

            if (!String.IsNullOrEmpty(searchString))
            {
                expenses = expenses.Where(s => s.Description.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(categories))
            {
                var arrayCategories = categories.Split(",");
                expenses = expenses.Where(x => arrayCategories.Contains(x.Category));
            }
            if (dateFrom.Year != 1)
            {
                expenses = expenses.Where(x => x.DateOfExpense >= dateFrom);
            }            
            if(dateTo.Year != 1)
            {
                expenses = expenses.Where(x => x.DateOfExpense <= dateTo);
            }

            Sum = expenses.Sum(x => x.Amount);
            CategoriesArray = await categoryQuery.Distinct().ToArrayAsync();


            Expense = await expenses.ToListAsync();
            SearchString = searchString;
            SelectedCategories = categories;
        }

        private string CreateStringForChart(string currentUser)
        {
            Dictionary<string, decimal> amountByCategory = _context.Expense.Where(x=>x.OwnerID == currentUser).GroupBy(m => m.Category).ToDictionary(g => g.Key, g => g.Sum(x=>x.Amount));

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