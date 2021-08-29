using System.Collections.Generic;

namespace CursoEFCore.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        // Virtual para LazyLoad
        public virtual List<Funcionario> Funcionarios { get; set; }
    }   
}