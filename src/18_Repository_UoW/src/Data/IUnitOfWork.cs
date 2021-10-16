using System;

namespace src.Data
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}