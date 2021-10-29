using System;
using Microsoft.EntityFrameworkCore;
using src.Entities;

namespace src.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Deparamento> Deparamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine)
                .UseNpgsql("Host=localhost;Database=SobreescrevendoComportamentos;Username=postgres;Password=123")
                .EnableSensitiveDataLogging();
        }
    }
}