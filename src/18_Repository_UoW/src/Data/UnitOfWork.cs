using src.Data.Repositories;

namespace src.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context) => _context = context;

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        private IDepartamentoRepository _departamentoRepository;
        public IDepartamentoRepository DepartamentoRepository
        {
            get => _departamentoRepository ?? (_departamentoRepository = new DepartamentoRepository(_context));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}