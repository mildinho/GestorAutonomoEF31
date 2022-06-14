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
    public class CategoriaProdutoRepository : GenericoRepository<CategoriaProduto>, ICategoriaProdutoRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public CategoriaProdutoRepository(GestorAutonomoContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(CategoriaProduto categoria)
        {
            if (!await _context.CategoriaProduto.AnyAsync(x => x.Id == categoria.Id))
            {
                throw new NotFoundException("Codigo nao encontrado");
            }
            try
            {
                categoria = AjustarCampos(categoria);
                //_context.Update(categoria);

                _context.Entry(categoria).State = EntityState.Modified;
                _context.Entry(categoria).Property(p => p.Data_Cadastro).IsModified = false;

                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }

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

        public async Task<List<CategoriaProduto>> ObterCategoriasPorCategoriaPai(int? Id)
        {
            return await _context.CategoriaProduto.Where(obj => obj.CategoriaPaiId == Id).ToListAsync();
        }

        public async Task<List<Produto>> ObterProdutosPorCategoria(int Id)
        {
            return await _context.Produto.Where(obj => obj.CategoriaProdutoId == Id).ToListAsync();
        }

      
    }

}