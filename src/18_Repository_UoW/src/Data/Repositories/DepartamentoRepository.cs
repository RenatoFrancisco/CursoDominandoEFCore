using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Domain;

namespace src.Data.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Departamento> _dbSet;

        public DepartamentoRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<Departamento>();
        }

        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(d => d.Colaboradores)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public void Add(Departamento departamento)
        {
            _dbSet.Add(departamento);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}