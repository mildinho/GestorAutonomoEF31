﻿using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Infra.Data.Repositories
{
    public class EmpresaRepository : GenericoRepository<Empresa>, IEmpresaRepository
    {
        private readonly IConfiguration _conf;
        private readonly Context.DBContexto _context;

        public EmpresaRepository(Context.DBContexto context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

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