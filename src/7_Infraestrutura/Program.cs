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
            // ConsultarDepartamentos();

            // DadosSensiveis();

            // HabilitandoBatchSize();

            TempoComandoGeral();
        }
        static void ConsultarDepartamentos()
        {
            using var db = new ApplicationContext();

            var departamentos = db.Departamentos.Where(p => p.Id >= 0).ToArray();
        }

        static void DadosSensiveis()
        {
            using var db = new ApplicationContext();

            var descricao = "Departamento";
            var departamentos = db.Departamentos.Where(p => p.Descricao == descricao).ToArray();
        }

        static void HabilitandoBatchSize()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            for (int i = 0; i < 50; i++)
                db.Departamentos.Add(new Curso.Domain.Departamento { Descricao = $"Departamento {i}"});

            db.SaveChanges();
        }

        static void TempoComandoGeral()
        {
            using var db = new ApplicationContext();

            db.Database.SetCommandTimeout(10);

            db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:07' SELECT 1");
        }
    }
}
