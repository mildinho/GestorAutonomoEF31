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
    public class DuplicataRepository : IDuplicataRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public DuplicataRepository(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(Duplicata duplicata)
        {
            if (!await _context.Duplicatas.AnyAsync(x => x.Id == duplicata.Id))
            {
                throw new NotFoundException("Código não encontrado");
            }
            try
            {
                duplicata = AjustarCampos(duplicata);


                //_context.Update(produto);
                _context.Entry(duplicata).State = EntityState.Modified;
                _context.Entry(duplicata).Property(p => p.Data_Cadastro).IsModified = false;

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
                Duplicata obj = await SelecionarPorCodigoAsync(Id);
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

        public async Task InserirAsync(Duplicata duplicata)
        {
            duplicata = AjustarCampos(duplicata);

           

            _context.Add(duplicata);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<Duplicata>> ListarTodosRegistrosAsync( int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.Duplicatas.AsQueryable();

            
            if (!string.IsNullOrEmpty(pesquisa))
            {
                //objConsulta = objConsulta.Where(a => a.Descricao.Contains(pesquisa.Trim()));
            }


            return await objConsulta.ToPagedListAsync<Duplicata>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<Duplicata>> ListarTodosRegistrosAsync()
        {
            var objConsulta = _context.Duplicatas.AsQueryable();
            
         
            return await objConsulta.ToListAsync();

           

        }

        public async Task<Duplicata> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.Duplicatas.FirstOrDefaultAsync(obj => obj.Id == Id);
        }


     


        public Duplicata AjustarCampos(Duplicata duplicata)
        {
            return duplicata;
        }

      
    }


   

}