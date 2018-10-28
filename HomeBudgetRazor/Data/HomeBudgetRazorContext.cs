using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<HomeBudgetRazor.Data.Category> Category { get; set; }
        public DbSet<HomeBudgetRazor.Data.Expense> Expense { get; set; }
    }
}
