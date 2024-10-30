using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using MvcFlowers.Models;
using MvcFlowers.Data;

namespace MvcFlowers.Models;
public static class SeedDataBq
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcFlowersContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcFlowersContext>>()))
        {
            // Look for any movies.
            if (context.Bouqets.Any())
            {
                return;   // DB has been seeded
            }
        }
    }
}
