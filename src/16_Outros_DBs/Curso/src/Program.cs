using System;
using System.Linq;
using Curso.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Pessoas.Add(new Curso.Domain.Pessoa
            {
                Nome = "Teste",
                Telefone = "999999"
            });

            db.Pessoas.ToList();
            
            Console.WriteLine("Hello World!");
        }
    }
}
