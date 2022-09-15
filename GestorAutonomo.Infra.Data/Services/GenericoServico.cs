using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.ServiceInterfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Infra.Data.Services
{
    public class GenericoServico<Tabela> : IGenericoServico<Tabela> where Tabela : ModelBase
    {
        private readonly DBContexto _context;
        internal DbSet<Tabela> _dbSet;

        public GenericoServico(DBContexto context)
        {
            _context = context;
            _dbSet = context.Set<Tabela>();
        }


        public virtual async Task<Tabela> BuscarPorCodigoAsync(Guid Id)
        {
            return await _dbSet.FindAsync(Id);

        }


        public virtual async Task<IEnumerable<Tabela>> TodosRegistrosPorEmpresaAsync(Guid EmpresaId)
        {
            IQueryable<Tabela> objConsulta = _dbSet.AsQueryable().
                Where(e => e.EmpresaId == EmpresaId);

            return await objConsulta.ToListAsync();

        }
    }
}
