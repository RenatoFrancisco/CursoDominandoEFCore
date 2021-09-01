using System;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly StreamWriter _writer = new StreamWriter("log_ef_core.txt", append: true);
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection="Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO-02;Integrated Security=true;pooling=true;";
            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                // .LogTo(Console.WriteLine, new [] { CoreEventId.ContextInitialized, RelationalEventId.CommandExecuted }, 
                //     LogLevel.Information,
                //     DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine);
                // .LogTo(_writer.WriteLine, LogLevel.Information);
                // .EnableDetailedErrors();
                .EnableSensitiveDataLogging();
        }

        public override void Dispose()
        {
            base.Dispose();
            _writer.Dispose();
        }
    }
}