using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcSistemaSalao.Data;
using System;
using System.Linq;

namespace MvcSistemaSalao.Models
{
    public class SeedLogin
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcSistemaSalaoContext(serviceProvider.GetRequiredService<DbContextOptions<MvcSistemaSalaoContext>>()))
            {
                // Look for any movies.
                if (context.Login.Any())
                {
                    return;   // DB has been seeded
                }

                context.Login.AddRange(
                    new Login
                    {
                        LoginID = "jhonny.azevedo",
                        Password = "My368091"
                    },

                    new Login
                    {
                        LoginID = "mylena.moraes",
                        Password = "96851808"
                    }
                 );
                    context.SaveChanges();
            }
        }
    }
}
