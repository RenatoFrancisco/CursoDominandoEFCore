using System;
using Microsoft.EntityFrameworkCore;
using tests.Data;
using tests.Entities;
using Xunit;

namespace EFCore.Tests
{
    public class InMemoryText
    {
        [Fact]
        public void Deve_inserir_um_deparamento()
        {
            // Arrange
            var departamento = new Departamento
            {
                Descricao = "Tecnologia",
                DataCadastro = DateTime.Now
            };

            // Setup
            var context = CreateContext();
            context.Departamentos.Add(departamento);

            // Act
            var inseridos = context.SaveChanges();

            // Assert
            Assert.Equal(1, inseridos);
        }   

        private ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .Options;

            return new ApplicationContext(options);
        }
    }
}
