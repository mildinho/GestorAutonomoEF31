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
    public class ParceiroRepository : IParceiroRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public ParceiroRepository(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(Parceiro parceiro)
        {
            if (!await _context.Parceiro.AnyAsync(x => x.Id == parceiro.Id))
            {
                throw new NotFoundException("Código não encontrado");
            }
            try
            {
                parceiro = AjustarCampos(parceiro);


                //_context.Update(parceiro);
                _context.Entry(parceiro).State = EntityState.Modified;
                _context.Entry(parceiro).Property(p => p.Data_Cadastro).IsModified = false;

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
                Parceiro obj = await SelecionarPorCodigoAsync(Id);
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

        public async Task InserirAsync(Parceiro parceiro)
        {
            parceiro = AjustarCampos(parceiro);

           

            _context.Add(parceiro);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<Parceiro>> ListarTodosRegistrosAsync(TipoParceiro tipo, int? pagina, string pesquisa)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            var objConsulta = _context.Parceiro.AsQueryable();

            if (tipo == TipoParceiro.Cliente)
                objConsulta = objConsulta.Where(a => a.Cliente == 1 );
            else if (tipo == TipoParceiro.Fornecedor)
                objConsulta = objConsulta.Where(a => a.Fornecedor == 1);
            else if (tipo == TipoParceiro.Vendedor)
                objConsulta = objConsulta.Where(a => a.Vendedor == 1);



            if (!string.IsNullOrEmpty(pesquisa))
            {
                objConsulta = objConsulta.Where(a => a.Nome.Contains(pesquisa.Trim()) || a.CNPJ_CPF.ToString().Contains(pesquisa.Trim()) );
            }


            return await objConsulta.ToPagedListAsync<Parceiro>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<Parceiro>> ListarTodosRegistrosAsync(TipoParceiro tipo)
        {
            var objConsulta = _context.Parceiro.AsQueryable();
            
            if (tipo == TipoParceiro.Cliente) 
                objConsulta = objConsulta.Where(a => a.Cliente == 1);
            else if (tipo == TipoParceiro.Fornecedor)
                    objConsulta = objConsulta.Where(a => a.Fornecedor == 1);
            else if (tipo == TipoParceiro.Vendedor)
                objConsulta = objConsulta.Where(a => a.Vendedor == 1);

            return await objConsulta.ToListAsync();

           

        }

        public async Task<Parceiro> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.Parceiro.FirstOrDefaultAsync(obj => obj.Id == Id);
        }


        public async Task<Parceiro> SelecionarPorCNPJ_CPFAsync(double documento)
        {
            return await _context.Parceiro.FirstOrDefaultAsync(obj => obj.CNPJ_CPF == documento);
        }



        public Parceiro AjustarCampos(Parceiro parceiro)
        {
            return parceiro;
        }


    }


   

}