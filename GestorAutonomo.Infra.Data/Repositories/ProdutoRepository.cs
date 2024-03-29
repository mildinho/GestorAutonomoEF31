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
    public class ProdutoRepository : GenericoRepository<Produto>, IProdutoRepository
    {
        private readonly IConfiguration _conf;
        private readonly Context.DBContexto _context;

        public ProdutoRepository(Context.DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

     


        public async Task<IPagedList<Produto>> ListarTodosRegistrosAsync( int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.Produto.AsQueryable();

            
            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Descricao.Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<Produto>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<Produto>> ListarTodosRegistrosAsync()
        {
            var objConsulta = _context.Produto.AsQueryable();
            
         
            return await objConsulta.ToListAsync();

           

        }

        public async Task<Produto> SelecionarPorCodigoAsync(Guid? Id)
        {
            return await _context.Produto.
                Include(obj => obj.Imagens).
                Include(obj => obj.ProdutoSaldo).
                ThenInclude(obj => obj.PontosEstoque).
                FirstOrDefaultAsync(obj => obj.Id == Id);

          
        }


     


        public Produto AjustarCampos(Produto produto)
        {
            return produto;
        }


    }


   

}