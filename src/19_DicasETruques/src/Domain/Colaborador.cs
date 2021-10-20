namespace src.Domain
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
    }
}