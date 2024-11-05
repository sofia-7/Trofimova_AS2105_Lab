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
        public DbSet<MvcFlowers.Models.Bouqet> Bouqet { get; set; } = default!;
        public DbSet<MonoFlowers> Flowers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
        base.OnModelCreating(modelBuilder);

        // Настройка отношения "многие ко многим" между Bouqet и MonoFlowers
        modelBuilder.Entity<Bouqet>()
            .HasMany(b => b.Flowers)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "BouqetFlowers", // Имя промежуточной таблицы
                j => j
                    .HasOne<MonoFlowers>()
                    .WithMany()
                    .HasForeignKey("MonoFlowerId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Bouqet>()
                    .WithMany()
                    .HasForeignKey("BouqetId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("BouqetId", "MonoFlowerId"); // Установка составного ключа
                });
}

    }
}
