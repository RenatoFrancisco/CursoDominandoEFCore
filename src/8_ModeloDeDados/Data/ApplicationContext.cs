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
        public DbSet<Estado> Estados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection="Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO05;Integrated Security=true;pooling=true;";
            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
            // // RENATO => renato
            // // João => Joao

            // modelBuilder
            //     .Entity<Departamento>()
            //     .Property(p => p.Descricao)
            //     .UseCollation("SQL_Latin1_General_CP1_CS_AS");

            // modelBuilder
            //     .HasSequence<int>("MinhaSequencia", "sequencias");
            //     // .StartAt(1)
            //     // .IncrementsBy(1)
            //     // .HasMin(1)
            //     // .HasMax(10)
            //     // .IsCyclic();

            // modelBuilder
            //     .Entity<Departamento>()
            //     .Property(p => p.Id)
            //     .HasDefaultValueSql("NEXT VALUE FOR sequencias.MinhaSequencia");

            // modelBuilder
            //     .Entity<Departamento>()
            //     .HasIndex(p => new { p.Descricao, p.Ativo })
            //     .HasDatabaseName("idx_meu_indice_composto")
            //     .HasFilter("Descricao IS NOT NULL")
            //     .HasFillFactor(80)
            //     .IsUnique();

            modelBuilder.Entity<Estado>().HasData(new[] 
            {
                new Estado { Id = 1, Nome = "São Paulo" },
                new Estado { Id = 2, Nome = "Sergipe" }
            });
        }
    }
}