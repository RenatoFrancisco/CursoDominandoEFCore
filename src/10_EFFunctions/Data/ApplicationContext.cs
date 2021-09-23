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
        }
    }
}