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
    public class EmpresaRepository : GenericoRepository<Empresa>, IEmpresaRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public EmpresaRepository(GestorAutonomoContext context, IConfiguration configuration) : base(context)
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
                //_context.Update(empresa);

                _context.Entry(empresa).Property(p => p.Data_Cadastro).IsModified = false;
                _context.Entry(empresa).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }

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