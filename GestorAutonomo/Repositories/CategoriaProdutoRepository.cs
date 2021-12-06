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
    public class CategoriaProdutoRepository : ICategoriaProdutoRepository
    {
        private IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public CategoriaProdutoRepository(GestorAutonomoContext context, IConfiguration configuration)
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
                _context.Update(categoria);
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
                CategoriaProduto obj = await SelecionarPorCodigoAsync(Id);
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

        public async Task InserirAsync(CategoriaProduto categoria)
        {
            categoria = AjustarCampos(categoria);
            _context.Add(categoria);
            await _context.SaveChangesAsync();
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

        public async Task<CategoriaProduto> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.CategoriaProduto.FirstOrDefaultAsync(obj => obj.Id == Id);
        }

        public CategoriaProduto AjustarCampos(CategoriaProduto categoria)
        {
           

            return categoria;
        }
    }

}