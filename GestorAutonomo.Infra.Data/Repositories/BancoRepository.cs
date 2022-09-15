using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Infra.Data.Repositories
{
    public class BancoRepository : GenericoRepository<Banco>,  IBancoRepository
    {
        private readonly IConfiguration _conf;
        private readonly Context.DBContexto _context;

        public BancoRepository(Context.DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

       

        public async Task<IPagedList<Banco>> ListarTodosRegistrosAsync(int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

           
            var objConsulta = _context.Banco.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Descricao.Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<Banco>(numeroPagina, RegistroPorPagina);

        }


       
    }

}