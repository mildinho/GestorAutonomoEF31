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
    public class ProdutoSaldoRepository : GenericoRepository<ProdutoSaldo>, IProdutoSaldoRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public ProdutoSaldoRepository(GestorAutonomoContext context, IConfiguration configuration) : base(context)
    {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(ProdutoSaldo produtoSaldo)
        {
            if (!await _context.ProdutoSaldo.AnyAsync(x => x.Id == produtoSaldo.Id))
            {
                throw new NotFoundException("Codigo nao encontrado");
            }
            try
            {
                produtoSaldo = AjustarCampos(produtoSaldo);
                //_context.Update(categoria);

                _context.Entry(produtoSaldo).State = EntityState.Modified;
                _context.Entry(produtoSaldo).Property(p => p.Data_Cadastro).IsModified = false;

                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }

        }


   
        public async Task<IPagedList<ProdutoSaldo>> ListarTodosRegistrosAsync(int? pagina)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.ProdutoSaldo.AsQueryable();
            


            return await objConsulta.ToPagedListAsync<ProdutoSaldo>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<ProdutoSaldo>> ListarTodosRegistrosAsync()
        {
            return await _context.ProdutoSaldo.ToListAsync();

        }

        public ProdutoSaldo AjustarCampos(ProdutoSaldo produtoSaldo)
        {
           

            return produtoSaldo;
        }
  

        public async Task<List<ProdutoSaldo>> ObterProdutosPorPonto(int Id, bool SomenteComSaldo )
        {
            if (SomenteComSaldo)
                return await _context.ProdutoSaldo.
                    Where(obj => obj.PontosEstoqueId == Id && obj.Saldo > 0).
                    ToListAsync();
        

            return await _context.ProdutoSaldo.Where(obj => obj.PontosEstoqueId == Id).ToListAsync();
        }

        public async Task<List<ProdutoSaldo>> ObterPontosPorProduto(int Id, bool SomenteComSaldo)
        {
            if (SomenteComSaldo)
                return await _context.ProdutoSaldo.
                    Where(obj => obj.PontosEstoqueId == Id && obj.Saldo > 0).
                    ToListAsync();


            return await _context.ProdutoSaldo.Where(obj => obj.ProdutoId == Id).ToListAsync();
        }
    }

}