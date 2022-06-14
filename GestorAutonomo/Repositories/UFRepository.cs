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
    public class UFRepository : GenericoRepository<UF>, IUFRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public UFRepository(GestorAutonomoContext context, IConfiguration configuration) : base(context)
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
                //_context.Update(uf);
                _context.Entry(uf).State = EntityState.Modified;
                _context.Entry(uf).Property(p => p.Data_Cadastro).IsModified = false;

                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }

        }

       public async Task<IPagedList<UF>> ListarTodosRegistrosAsync(int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.UF.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Descricao.Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<UF>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<UF>> ListarTodosRegistrosAsync()
        {
            return await _context.UF.ToListAsync();

        }

   
    }

}