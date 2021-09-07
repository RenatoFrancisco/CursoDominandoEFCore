using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Curso.Data;
using Curso.Domain;

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
            // ConversorCustomizado();
            TrabalhandoComPropriedadeDeSombra();
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

            db.Conversores.Add(new Conversor { Status = Status.Devolvido });
            db.SaveChanges();

            var conversorEmAnalise = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Status.Analise);
            var conversorDevolvido = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Status.Devolvido);
        }

        static void TrabalhandoComPropriedadeDeSombra()
        {
            using var db = new ApplicationContext();
            // db.Database.EnsureDeleted();
            // db.Database.EnsureCreated();

            // var departamento = new Departamento
            // {
            //     Descricao = "Departamento Propriedade de Sombra"
            // };

            // db.Departamentos.Add(departamento);

            // db.Entry(departamento).Property("UltimaAtualizacao").CurrentValue = DateTime.Now;

            // db.SaveChanges();

            var departamento = db.Departamentos.Where(p => EF.Property<DateTime>(p, "UltimaAtualizacao") < DateTime.Now).ToArray();
        }
    }
}
