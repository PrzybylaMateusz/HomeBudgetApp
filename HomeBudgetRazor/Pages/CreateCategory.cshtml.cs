using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeBudgetRazor.Pages
{
    public class CreateCategoryModel : PageModel
    {
        private readonly AppDbContext _db;

        public CreateCategoryModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Categories.Add(Category);
            await _db.SaveChangesAsync();
            return Redirect("/Categories");
        }
    }
}