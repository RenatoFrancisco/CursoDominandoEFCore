using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Curso.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;

namespace DominandoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //ComportamentoPadrao();
            //GerenciandoTransacaoManualmente();
            //ReverterTransacao();
            TransactionScope();
        }    

        static void FuncaoLEFT()
        {
            CadastrarLivro();

            using var db = new Curso.Data.ApplicationContext(); 

            var resultado = db.Livros.Select(p=> Curso.Funcoes.MinhasFuncoes.Left(p.Titulo, 10));
            foreach(var parteTitulo in resultado)
            {
                Console.WriteLine(parteTitulo);
            }
        }
         
        static void CadastrarLivro()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Introdução ao Entity Framework Core",
                        Autor = "Rafael",
                        CadastradoEm = DateTime.Now.AddDays(-1)
                    }); 

                db.SaveChanges();
            }
        }
    }
}
