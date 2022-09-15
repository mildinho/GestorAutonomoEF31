using GestorAutonomo.Domain.Biblioteca.Exceptions;
using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GestorAutonomo.Infra.Data.Repositories
{
    public class GenericoRepository<Tabela> : IGenericoRepository<Tabela> where Tabela : class
    {
        private readonly Context.DBContexto _context;
        internal DbSet<Tabela> _dbSet;


        public GenericoRepository(Context.DBContexto context)
        {
            _context = context;
            _dbSet = context.Set<Tabela>();
        }

        public virtual async Task InserirAsync(Tabela tabela)
        {
            await _dbSet.AddAsync(tabela);
        }



        public virtual async Task AtualizarAsync(Tabela tabela)
        {
            _context.Entry<Tabela>(tabela).State = EntityState.Modified;
            _context.Entry<Tabela>(tabela).Property("Data_Cadastro").IsModified = false;
            
        }



        public virtual async Task DeletarAsync(Guid Id)
        {
            try
            {
                var obj = await SelecionarPorCodigoAsync(Id);
                if (obj != null)
                {
                    _dbSet.Remove(obj);
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }



        public virtual async Task<Tabela> SelecionarPorCodigoAsync(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }



    }
}
