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
    public class PontosEstoqueRepository : IPontosEstoqueRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public PontosEstoqueRepository(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(PontosEstoque pontos)
        {
            if (!await _context.PontosEstoque.AnyAsync(x => x.Id == pontos.Id))
            {
                throw new NotFoundException("Código não encontrado");
            }
            try
            {
                pontos = AjustarCampos(pontos);
        
                    
                _context.Update(pontos);
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }

        }

        public async Task DeletarAsync(int Id)
        {
            try
            {
                PontosEstoque obj = await SelecionarPorCodigoAsync(Id);
                if (obj != null)
                {

                    _context.Remove(obj);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task InserirAsync(PontosEstoque pontos)
        {
            pontos = AjustarCampos(pontos);

           

            _context.Add(pontos);
            await _context.SaveChangesAsync();
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

        public async Task<PontosEstoque> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.PontosEstoque.FirstOrDefaultAsync(obj => obj.Id == Id);
        }


     


        public PontosEstoque AjustarCampos(PontosEstoque pontos)
        {
            return pontos;
        }


    }


   

}