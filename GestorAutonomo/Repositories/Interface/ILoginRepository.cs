using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface ILoginRepository : IGenericoRepository<Login>
    {
        Task AtualizarAsync(Login login);

        Task<Login> SelecionarPorEmailSenhaAsync(string Email, string Senha);

        Task<IPagedList<Login>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<Login>> ListarTodosRegistrosAsync();


    }
}
