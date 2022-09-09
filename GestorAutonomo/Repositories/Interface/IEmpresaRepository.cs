using GestorAutonomo.Domain.Entities;
using GestorAutonomo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GestorAutonomo.Repositories.Interface
{
    public interface IEmpresaRepository : IGenericoRepository<Empresa>
    {
    
        Task<IPagedList<Empresa>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<Empresa>> ListarTodosRegistrosAsync();

        Empresa AjustarCampos(Empresa empresa);



    }
}
