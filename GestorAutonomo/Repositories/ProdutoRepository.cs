using GestorAutonomo.Biblioteca.Exceptions;
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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public ProdutoRepository(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(Produto produto)
        {
            if (!await _context.Produto.AnyAsync(x => x.Id == produto.Id))
            {
                throw new NotFoundException("Código não encontrado");
            }
            try
            {
                produto = AjustarCampos(produto);
        
                    
                _context.Update(produto);
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
                Produto obj = await SelecionarPorCodigoAsync(Id);
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

        public async Task InserirAsync(Produto produto)
        {
            produto = AjustarCampos(produto);

           

            _context.Add(produto);
            await _context.SaveChangesAsync();
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

        public async Task<Produto> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.Produto.FirstOrDefaultAsync(obj => obj.Id == Id);
        }


     


        public Produto AjustarCampos(Produto produto)
        {
            return produto;
        }


    }


   

}