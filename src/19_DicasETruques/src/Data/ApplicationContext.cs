using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using src.Domain;

namespace src.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<UsuarioFuncao> UsuarioFuncoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=Tips;Username=postgres;Password=123");
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder.Model.GetEntityTypes()
                .SelectMany(p => p.GetProperties())
                .Where(p => p.ClrType == typeof(string)
                    && p.GetColumnType() is null);
                
            foreach (var property in properties)
            {
                property.SetIsUnicode(false);
            }
        }
    }
}