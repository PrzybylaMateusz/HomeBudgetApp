using Microsoft.EntityFrameworkCore;
using HomeBudgetRazor.Data;

namespace HomeBudgetRazor.Models
{
    public class HomeBudgetRazorContext : DbContext
    {
        public HomeBudgetRazorContext (DbContextOptions<HomeBudgetRazorContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Expense> Expense { get; set; }
    }
}
