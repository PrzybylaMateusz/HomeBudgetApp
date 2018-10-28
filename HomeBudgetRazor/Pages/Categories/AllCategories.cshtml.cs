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
    public class AllCategoriesModel : PageModel
    {
        private readonly AppDbContext _db;

        [TempData]
        public string Message { get; set; }

        public AllCategoriesModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<Category> AllCategories { get; private set; }

        public async Task OnGetAsync()
        {
            AllCategories = await _db.AllCategories.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var category = await _db.AllCategories.FindAsync(id);

            if (category != null)
            {
                _db.AllCategories.Remove(category);
                await _db.SaveChangesAsync();
            }

            return Redirect("./AllCategories");
        }
    }
}