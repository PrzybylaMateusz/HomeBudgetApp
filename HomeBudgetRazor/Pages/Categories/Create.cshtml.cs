using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomeBudgetRazor.Data;
using HomeBudgetRazor.Models;

namespace HomeBudgetRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly HomeBudgetRazor.Models.HomeBudgetRazorContext _context;

        public CreateModel(HomeBudgetRazor.Models.HomeBudgetRazorContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}