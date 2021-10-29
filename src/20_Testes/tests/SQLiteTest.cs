using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using tests.Data;
using tests.Entities;
using Xunit;

namespace EFCore.Tests
{
    public class SQLiteTest
    {
        [Theory]
        [InlineData("Tecnologia")]
        [InlineData("Financeiro")]
        [InlineData("Departamento Pessoal")]
        public void Deve_inserir_um_deparamento(string descricao)
        {
            // Arrange
            var departamento = new Departamento
            {
                Descricao = descricao,
                DataCadastro = DateTime.Now
            };

            // Setup
            var context = CreateContext();
            context.Database.EnsureCreated();
            
            context.Departamentos.Add(departamento);

            // Act
            var inseridos = context.SaveChanges();

            // Assert
            Assert.Equal(1, inseridos);
        }   

        private ApplicationContext CreateContext()
        {
            var conexao = new SqliteConnection("Datasource=memory:");
            conexao.Open();

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(conexao)
                .Options;

            return new ApplicationContext(options);
        }
    }
}
