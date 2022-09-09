using GestorAutonomo.Data;
using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Servico.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Servico
{
    public class GenericoServico<Tabela> : IGenericoServico<Tabela> where Tabela : ModelBase
    {
        private readonly GestorAutonomoContext _context;


        public GenericoServico(GestorAutonomoContext context)
        {
            _context = context;

        }


        public virtual async Task<Tabela> BuscarPorCodigoAsync(Guid Id)
        {
            return await _context.Set<Tabela>().FindAsync(Id);

        }


        public virtual async Task<IEnumerable<Tabela>> TodosRegistrosPorEmpresaAsync(Guid EmpresaId)
        {
            IQueryable<Tabela> objConsulta = _context.
                Set<Tabela>().
                AsQueryable().
                Where(e => e.EmpresaId == EmpresaId);

            return await objConsulta.ToListAsync();

        }
    }
}
