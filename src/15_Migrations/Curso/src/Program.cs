using System;
using Curso.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();

            var migracoes = db.Database.GetPendingMigrations();
            foreach (var migracao in migracoes)
                Console.WriteLine(migracao);

            Console.WriteLine("Hello World!");
        }
    }
}
