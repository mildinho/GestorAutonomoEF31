using GestorAutonomo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Domain.Interfaces
{
    public interface IUFRepository : IGenericoRepository<UF>
    {
        Task<IPagedList<UF>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

        Task<IEnumerable<UF>> ListarTodosRegistrosAsync();


    }
}
