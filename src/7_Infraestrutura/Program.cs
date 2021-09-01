using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Curso.Data;

namespace DominandoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // ConsultarDepartamentos();

            DadosSensiveis();
        }
        static void ConsultarDepartamentos()
        {
            using var db = new ApplicationContext();

            var departamentos = db.Departamentos.Where(p => p.Id >= 0).ToArray();
        }

        static void DadosSensiveis()
        {
            using var db = new ApplicationContext();

            var descricao = "Departamento";
            var departamentos = db.Departamentos.Where(p => p.Descricao == descricao).ToArray();
        }
    }
}
