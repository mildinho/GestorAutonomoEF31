using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Domain.Interfaces;
using GestorAutonomo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public async Task<Login> SelecionarPorEmailSenhaAsync(double CPNJ_CPF, string Email, string Senha)
        {
            return await _context.Login.Where(m => m.Empresa.CNPJ_CPF == CPNJ_CPF &&  m.EMail == Email && m.Password == Senha).FirstOrDefaultAsync();

        }

    }

}