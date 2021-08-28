using System;
using System.Diagnostics;
using CursoEFCore.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // EnsureCreatedAndDeleted();
            // GepDoEnsureCreated();
            // HealthCheckDatabase();

            // _count = 0;
            // GerenciarEstadoDaConexao(false);

            // _count = 0;
            // GerenciarEstadoDaConexao(true);

            MigracoesPendentes();

            AplicarMigracoesEmTempoDeExecucao();
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

        static int _count;
        static void GerenciarEstadoDaConexao(bool gerenciaEstadoConexao)
        {
            using var db = new ApplicationContext();

            var time = Stopwatch.StartNew();

            var connection = db.Database.GetDbConnection();
            connection.StateChange += (_, _) => ++_count;

            if (gerenciaEstadoConexao)
                connection.Open();

            for (var i = 0; i < 200; i++)
                db.Departamentos.AsNoTracking().Any();

            time.Stop();

            var message = $"Tempo: {time.Elapsed}, {gerenciaEstadoConexao}, contador: {_count}";
            Console.WriteLine(message);
        }

        static void ExecuteSQL()
        {
            using var db = new ApplicationContext();

            // Primeira Opção
            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT 1";
                cmd.ExecuteNonQuery();
            }

            // Segunda Opção (Evita ataques de Sql Injection)
            var descricao = "TESTE";
            db.Database.ExecuteSqlRaw("update departamentos set descricao={0} where id=1", descricao);

            // Terceira Opção (Evita ataques de Sql Injection)
            db.Database.ExecuteSqlInterpolated($"update departamentos set descricao={descricao} where id=1");
        }

        static void MigracoesPendentes()
        {
            // Para trabalhar com migrations é necessário instalar o pacote Microsoft.EntityFrameworkCore.Design
            using var db = new ApplicationContext();

            var migracoesPendentes = db.Database.GetPendingMigrations();
            Console.WriteLine($"Total: {migracoesPendentes.Count()}");

            foreach (var migracao in migracoesPendentes)
                Console.WriteLine($"Migração: {migracao}");
        }

        static void AplicarMigracoesEmTempoDeExecucao()
        {
            using var db = new ApplicationContext();
            db.Database.Migrate();
        }
    }
}
