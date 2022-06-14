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
    public class LoginRepository : GenericoRepository<Login>, ILoginRepository
    {
        private readonly IConfiguration _conf;
        private readonly GestorAutonomoContext _context;

        public LoginRepository(GestorAutonomoContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _conf = configuration;

        }


        public async Task AtualizarAsync(Login login)
        {
            if (!await _context.Login.AnyAsync(x => x.Id == login.Id))
            {
                throw new NotFoundException("Codigo nao encontrado");
            }
            try
            {
                //_context.Update(login);
                _context.Entry(login).State = EntityState.Modified;
                _context.Entry(login).Property(p => p.Data_Cadastro).IsModified = false;

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
                Login obj = await SelecionarPorCodigoAsync(Id);
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

        public async Task InserirAsync(Login login)
        {
            _context.Add(login);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<Login>> ListarTodosRegistrosAsync(int? pagina)
        {

            int numeroPagina = pagina ?? 1;
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");

            return await _context.Login.ToPagedListAsync<Login>(numeroPagina, RegistroPorPagina);

        }

        public async Task<IEnumerable<Login>> ListarTodosRegistrosAsync()
        {
            return await _context.Login.ToListAsync();

        }

        public async Task<Login> SelecionarPorEmailSenhaAsync(string Email, string Senha)
        {
            return await _context.Login.Where(m => m.EMail == Email && m.Password == Senha).FirstOrDefaultAsync();
            
        }

    }

}