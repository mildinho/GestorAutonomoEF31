using GestorAutonomo.Data;
using GestorAutonomo.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestorAutonomoContext _context;

        public UnitOfWork(GestorAutonomoContext context)
        {
            _context = context;


        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


        void IDisposable.Dispose()
        {
            _context.Dispose();
        }
    }
}
