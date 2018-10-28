using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudgetRazor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<Expense> AllExpenses { get; set; }
        public DbSet<Category> AllCategories { get; set; }
    }
}
