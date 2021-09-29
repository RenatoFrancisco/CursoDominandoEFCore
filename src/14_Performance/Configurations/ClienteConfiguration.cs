using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curso.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.OwnsOne(x => x.Endereco, end =>
            {
                end.Property(p => p.Bairro).HasColumnName("Bairro");

                // Se quiser criar uma tabela específica para endereço
                end.ToTable("Enderecos");
            });
        }
    }
}