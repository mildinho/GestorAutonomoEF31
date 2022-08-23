using GestorAutonomo.Data;
using GestorAutonomo.Servico.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Servico
{
    public class GenericoServico<Tabela> : IGenericoServico<Tabela> where Tabela : class
    {
        private readonly GestorAutonomoContext _context;


        public GenericoServico(GestorAutonomoContext context)
        {
            _context = context;

        }


        public virtual async Task<Tabela> SelecionarPorCodigoAsync(Guid Id)
        {
            return await _context.Set<Tabela>().FindAsync(Id);

        }


        public virtual async Task<IEnumerable<Tabela>> ListarTodosRegistrosAsync(Guid EmpresaId)
        {
            IQueryable<Tabela> objConsulta = _context.Set<Tabela>().AsQueryable();

            IQueryable<Tabela> objConsulta2 = objConsulta
            .Where(e => <objConsulta>.EmpresaId == EmpresaId);
            //_context.Entry<Tabela>(tabela).Property("Data_Cadastro").IsModified = false;

            return await objConsulta.ToListAsync();

        }
    }
}
