using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HomeBudgetRazor.Models
{
    public class HomeBudgetRazorContext : IdentityDbContext<IdentityUser>
    {
        public HomeBudgetRazorContext (DbContextOptions<HomeBudgetRazorContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Expense> Expense { get; set; }
    }
}
