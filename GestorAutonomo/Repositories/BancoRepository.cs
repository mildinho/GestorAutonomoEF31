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
    public class BancoRepository : IBancoRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public BancoRepository(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(Banco banco)
        {
            if (!await _context.Banco.AnyAsync(x => x.Id == banco.Id))
            {
                throw new NotFoundException("Codigo nao encontrado");
            }
            try

            {
                //_context.Update(banco);
                _context.Entry(banco).State = EntityState.Modified;
                _context.Entry(banco).Property(p => p.Data_Cadastro).IsModified = false;
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
                Banco obj = await SelecionarPorCodigoAsync(Id);
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

        public async Task InserirAsync(Banco banco)
        {
            _context.Add(banco);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<Banco>> ListarTodosRegistrosAsync(int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.Banco.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Descricao.Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<Banco>(numeroPagina, RegistroPorPagina);

        }


        public async Task<IEnumerable<Banco>> ListarTodosRegistrosAsync()
        {
            return await _context.Banco.ToListAsync();

        }

        public async Task<Banco> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.Banco.FirstOrDefaultAsync(obj => obj.Id == Id);
        }
    }

}