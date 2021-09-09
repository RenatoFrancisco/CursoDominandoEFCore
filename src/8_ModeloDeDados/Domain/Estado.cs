namespace Curso.Domain
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Governador Governador { get; set; }
    }

    public class Governador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public int EstadoReference { get; set; }
        public Estado Estado { get; set; }
    }
}