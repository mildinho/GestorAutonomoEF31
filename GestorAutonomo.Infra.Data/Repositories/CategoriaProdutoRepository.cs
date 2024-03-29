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
    public class CategoriaProdutoRepository : GenericoRepository<CategoriaProduto>, ICategoriaProdutoRepository
    {
        private readonly IConfiguration _conf;
        private readonly Context.DBContexto _context;

        public CategoriaProdutoRepository(Context.DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

     

          public async Task<IPagedList<CategoriaProduto>> ListarTodosRegistrosAsync(int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.CategoriaProduto.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Descricao.Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<CategoriaProduto>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<CategoriaProduto>> ListarTodosRegistrosAsync()
        {
            return await _context.CategoriaProduto.ToListAsync();

        }

        public CategoriaProduto AjustarCampos(CategoriaProduto categoria)
        {
           

            return categoria;
        }

        public async Task<List<CategoriaProduto>> ObterCategoriasPorCategoriaPai(Guid? Id)
        {
            return await _context.CategoriaProduto.Where(obj => obj.CategoriaPaiId == Id).ToListAsync();
        }

        public async Task<List<Produto>> ObterProdutosPorCategoria(Guid Id)
        {
            return await _context.Produto.Where(obj => obj.CategoriaProdutoId == Id).ToListAsync();
        }

      
    }

}