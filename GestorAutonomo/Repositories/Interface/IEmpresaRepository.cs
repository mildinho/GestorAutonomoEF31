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
        Task InserirAsync(Empresa empresa);

        Task AtualizarAsync(Empresa empresa);

        Task DeletarAsync(int Id);

         Task<IPagedList<Empresa>> ListarTodosRegistrosAsync(int? pagina);

        Task<IEnumerable<Empresa>> ListarTodosRegistrosAsync();

        Empresa AjustarCampos(Empresa empresa);



    }
}
