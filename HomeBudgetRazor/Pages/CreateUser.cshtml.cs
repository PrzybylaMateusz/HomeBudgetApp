using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HomeBudgetRazor.Data;

namespace HomeBudgetRazor.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly AppDbContext _db;

        public CreateUserModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Users.Add(User);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}