using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
        
    }
}
