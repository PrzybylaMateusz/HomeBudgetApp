using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;
using HomeBudgetRazor.Models;
using System.Security.Claims;

namespace HomeBudgetRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly HomeBudgetRazor.Models.HomeBudgetRazorContext _context;

        public IndexModel(HomeBudgetRazor.Models.HomeBudgetRazorContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Category = await _context.Category.Where(x=>x.OwnerID == currentUser).ToListAsync();
        }
    }
}
