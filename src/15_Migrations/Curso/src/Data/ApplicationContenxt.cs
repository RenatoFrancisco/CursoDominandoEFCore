using System;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevI-O3;Integrated Security=true";

            optionsBuilder
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(p => 
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Nome).HasMaxLength(60).IsUnicode(false);
            });
        }
    }
}