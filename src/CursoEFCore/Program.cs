using System;
using CursoEFCore.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // EnsureCreatedAndDeleted();
            // GepDoEnsureCreated();
            HealthCheckDatabase();
        }

        static void EnsureCreatedAndDeleted()
        {
            using var db = new ApplicationContext();
            // db.Database.EnsureCreated();
            db.Database.EnsureDeleted();
        }

        static void GepDoEnsureCreated()
        {
            using var db1 = new ApplicationContext();
            using var db2 = new ApplicationContextCidade();

            // Cria o banco e as tabelas Funcionarios e Departamentos
            db1.Database.EnsureCreated();

            // Não vai criar a tabela Cidades, porque o banco já possui tabelas criadas
            db2.Database.EnsureCreated();

            // Força a criação da tabela Cidades mesmo já existindo tabelas no banco
            var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();
            databaseCreator.CreateTables();
        }

        static void HealthCheckDatabase()
        {
            using var db = new ApplicationContext();
            var canConnect = db.Database.CanConnect();

            if (canConnect)
                Console.WriteLine("Posso me conectar");
            else
                Console.WriteLine("Não posso me conectar");
        }
    }
}
