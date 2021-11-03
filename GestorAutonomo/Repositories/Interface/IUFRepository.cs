using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IUFRepository
    {
        Task InserirAsync(UF uf);

        Task AtualizarAsync(UF uf);

        Task DeletarAsync(int Id);

        Task<UF> SelecionarPorCodigoAsync(int? Id);

        Task<IPagedList<UF>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<UF>> ListarTodosRegistrosAsync();


    }
}
