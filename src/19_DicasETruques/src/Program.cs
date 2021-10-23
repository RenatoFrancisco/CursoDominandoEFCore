using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using src.Data;

namespace EFCore.Tips
{
    class Program
    {
        static void Main(string[] args)
        {
            // ToQueryString();
            DebugView();
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
    }
}
