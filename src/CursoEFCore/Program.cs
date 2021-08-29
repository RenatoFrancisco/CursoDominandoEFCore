using System;
using System.Diagnostics;
using CursoEFCore.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CursoEFCore.Domain;

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

            // MigracoesPendentes();

            // AplicarMigracoesEmTempoDeExecucao();

            // TodasMigracoes();

            // MigracoesJaAplicadas();

            // ScriptGeralBancoDeDados();

            // CarregamentoAdiantado();

            // CarregamentoExplicito();

            CarregamentoLento();
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

        static void TodasMigracoes()
        {
            using var db = new ApplicationContext();

            var migracoes = db.Database.GetMigrations();
            Console.WriteLine($"Total: {migracoes.Count()}");

            foreach (var migracao in migracoes)
                Console.WriteLine($"Migração: {migracao}");
        }

        static void MigracoesJaAplicadas()
        {
            using var db = new ApplicationContext();

            var migracoes = db.Database.GetAppliedMigrations();
            Console.WriteLine($"Total: {migracoes.Count()}");

            foreach (var migracao in migracoes)
                Console.WriteLine($"Migração: {migracao}");
        }

        static void ScriptGeralBancoDeDados()
        {
            using var db = new ApplicationContext();
            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void CarregamentoAdiantado()
        {
            using var db = new ApplicationContext();
            SetupTipoCarregamento(db);

            var departamentos = db
                .Departamentos
                .Include(p => p.Funcionarios);

            foreach (var departamento in departamentos)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                    foreach (var funcionario in departamento.Funcionarios)
                        Console.WriteLine($"\tFuncionário: {funcionario.Nome}");
                else
                    Console.WriteLine($"\tNenhum funcionário encontrado!");
            }
        }

        static void CarregamentoExplicito()
        {
            using var db = new ApplicationContext();
            SetupTipoCarregamento(db);

            var departamentos = db
                .Departamentos
                .ToList();

            foreach (var departamento in departamentos)
            {
                if (departamento.Id == 2)
                {
                    // db.Entry(departamento).Collection(p => p.Funcionarios).Load();
                    db.Entry(departamento)
                        .Collection(p => p.Funcionarios)
                        .Query()
                        .Where(p => p.Id > 2)
                        .ToList();
                }

                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                    foreach (var funcionario in departamento.Funcionarios)
                        Console.WriteLine($"\tFuncionário: {funcionario.Nome}");
                else
                    Console.WriteLine($"\tNenhum funcionário encontrado!");
            }
        }

        static void CarregamentoLento()
        {
            using var db = new ApplicationContext();
            SetupTipoCarregamento(db);

            // db.ChangeTracker.LazyLoadingEnabled = false;

            var departamentos = db
                .Departamentos
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                    foreach (var funcionario in departamento.Funcionarios)
                        Console.WriteLine($"\tFuncionário: {funcionario.Nome}");
                else
                    Console.WriteLine($"\tNenhum funcionário encontrado!");
            }
        }

        private static void SetupTipoCarregamento(ApplicationContext db)
        {
            if (!db.Departamentos.Any())
            {
                db.Departamentos.AddRange(
                    new Departamento
                    {
                        Descricao = "Departamento 01",
                        Funcionarios = new List<Funcionario>
                        {
                               new Funcionario
                               {
                                   Nome = "Rafael Almeida",
                                   CPF = "99999999911",
                                   RG = "2100062"
                               }
                        }
                    },
                    new Departamento
                    {
                        Descricao = "Departamento 02",
                        Funcionarios = new List<Funcionario>
                        {
                               new Funcionario
                               {
                                   Nome = "Bruno Brito",
                                   CPF = "88888888811",
                                   RG = "3100062"
                               },
                               new Funcionario
                               {
                                   Nome = "Eduardo Pires",
                                   CPF = "77777777711",
                                   RG = "1100062"
                               },
                        }
                    }
                );
                
                db.SaveChanges();
                db.ChangeTracker.Clear();
            }

        }


    }
}
