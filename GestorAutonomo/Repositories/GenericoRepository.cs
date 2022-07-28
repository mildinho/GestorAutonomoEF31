using GestorAutonomo.Biblioteca.Exceptions;
using GestorAutonomo.Data;
using GestorAutonomo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorAutonomo.Repositories
{
    public class GenericoRepository<Tabela> : IGenericoRepository<Tabela> where Tabela : class
    {
        private readonly GestorAutonomoContext _context;


        public GenericoRepository(GestorAutonomoContext context)
        {
            _context = context;

        }

        public async Task InserirAsync(Tabela tabela)
        {
            await _context.Set<Tabela>().AddAsync(tabela);
            await _context.SaveChangesAsync();
        }



        public async Task AtualizarAsync(Tabela tabela)
        {
             _context.Entry<Tabela>(tabela).State = EntityState.Modified;
            _context.Entry<Tabela>(tabela).Property("Data_Cadastro").IsModified = false;
            await _context.SaveChangesAsync();
        }



        public async Task DeletarAsync(int Id)
        {
            try
            {
                var obj = await SelecionarPorCodigoAsync(Id);
                if (obj != null)
                {
                    _context.Set<Tabela>().Remove(obj);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }



        public async Task<Tabela> SelecionarPorCodigoAsync(int Id)
        {
            return await _context.Set<Tabela>().FindAsync(Id);
        }
    }
}
