using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using MvcFlowers.Models;
using MvcFlowers.Data;

namespace MvcFlowers.Models;
public static class SeedDataMF
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcFlowersContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcFlowersContext>>()))
        {
            // Look for any flowers.
            if (context.Flowers.Any())
            {
                return;   // DB has been seeded
            }
        }
    }
}
