using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Infra.Data.Repositories
{
    public class ParceiroRepository : GenericoRepository<Parceiro>, IParceiroRepository
    {
        private readonly IConfiguration _conf;
        private readonly Context.DBContexto _context;

        public ParceiroRepository(Context.DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

      


        public async Task<IPagedList<Parceiro>> ListarTodosRegistrosAsync(TipoParceiro tipo, int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.Parceiro.AsQueryable();

            if (tipo == TipoParceiro.Cliente)
                objConsulta = objConsulta.Where(a => a.Cliente == 1);
            else if (tipo == TipoParceiro.Fornecedor)
                objConsulta = objConsulta.Where(a => a.Fornecedor == 1);
            else if (tipo == TipoParceiro.Vendedor)
                objConsulta = objConsulta.Where(a => a.Vendedor == 1);



            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Nome.Contains(pesquisa.Trim()) || a.CNPJ_CPF.ToString().Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<Parceiro>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<Parceiro>> ListarTodosRegistrosAsync(TipoParceiro tipo)
        {
            var objConsulta = _context.Parceiro.AsQueryable();

            if (tipo == TipoParceiro.Cliente)
                objConsulta = objConsulta.Where(a => a.Cliente == 1);
            else if (tipo == TipoParceiro.Fornecedor)
                objConsulta = objConsulta.Where(a => a.Fornecedor == 1);
            else if (tipo == TipoParceiro.Vendedor)
                objConsulta = objConsulta.Where(a => a.Vendedor == 1);

            return await objConsulta.ToListAsync();



        }

        public async Task<Parceiro> SelecionarPorCNPJ_CPFAsync(double documento)
        {
            return await _context.Parceiro.FirstOrDefaultAsync(obj => obj.CNPJ_CPF == documento);
        }



        public Parceiro AjustarCampos(Parceiro parceiro)
        {
            return parceiro;
        }


    }




}