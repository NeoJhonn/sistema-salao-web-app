using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcSistemaSalao.Models;

namespace MvcSistemaSalao.Data
{
    public class MvcSistemaSalaoContext : DbContext
    {
        public MvcSistemaSalaoContext (DbContextOptions<MvcSistemaSalaoContext> options)
            : base(options)
        {
        }

        public DbSet<MvcSistemaSalao.Models.Schedule> Schedule { get; set; } = default!;

        public DbSet<MvcSistemaSalao.Models.Client> Client { get; set; }

        public DbSet<MvcSistemaSalao.Models.Login> Login { get; set; }

    }
}
