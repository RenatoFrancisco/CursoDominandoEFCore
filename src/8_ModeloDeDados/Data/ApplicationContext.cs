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
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection="Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO04;Integrated Security=true;pooling=true;";
            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
            // RENATO => renato
            // João => Joao

            modelBuilder
                .Entity<Departamento>()
                .Property(p => p.Descricao)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");

            modelBuilder
                .HasSequence<int>("MinhaSequencia", "sequencias");
                // .StartAt(1)
                // .IncrementsBy(1)
                // .HasMin(1)
                // .HasMax(10)
                // .IsCyclic();

            modelBuilder
                .Entity<Departamento>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEXT VALUE FOR sequencias.MinhaSequencia");
        }
    }
}