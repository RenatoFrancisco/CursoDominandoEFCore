using System;
using System.Linq;
using Curso.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Curso.Conversores
{
    public class ConversorCustomizado : ValueConverter<Status, string>
    {
        public ConversorCustomizado() 
            : base(p => ConverterParaBancoDeDados(p), value => ConveterParaAplicacao(value), new ConverterMappingHints(1)) { }
                                             
        static string ConverterParaBancoDeDados(Status status)
        {
            return status.ToString()[0..1];
        }

        static Status ConveterParaAplicacao(string value)
        {
            var status = Enum
                .GetValues<Status>()
                .FirstOrDefault(p => p.ToString()[0..1] == value);

            return status; 
        }
    }
}