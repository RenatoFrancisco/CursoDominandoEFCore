using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Curso.Data;

namespace DominandoEFCore
{
    class Program
    {

        static void Main(string[] args)
        {
            // Collations();
            // PropagarDados();
            // Esquema();
            // ConversorDeValor();
            ConversorCustomizado();
        }

        static void Collations()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        static void PropagarDados()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void Esquema()
        {
            using var db = new ApplicationContext();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void ConversorDeValor() => Esquema();

        static void ConversorCustomizado()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Conversores.Add(new Curso.Domain.Conversor { Status = Curso.Domain.Status.Devolvido });
            db.SaveChanges();

            var conversorEmAnalise = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Curso.Domain.Status.Analise);
            var conversorDevolvido = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Curso.Domain.Status.Devolvido);
        }
    }
}
