﻿using GestorAutonomo.Domain.Entities;
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
    public class ProdutoSaldoRepository : GenericoRepository<ProdutoSaldo>, IProdutoSaldoRepository
    {
        private readonly IConfiguration _conf;
        private readonly Context.DBContexto _context;

        public ProdutoSaldoRepository(Context.DBContexto context, IConfiguration configuration) : base(context)
    {
            _context = context;
            _conf = configuration;

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
  

        public async Task<List<ProdutoSaldo>> ObterProdutosPorPonto(Guid Id, bool SomenteComSaldo )
        {
            if (SomenteComSaldo)
                return await _context.ProdutoSaldo.
                    Where(obj => obj.PontosEstoqueId == Id && obj.Saldo > 0).
                    ToListAsync();
        

            return await _context.ProdutoSaldo.Where(obj => obj.PontosEstoqueId == Id).ToListAsync();
        }

        public async Task<List<ProdutoSaldo>> ObterPontosPorProduto(Guid Id, bool SomenteComSaldo)
        {
            if (SomenteComSaldo)
                return await _context.ProdutoSaldo.
                    Where(obj => obj.PontosEstoqueId == Id && obj.Saldo > 0).
                    ToListAsync();


            return await _context.ProdutoSaldo.Where(obj => obj.ProdutoId == Id).ToListAsync();
        }
    }

}