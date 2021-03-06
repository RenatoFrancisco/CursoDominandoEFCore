using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Curso.Configurations;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Livro> Livros {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO-02;Integrated Security=true;pooling=true;";

            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder
                .HasDbFunction(_minhaFuncao)
                .HasName("LEFT")
                .IsBuiltIn();

            modelBuilder
                .HasDbFunction(_letrasMaiusculas)
                .HasName("ConverterParaLetrasMaiusculas")
                .HasSchema("dbo");
        }

        private static MethodInfo _minhaFuncao = typeof(MinhasFuncoes)
            .GetRuntimeMethod("Left", new[] {typeof(string), typeof(int)});

        private static MethodInfo _letrasMaiusculas = typeof(MinhasFuncoes)
            .GetRuntimeMethod(nameof(MinhasFuncoes.LetrasMaiusculas), new[] {typeof(string)});
    }
}