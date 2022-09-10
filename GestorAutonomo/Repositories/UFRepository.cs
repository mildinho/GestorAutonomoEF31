using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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