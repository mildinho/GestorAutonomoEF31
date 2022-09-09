﻿using GestorAutonomo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IUFRepository : IGenericoRepository<UF>
    {
        Task<IPagedList<UF>> ListarTodosRegistrosAsync(int? pagina, string pesquisa);

        Task<IEnumerable<UF>> ListarTodosRegistrosAsync();


    }
}
