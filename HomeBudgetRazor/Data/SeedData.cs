using HomeBudgetRazor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HomeBudgetRazor.Data
{
    public static class SeedData
    {      
        public static void Initialize(IServiceProvider serviceProvider)
        {     
            using (var context = new HomeBudgetRazorContext(
                serviceProvider.GetRequiredService<DbContextOptions<HomeBudgetRazorContext>>()))
            {                
                if (context.Category.Any())
                {
                    return; 
                }
                
                context.SaveChanges();
            }
        }
    }
}
