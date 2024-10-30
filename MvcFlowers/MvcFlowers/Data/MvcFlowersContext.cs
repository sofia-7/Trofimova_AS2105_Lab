using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcFlowers.Models;

namespace MvcFlowers.Data
{
    public class MvcFlowersContext : DbContext
    {
        public MvcFlowersContext (DbContextOptions<MvcFlowersContext> options)
            : base(options)
        {
        }

        public DbSet<MvcFlowers.Models.MonoFlowers> MonoFlowers { get; set; } = default!;
        public DbSet<MvcFlowers.Models.Bouqets> Bouqets { get; set; } = default!;
        public DbSet<MvcFlowers.Models.PottedFlowers> PottedFlowers { get; set; } = default!;
    }
}
