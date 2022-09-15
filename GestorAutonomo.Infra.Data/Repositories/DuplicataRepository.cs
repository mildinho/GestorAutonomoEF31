using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Infra.Data.Repositories
{
    public class DuplicataRepository : GenericoRepository<Duplicata>, IDuplicataRepository
    {
        private readonly IConfiguration _conf;
        private readonly Context.DBContexto _context;

        public DuplicataRepository(Context.DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

     

        public async Task<IPagedList<Duplicata>> ListarTodosRegistrosAsync(TipoDuplicata Tipo, Guid IdParceiro, int? pagina)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.Duplicatas.Where(x => x.ParceiroId == IdParceiro).AsQueryable();

            return await objConsulta.ToPagedListAsync<Duplicata>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<Duplicata>> ListarTodosRegistrosAsync(TipoDuplicata Tipo, Guid IdParceiro)
        {
            var objConsulta = _context.Duplicatas.Where(x => x.ParceiroId == IdParceiro).AsQueryable();
            
         
            return await objConsulta.ToListAsync();

           

        }

        public Duplicata AjustarCampos(Duplicata duplicata)
        {
            return duplicata;
        }

      
    }


   

}