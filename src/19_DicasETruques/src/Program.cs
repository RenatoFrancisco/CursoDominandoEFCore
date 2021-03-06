using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Domain;

namespace EFCore.Tips
{
    class Program
    {
        static void Main(string[] args)
        {
            // ToQueryString();
            // DebugView();
            // Clear();
            // ConsultaFiltrada();
            // SingleOrDefaultVsFirstOrDefault();
            // SemChavePrimaria();
            // NaoUnicode();
            // OperadoreDeAgregacao();
            // OperadoreDeAgregacaoNoAgrupamento();
            ContadorDeEventos();
        }

        static void ToQueryString()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            var query = db.Departamentos.Where(p => p.Id > 2);
            var sql = query.ToQueryString();

            Console.WriteLine(sql);
        }

        static void DebugView()
        {
            using var db = new ApplicationContext();
            db.Departamentos.Add(new src.Domain.Departamento { Descricao = "TESTE DebugView"});

            var query = db.Departamentos.Where(p => p.Id > 2);
        }

        static void Clear()
        {
            using var db = new ApplicationContext();
            db.Departamentos.Add(new src.Domain.Departamento { Descricao = "TESTE DebugView"});

            db.ChangeTracker.Clear();
        }

        static void ConsultaFiltrada()
        {
            using var db = new ApplicationContext();
            var sql = db.Departamentos
                .Include(p => p.Colaboradores.Where(c => c.Name.Contains("Teste")))
                .ToQueryString();

            Console.WriteLine(sql);
        }

        static void SingleOrDefaultVsFirstOrDefault()
        {
            using var db = new ApplicationContext();

            Console.WriteLine("SingleOrDefault");
            _ = db.Departamentos.SingleOrDefault(p => p.Id > 2);

            Console.WriteLine("FirtsOrDefault");
            _ = db.Departamentos.FirstOrDefault(p => p.Id > 2);
        }

        static void SemChavePrimaria()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var usuarioFuncoes = db.UsuarioFuncoes.Where(p => p.UsuarioId == Guid.NewGuid()).ToArray();
        }

        static void NaoUnicode()
        {
            using var db = new ApplicationContext();
            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void OperadoreDeAgregacao()
        {
            using var db = new ApplicationContext();

            var sql = db.Departamentos
                .GroupBy(p => p.Descricao)
                .Select(p => 
                    new 
                    {
                        Descricao = p.Key,
                        Contador = p.Count(),
                        Media = p.Average(p => p.Id),
                        Max = p.Max(p => p.Id),
                        Soma = p.Sum(p => p.Id)
                    }).ToQueryString();

            Console.WriteLine(sql);
        }

        static void OperadoreDeAgregacaoNoAgrupamento()
        {
            using var db = new ApplicationContext();

            var sql = db.Departamentos
                .GroupBy(p => p.Descricao)
                .Where(p => p.Count() > 1)
                .Select(p => 
                    new 
                    {
                        Descricao = p.Key,
                        Contador = p.Count(),
                    }).ToQueryString();

            Console.WriteLine(sql);
        }

        static void ContadorDeEventos()
        {
            // dotnet tool install --global dotnet-counters
            // dotnet counters monitor -p 47306 --counters Microsoft.EntityFrameworkCore
            // Onde -p é o id do processo do EntityFrameworkCore

            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.WriteLine($"PID: {Process.GetCurrentProcess().Id}");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                var deparatamento = new Departamento
                {
                    Descricao = "Departamento sem colaborador"
                };

                db.Departamentos.Add(deparatamento);
                db.SaveChanges();

                _ = db.Departamentos.Find(1);
                _ = db.Departamentos.AsNoTracking().FirstOrDefault();
            }   
        }
    }
}
