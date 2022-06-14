using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IUFRepository : IGenericoRepository<UF>
    {
        Task AtualizarAsync(UF uf);

        Task<IPagedList<UF>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

        Task<IEnumerable<UF>> ListarTodosRegistrosAsync();


    }
}
