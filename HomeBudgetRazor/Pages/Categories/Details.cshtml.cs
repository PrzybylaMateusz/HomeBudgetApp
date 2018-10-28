using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;
using HomeBudgetRazor.Models;

namespace HomeBudgetRazor.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly HomeBudgetRazor.Models.HomeBudgetRazorContext _context;

        public DetailsModel(HomeBudgetRazor.Models.HomeBudgetRazorContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
