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
    public class CategoriesModel : PageModel
    {
        private readonly AppDbContext _db;

        public CategoriesModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<Category> Categories { get; private set; }

        public async Task OnGetAsync()
        {
            Categories = await _db.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);

            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
            }

            return Redirect("/Categories");
        }
    }
}