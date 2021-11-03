using GestorAutonomo.Biblioteca.Exceptions;
using GestorAutonomo.Data;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories
{
    public class UFRepository : IUFRepository
    {
        private IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public UFRepository(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(UF uf)
        {
            if (!await _context.UF.AnyAsync(x => x.Id == uf.Id))
            {
                throw new NotFoundException("Codigo nao encontrado");
            }
            try
            {
                _context.Update(uf);
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }

        }

        public async Task DeletarAsync(int Id)
        {
            try
            {
                UF obj = await SelecionarPorCodigoAsync(Id);
                if (obj != null)
                {

                    _context.Remove(obj);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task InserirAsync(UF uf)
        {
            _context.Add(uf);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<UF>> ListarTodosRegistrosAsync(int? pagina)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            return await _context.UF.ToPagedListAsync<UF>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<UF>> ListarTodosRegistrosAsync()
        {
            return await _context.UF.ToListAsync();

        }

        public async Task<UF> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.UF.FirstOrDefaultAsync(obj => obj.Id == Id);
        }
    }

}