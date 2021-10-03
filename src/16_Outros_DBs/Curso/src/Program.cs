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

            db.Database.EnsureCreated();
            var pessoas = db.Pessoas.ToList();
            
            Console.WriteLine("Hello World!");
        }
    }
}
