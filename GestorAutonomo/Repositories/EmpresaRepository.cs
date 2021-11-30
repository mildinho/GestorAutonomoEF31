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
    public class EmpresaRepository : IEmpresaRepository
    {
        private IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public EmpresaRepository(GestorAutonomoContext context, IConfiguration configuration)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(Empresa empresa)
        {
            if (!await _context.Empresa.AnyAsync(x => x.Id == empresa.Id))
            {
                throw new NotFoundException("Codigo nao encontrado");
            }
            try
            {
                empresa = AjustarCampos(empresa);
                _context.Update(empresa);
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
                Empresa obj = await SelecionarPorCodigoAsync(Id);
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

        public async Task InserirAsync(Empresa empresa)
        {
            empresa = AjustarCampos(empresa);
            _context.Add(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<Empresa>> ListarTodosRegistrosAsync(int? pagina)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            return await _context.Empresa.ToPagedListAsync<Empresa>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<Empresa>> ListarTodosRegistrosAsync()
        {
            return await _context.Empresa.ToListAsync();

        }

        public async Task<Empresa> SelecionarPorCodigoAsync(int? Id)
        {
            return await _context.Empresa.FirstOrDefaultAsync(obj => obj.Id == Id);
        }

        public Empresa AjustarCampos(Empresa empresa)
        {
            if (!String.IsNullOrEmpty(empresa.CEP))
            {
                empresa.CEP = String.Join("", System.Text.RegularExpressions.Regex.Split(empresa.CEP, @"[^\d]"));
            }

            if (!String.IsNullOrEmpty(empresa.TelefoneComercial))
            {
                empresa.TelefoneComercial = String.Join("", System.Text.RegularExpressions.Regex.Split(empresa.TelefoneComercial, @"[^\d]"));
            }

            if (!String.IsNullOrEmpty(empresa.TelefoneFinanceiro))
            {
                empresa.TelefoneFinanceiro = String.Join("", System.Text.RegularExpressions.Regex.Split(empresa.TelefoneFinanceiro, @"[^\d]"));
            }

            if (!String.IsNullOrEmpty(empresa.TelefonePrincipal))
            {
                empresa.TelefonePrincipal = String.Join("", System.Text.RegularExpressions.Regex.Split(empresa.TelefonePrincipal, @"[^\d]"));
            }

            return empresa;
        }
    }

}