using System;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Conversor> Conversores { get; set; }

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

            // modelBuilder.Entity<Estado>().HasData(new[] 
            // {
            //     new Estado { Id = 1, Nome = "São Paulo" },
            //     new Estado { Id = 2, Nome = "Sergipe" }
            // });

            // modelBuilder.HasDefaultSchema("cadastros");
            // modelBuilder.Entity<Estado>().ToTable("Estados", "SegundoEsquema");

            var conversao = new ValueConverter<Versao, string>(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));
            var conversao1 = new EnumToStringConverter<Versao>();

            modelBuilder.Entity<Conversor>()
                .Property(p =>  p.Versao)
                .HasConversion(conversao1);
                // .HasConversion(conversao);
                // .HasConversion(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));
                // .HasConversion<string>();

            modelBuilder.Entity<Conversor>()
                .Property(p => p.Status)
                .HasConversion(new Curso.Conversores.ConversorCustomizado());
        }
    }
}