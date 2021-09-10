using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Curso.Data;
using Curso.Domain;
using System.Text.Json;

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
            // TrabalhandoComPropriedadeDeSombra();
            // TiposDePropriedades();
            // Relacionamento1Para1();
            Relacionamento1ParaMuitos();
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

        static void TiposDePropriedades()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var cliente = new Cliente
            {
                Nome = "Fulano de Tal",
                Telefone = "(79) 98888-7777",
                Endereco = new Endereco { Bairro = "Centro", Cidade = "São Paulo" }
            };

            db.Clientes.Add(cliente);
            db.SaveChanges();

            var clientes = db.Clientes.AsNoTracking().ToList();
            var options = new JsonSerializerOptions { WriteIndented = true };

            clientes.ForEach(c =>
            {
                var json = JsonSerializer.Serialize(c, options);
                Console.WriteLine(json);
            });
        }

        static void Relacionamento1Para1()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var estado = new Estado
            {
                Nome = "Sergipe",
                Governador = new Governador { Nome = "Rafael de Almeida" }
            };

            db.Estados.Add(estado);
            db.SaveChanges();

            var estados = db.Estados.AsNoTracking().ToList();

            estados.ForEach(e => Console.WriteLine($"Estado: {e.Nome}, Governador: {e.Governador.Nome}"));
        }

        static void Relacionamento1ParaMuitos()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var estado = new Estado
                {
                    Nome = "Sergipe",
                    Governador = new Governador { Nome = "Rafael de Almeida" }
                };

                estado.Cidades.Add(new Cidade { Nome = "Itabaiana" });
                db.Estados.Add(estado);
                db.SaveChanges();
            }

            using (var db = new ApplicationContext())
            {
                var estados = db.Estados.ToList();
                estados[0].Cidades.Add(new Cidade { Nome = "Aracaju" });
                db.SaveChanges();

                foreach (var est in db.Estados.Include(e => e.Cidades).AsNoTracking())
                {
                    Console.WriteLine($"Estado: {est.Nome}, Governador: {est.Governador.Nome}");

                    foreach (var cidade in est.Cidades)
                        Console.WriteLine($"\t Cidade: {cidade.Nome}");
                }
            }
        }
    }
}
