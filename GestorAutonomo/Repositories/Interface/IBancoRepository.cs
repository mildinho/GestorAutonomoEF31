using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IBancoRepository
    {
        Task InserirAsync(Banco banco);

        Task AtualizarAsync(Banco banco);

        Task DeletarAsync(int Id);

        Task<Banco> SelecionarPorCodigoAsync(int? Id);

        Task<IPagedList<Banco>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

        Task<IEnumerable<Banco>> ListarTodosRegistrosAsync();


    }
}
