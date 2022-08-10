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
        Task<Login> SelecionarPorEmailSenhaAsync(double CPNJ_CPF, string Email, string Senha);

        Task<IPagedList<Login>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<Login>> ListarTodosRegistrosAsync();


    }
}
