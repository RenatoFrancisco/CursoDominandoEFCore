using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Domain;

namespace src.Data.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApplicationContext _context;

        public DepartamentoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context
                .Departamentos
                .Include(d => d.Colaboradores)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public void Add(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}