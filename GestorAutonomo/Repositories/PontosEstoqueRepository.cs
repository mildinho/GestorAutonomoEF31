﻿using GestorAutonomo.Biblioteca.Exceptions;
using GestorAutonomo.Data;
using GestorAutonomo.Models;
using GestorAutonomo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using System;

namespace GestorAutonomo.Repositories
{
    public class PontosEstoqueRepository : GenericoRepository<PontosEstoque>, IPontosEstoqueRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public PontosEstoqueRepository(GestorAutonomoContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }

       


        public async Task<IPagedList<PontosEstoque>> ListarTodosRegistrosAsync( int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.PontosEstoque.AsQueryable();

            
            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Descricao.Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<PontosEstoque>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<PontosEstoque>> ListarTodosRegistrosAsync()
        {
            var objConsulta = _context.PontosEstoque.AsQueryable();
            
         
            return await objConsulta.ToListAsync();

           

        }

     
        public PontosEstoque AjustarCampos(PontosEstoque pontos)
        {
            return pontos;
        }


    }


   

}